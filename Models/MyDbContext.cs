using Microsoft.EntityFrameworkCore;

namespace ApiUsuarios.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios {get; set;}
    }
}