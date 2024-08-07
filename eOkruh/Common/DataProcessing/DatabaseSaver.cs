using eOkruh.Common.UserManagement;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseSaver
    {
        public static async Task SaveUser(User user)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(Strings.userDatabase));
            var query = @"
                MERGE (u:User {FullName: $fullName})
                ON CREATE SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                ON MATCH SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate";
            
            string loginDate = Strings.noData;
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = user.Login,
                    fullName = user.FullName,
                    password = user.Password,
                    userRole = user.UserRole,
                    logDate = loginDate
                });
            });
        }

        public static async Task SaveUserWithAssignee(User userToSave, User assignee)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(Strings.userDatabase));
            var query = @"
                MERGE (u:User {FullName: $fullName})
                ON CREATE SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                ON MATCH SET u.Login = $login, u.Password = $password, u.UserRole = $userRole, u.DateOfLogin = $logDate
                
                MERGE (a:User {FullName: $assigneeFullName})
                OPTIONAL MATCH (u)-[r:ASSIGNED_BY]->() DELETE r
                MERGE (u)-[:ASSIGNED_BY {assignedDate: $assignedDate}]->(a)";

            string loginDate = Strings.noData;
            string currentDate = DateTime.Now.ToString();
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = userToSave.Login,
                    fullName = userToSave.FullName,
                    password = userToSave.Password,
                    userRole = userToSave.UserRole,
                    logDate = loginDate,

                    assigneeFullName = assignee.FullName,
                    assignedDate = currentDate
                });
            });
        }

        public static async Task UpdateLastLoginTime(User user)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(Strings.userDatabase));
            var query = @"
                MERGE (u:User {FullName: $fullName})
                ON CREATE SET u.DateOfLogin = $currentDate
                ON MATCH SET u.DateOfLogin = $currentDate";
            string loginDate = DateTime.Now.ToString();
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = user.Login,
                    fullName = user.FullName,
                    password = user.Password,
                    userRole = user.UserRole,
                    currentDate = loginDate
                });
            });
        }

        public static async Task SetNewPassword(string userLogin, string newPassword)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(Strings.userDatabase));
            var query = @"
                MATCH (u:User {Login: $userLogin})
                SET u.Password = $newPassword
                RETURN u";
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new { userLogin, newPassword });
            });
        }        
    }
}
