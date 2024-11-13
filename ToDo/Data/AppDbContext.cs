using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User_lw9_02> Users_lw9_02 { get; set; }
        public DbSet<Note_lw9_02> Notes_lw9_02 { get; set; }
    }
}
