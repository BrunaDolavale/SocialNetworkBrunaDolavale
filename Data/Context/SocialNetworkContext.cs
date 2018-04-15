using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class SocialNetworkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PhotoProfile> PhotoProfiles { get; set; }
        public System.Data.Entity.DbSet<DomainModel.Entities.Message> Messages { get; set; }

        public SocialNetworkContext()
            : base(Data.Properties.Settings.Default.DbConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasRequired(u => u.PhotoProfile);

            //Para Objeto de Valor
            //modelBuilder.Entity<User>()
            //    .Map(user =>
            //    {
            //        user.ToTable("User");
            //        user.Property(u => u.PhotoProfile.Url).HasColumnName("PhotoUrl");
            //    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
