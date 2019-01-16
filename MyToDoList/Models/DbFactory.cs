using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace MyToDoList.Models
{
    public interface IDbFactory
    {
        ToDoDbContext Context { get; set; }
    }
    public class DbFactory : IDbFactory
    {
        public ToDoDbContext Context { get; set; }
        public DbFactory(ToDoDbContext ctx)
        {
            Context = ctx;
            var conn = (SqlConnection)Context.Database.GetDbConnection();
            conn.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
        }

    }
}
