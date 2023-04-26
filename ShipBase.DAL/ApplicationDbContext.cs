using ShipBase.Domain.Entity;
using ShipBase.Domain.Enum;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShipBase.Domain.Helpers;

namespace ShipBase.DAL
{
    public class ApplicationDbContext : DbContext
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public ApplicationDbContext()
        {

        }


        public DbSet<Profile> Profiles { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User
                {
                    Id = 1,
                    Login = "Admin",
                    Password = HashPasswordHelper.HashPassowrd("123456"),
                    Role = Role.Admin
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Login).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

               
            });

            

           

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age);
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);
                builder.Property(x => x.FirstName).HasMaxLength(200).IsRequired(false);
                builder.Property(x => x.Patronymic).HasMaxLength(200).IsRequired(false);
                builder.Property(x => x.SecondName).HasMaxLength(200).IsRequired(false);
                builder.Property(x => x.PhoneNum).HasMaxLength(200).IsRequired(false);
                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1
                });
            });

        }

    }
}