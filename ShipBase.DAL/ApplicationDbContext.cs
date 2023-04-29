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

        public DbSet<PurchasingData> PurchasingDatas { get; set; }

        public DbSet<Сustomer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User
                {
                    Id = 1,
                    Login = "admin",
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

            modelBuilder.Entity<PurchasingData>(builder =>
            {
                builder.ToTable("PurchasingData").HasKey(x => x.Id);

                builder.Property(x => x.Id);
                builder.Property(x => x.Purchase_stage).IsRequired(false); ;
                builder.Property(x => x.Num_Of_Applications);
                builder.Property(x => x.Method_of_purchasing).IsRequired(false);
                builder.Property(x => x.Start_data);
                builder.Property(x => x.End_data);
                builder.Property(x => x.Federal_law);
                builder.Property(x => x.Num_of_ships);
                builder.Property(x => x.Purchase_object).IsRequired(false);

                builder.HasData(new PurchasingData()
                {
                    Id = 32008913701,
                    Purchase_stage = "Закупка завершена",
                    Num_Of_Applications = 4,
                    Method_of_purchasing = "Аукцион",
                    Start_data = DateTimeOffset.Parse("2022-05-01"),
                    End_data = DateTimeOffset.Parse("2022-12-21"),
                    Federal_law = 44,
                    Num_of_ships = 4,
                    Purchase_object = "Широкоформатное цветное МФУ с дополнительными картриджами",

                }) ;

            });

            modelBuilder.Entity<Сustomer>(builder =>
            {
                builder.ToTable("Сustomer").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name_of_organization).IsRequired(false); ;
                builder.Property(x => x.OGRN);
                builder.Property(x => x.INN);
                builder.Property(x => x.KPP);
                builder.HasOne(x => x.Purchasing_data)
               .WithMany()
               .HasForeignKey(x => x.purchasing_id);


                builder.HasData(new Сustomer()
                {
                    Id = 1,
                    Name_of_organization = "ЮЖНЫЙ ЦЕНТР СУДОСТРОЕНИЯ И СУДОРЕМОНТА",
                    OGRN = 1133023000109,
                    INN = 3023004670,
                    KPP = 302301001,
                    purchasing_id = 32008913701
                  


                });

            });


        }

    }
}