namespace MedicSoft.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Entities;
    using System.Linq;

    public class DataContext : IdentityDbContext<User>
    {
        //public DbSet<Product> Products { get; set; }

        //public DbSet<Country> Countries { get; set; }

        //public DbSet<Order> Orders { get; set; }

        //public DbSet<OrderDetail> OrderDetails { get; set; }

        //public DbSet<OrderDetailTemp> OrderDetailTemps { get; set; }

        //public DbSet<City> Cities { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: CODIGO PARA LOS CAMPOS DE TIPO decimal..

            //modelBuilder.Entity<Product>()
            //.Property(p => p.Price)
            //.HasColumnType("decimal(18,2)");

            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership &&
                fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }


            base.OnModelCreating(modelBuilder);
        }



    }
}
