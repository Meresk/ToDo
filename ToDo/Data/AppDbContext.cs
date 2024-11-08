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

        public DbSet<User_lw5_02> Users_lw5_02 { get; set; }
        public DbSet<Note_lw5_02> Notes_lw5_02 { get; set; }
    }
}
