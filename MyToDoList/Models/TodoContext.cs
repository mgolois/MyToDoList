using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
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
        public ToDoDbContext(DbContextOptions options) :base(options) { }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
