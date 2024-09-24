using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseSaver
    {
        #region UsersTab
        public static async Task SaveUser(User user)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MERGE (u:User {FullName: $fullName})
                ON CREATE SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                ON MATCH SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate";
            
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = user.Login.Trim(),
                    fullName = user.FullName.Trim(),
                    password = user.Password.Trim(),
                    userRole = user.UserRole.Trim(),
                    logDate = user.DateOfLogin.Trim()
                });
            });
        }

        public static async Task SaveUserWithAssignee(User userToSave, User assignee)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MERGE (u:User {FullName: $fullName})
                ON CREATE SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                ON MATCH SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                MERGE (a:User {FullName: $assigneeFullName})
                
                WITH u, a
                OPTIONAL MATCH (u)-[r:ASSIGNED_BY]->() DELETE r
                MERGE (u)-[:ASSIGNED_BY {assignedDate: $assignedDate}]->(a)";

            string currentDate = DateTime.Now.ToString();
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = userToSave.Login.Trim(),
                    fullName = userToSave.FullName.Trim(),
                    password = userToSave.Password.Trim(),
                    userRole = userToSave.UserRole.Trim(),
                    logDate = userToSave.DateOfLogin.Trim(),

                    assigneeFullName = assignee.FullName.Trim(),
                    assignedDate = currentDate.Trim()
                });
            });
        }

        public static async Task UpdateLastLoginTime(User user)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MERGE (u:User {FullName: $fullName})
                ON CREATE SET u.DateOfLogin = $currentDate
                ON MATCH SET u.DateOfLogin = $currentDate";
            string loginDate = DateTime.Now.ToString();
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = user.Login.Trim(),
                    fullName = user.FullName.Trim(),
                    password = user.Password.Trim(),
                    userRole = user.UserRole.Trim(),
                    currentDate = loginDate.Trim()
                });
            });
        }

        public static async Task SetNewPassword(string userLogin, string newPassword)
        {
            userLogin = userLogin.Trim();
            newPassword = newPassword.Trim();
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MATCH (u:User {Login: $userLogin})
                SET u.Password = $newPassword
                RETURN u";
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new { userLogin, newPassword });
            });
        }
        #endregion

        #region PersonnelTab
        public static async Task SavePerson(MilitaryPerson person)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var personQuery = @"
                MERGE (p:MilitaryPerson {FullName: $name})
                ON CREATE SET 
                    p.Rank = $rank, 
                    p.Specialities = $specialities, 
                    p.SpecialProperty1 = $SP1,  
                    p.SpecialProperty2 = $SP2
                ON MATCH SET 
                    p.Rank = $rank, 
                    p.Specialities = $specialities, 
                    p.SpecialProperty1 = $SP1,  
                    p.SpecialProperty2 = $SP2";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(personQuery, new
                {
                    name = person.FullName.Trim(),
                    rank = person.Rank,
                    specialities = person.Specialities.Trim(),
                    SP1 = person.SpecialProperty1.Trim(),
                    SP2 = person.SpecialProperty2.Trim()
                });
            });
        }
        public static async Task SavePersonnelInfo(FullPersonnelInfo info)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            await session.ExecuteWriteAsync(async tx =>
            {
                // Check if the MilitaryBase exists
                var militaryBaseQuery = @"
                MATCH (s:Structure {Name: $name, Type: 'Військова частина'})
                RETURN s
            ";
                var militaryBaseResult = await tx.RunAsync(militaryBaseQuery, new { name = info.MilitaryBase });
                if (!await militaryBaseResult.FetchAsync() && !string.IsNullOrEmpty(info.MilitaryBase))
                {
                    throw new Exception($"Military base '{info.MilitaryBase}' not found.");
                }

                // Check if the StructuresUnderControl exist
                var structuresUnderControl = info.StructuresUnderControl.Split(',').Select(s => s.Trim()).ToList();
                foreach (var structure in structuresUnderControl)
                {
                    var structureQuery = @"
                    MATCH (s:Structure {Name: $name})
                    WHERE s.Type IN ['Відділення', 'Взвод']
                    RETURN s
                ";
                    var structureResult = await tx.RunAsync(structureQuery, new { name = structure });
                    if (!await structureResult.FetchAsync())
                    {
                        throw new Exception($"Structure '{structure}' not found or is not of type 'Відділення' or 'Взвод'.");
                    }
                }

                // Create or update MilitaryPerson node
                var createPersonQuery = @"
                MERGE (p:MilitaryPerson {FullName: $fullName})
                SET p.Rank = $rank,
                    p.Specialities = $specialities,
                    p.SpecialProperty1 = $specialProperty1,
                    p.SpecialProperty2 = $specialProperty2
                WITH p
                OPTIONAL MATCH (p)-[r:REGISTERED_IN]->()
                DELETE r
                WITH p
                OPTIONAL MATCH (p)-[c:COMMANDS]->()
                DELETE c
                WITH p
                MATCH (mb:Structure {Name: $militaryBase, Type: 'Військова частина'})
                CREATE (p)-[:REGISTERED_IN]->(mb)
                WITH p
                UNWIND $structuresUnderControl AS structureName
                MATCH (s:Structure {Name: structureName})
                WHERE s.Type IN ['Відділення', 'Взвод']
                CREATE (p)-[:COMMANDS]->(s)
            ";

                await tx.RunAsync(createPersonQuery, new
                {
                    fullName = info.MilitaryPerson.FullName,
                    rank = info.MilitaryPerson.Rank,
                    specialities = info.MilitaryPerson.Specialities,
                    specialProperty1 = info.MilitaryPerson.SpecialProperty1,
                    specialProperty2 = info.MilitaryPerson.SpecialProperty2,
                    militaryBase = info.MilitaryBase,
                    structuresUnderControl
                });
            });
        }
        #endregion

        #region Structures
        public static async Task SaveStructure(Structure structure)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var structureQuery = @"
                MERGE (s:Structure {Name: $name})
                ON CREATE SET 
                    s.Rank = $name, 
                    s.Type = $type, 
                    s.SpecialProperty = $SP
                ON MATCH SET 
                    s.Rank = $name, 
                    s.Type = $type, 
                    s.SpecialProperty = $SP";
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(structureQuery, new
                {
                    name = structure.Name.Trim(),
                    type = structure.Type,
                    SP = structure.SpecialProperty.Trim()
                });
            });
        }

        public static async Task UpdateStructure(Structure oldStructure, Structure newStructure)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var query = @"
                MATCH (s:Structure {Name: $oldName})
                SET s.Name = $newName, s.SpecialProperty = $newSpecialProperty
                RETURN s";
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    oldName = oldStructure.Name.Trim(),
                    newName = newStructure.Name.Trim(),
                    newSpecialProperty = newStructure.SpecialProperty.Trim()
                });
            });
        }
        #endregion
    }
}
