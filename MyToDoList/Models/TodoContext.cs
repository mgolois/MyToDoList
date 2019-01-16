using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoList.Models
{
    public class ToDoDbContext : DbContext
    {
        public DbContextOptions DbContextOptions { get; set; }
        public ToDoDbContext(DbContextOptions options) : base(options) { DbContextOptions = options; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public static SqlConnection GetToken(IConfiguration configuration)
        {
            var connStr = configuration["ConnectionString"];
            var conn = new SqlConnection(connStr);
            conn.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
            return conn;
        }
    }

    public interface IAuthenticatedContext
    {
        ToDoDbContext DbContext { get; set; }
    }

    public class AuthenticatedContext : IAuthenticatedContext
    {
        public ToDoDbContext DbContext { get; set; }
        public AuthenticatedContext()
        {
            DbContext = null;
            /// var conn = (SqlConnection)DbContext.Database.GetDbConnection();
            // conn.AccessToken = AccessToken();
        }


        private string AccessToken()
        {
            //https://docs.microsoft.com/en-us/azure/app-service/app-service-managed-service-identity
            //https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-connect-msi
            // Token lifetime https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-configurable-token-lifetimes#token-types
            // Adding ADD access to https://docs.microsoft.com/en-us/azure/sql-database/sql-database-aad-authentication-configure
            //Question how can we refresh the acessToken
            return (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
        }
    }
}
