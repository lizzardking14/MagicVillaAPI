using MagicVilla_API.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext : IdentityDbContext<UsuariosAplicacion>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UsuariosAplicacion> UsuariosAplicacion { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVillas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Villa Real",
                    Detalle = "Datalle de la Villa Real",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Villa()
                {
                 Id = 2,
                 Nombre = "Premium Vista a la Piscina",
                 Detalle = "Datalle de la Villa Premium",
                 ImagenUrl = "",
                 Ocupantes = 4,
                 MetrosCuadrados = 40,
                 Tarifa = 150,
                 Amenidad = "",
                 FechaCreacion = DateTime.Now,
                 FechaActualizacion = DateTime.Now
                 }
                );
        }
    }
}
