using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;

namespace eOkruh.Common.DataProcessing
{
    public static class NeoSaver
    {
        #region UsersTab
        public static async Task SaveUser(User user)
        {
            using var session = NeoAccessor.driver.AsyncSession(o => 
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MERGE (u:{nameof(User)} {{FullName: $fullName}})
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
            using var session = NeoAccessor.driver.AsyncSession(o => 
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MERGE (u:{nameof(User)} {{FullName: $fullName}})
                ON CREATE SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                ON MATCH SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                MERGE (a:{nameof(User)} {{FullName: $assigneeFullName}})
                
                WITH u, a
                OPTIONAL MATCH (u)-[r:{NeoStrings.assignedByRelation}]->() DELETE r
                MERGE (u)-[:{NeoStrings.assignedByRelation} {{assignedDate: $assignedDate}}]->(a)";

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
            using var session = NeoAccessor.driver.AsyncSession(o => 
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MERGE (u:{nameof(User)} {{FullName: $fullName}})
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
            using var session = NeoAccessor.driver.AsyncSession(o => 
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MATCH (u:{nameof(User)} {{Login: $userLogin}})
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
            using var session = NeoAccessor.driver.AsyncSession();
            var personQuery = $@"
                MERGE (p:{nameof(MilitaryPerson)} {{FullName: $name}})
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
        #endregion

        #region Structures
        public static async Task SaveStructure(Structure structure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var structureQuery = $@"
                MERGE (s:{nameof(Structure)} {{Name: $name}})
                ON CREATE SET 
                    s.Name = $name, 
                    s.Type = $type, 
                    s.SpecialProperty = $SP
                ON MATCH SET 
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
        #endregion
    }
}
