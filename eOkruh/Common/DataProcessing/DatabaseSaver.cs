using eOkruh.Common.UserManagement;
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
        public static async Task SavePersonnelInfo(FullPersonnelInfo info)
        {
            throw new NotImplementedException();// TODO implement
        }
        #endregion
    }
}
