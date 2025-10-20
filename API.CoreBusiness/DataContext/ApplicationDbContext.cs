using API_CoreBusiness.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_CoreBusiness.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) 
        {
                
        }

        public DbSet<Usuarios> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email);
                entity.HasKey(e => e.Fecha_Add);
                entity.Property(e => e.Fecha_Mod);
                entity.Property(e => e.PasswordSalt);
                entity.Property(e => e.PasswordHash);

            
            });

            
        }
    }
}
