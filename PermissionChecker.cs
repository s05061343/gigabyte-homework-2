using Microsoft.Data.SqlClient;
using System.Data;

namespace gigabyte_homework_2
{
    public class PermissionChecker
    {
        private readonly string connectionString;

        public PermissionChecker(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable Query(string userName)
        {
            var sql = @"
            SELECT 
                u.UserName,
                STRING_AGG(r.RoleName, N'、') AS RoleName,
                f.FunctionName,
                MAX(CAST(rfp.CanCreate AS INT)) AS CanCreate,
                MAX(CAST(rfp.CanRead AS INT)) AS CanRead,
                MAX(CAST(rfp.CanUpdate AS INT)) AS CanUpdate,
                MAX(CAST(rfp.CanDelete AS INT)) AS CanDelete
            FROM Users u
            JOIN UserRoles ur ON u.UserId = ur.UserId
            JOIN Roles r ON ur.RoleId = r.RoleId
            JOIN RoleFunctionPermissions rfp ON r.RoleId = rfp.RoleId
            JOIN Functions f ON rfp.FunctionId = f.FunctionId
            WHERE u.UserName = @UserName
            GROUP BY u.UserName, f.FunctionName;";

            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }
    }
}
