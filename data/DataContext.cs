using backend.model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Objects> Objects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<User_Role> User_Roles { get; set; }

        
    }
}
