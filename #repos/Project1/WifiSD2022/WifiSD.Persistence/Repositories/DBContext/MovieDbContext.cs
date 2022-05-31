using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WifiSD.Core.Entities.Movies;

namespace WifiSD.Persistence.Repositories.DBContext
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext() { }
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(60); //Standart = 90 Sek
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediumType> MediumTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var currentDirectory = Directory.GetCurrentDirectory();

#if DEBUG
            if (currentDirectory.IndexOf("bin") > -1)
            {
                currentDirectory = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));
            }

#endif
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("MovieDbContext");
            //connectionString = configuration.GetConnectionString(nameof(MovieDbContext) + "Local");
            var commandTimeout = 60;
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout(commandTimeout));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(p => p.Id); /* Nicht notwendig, da implizit als Key verwendet */
                entity.Property(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(256).HasDefaultValue("Unknown");
                entity.HasIndex(p => p.Name).HasName("IX_Movies_Name");
                entity.Property(p => p.ReleaseDate).HasColumnType("date");
                //entity.Property(p => p.Price).HasColumnType("decimal(18,6)");

            });

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MediumType)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.MediumTypeCode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Movies_MediumTypes_MediumTypeCode");

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Movies) /* Ein Genre kann in beliebig vielen Movie Datensätzen verwendet werden. */
                .WithOne(m => m.Genre) /* Jedes Genre existiert nur einmal und für Movies kann nur ein Genre definiert werden */
                .HasForeignKey(g => g.GenreId) /* Fremdschlüsselfeld PK <=> FK */
                .OnDelete(DeleteBehavior.Restrict); /* Löschweitergabe unterbinden*/

            modelBuilder.Entity<Genre>().HasData(
                
                new Genre { Id = 1, Name = "Action"},
                new Genre { Id = 2, Name = "Horror"},
                new Genre { Id = 3, Name = "Drama"},
                new Genre { Id = 4, Name = "Science Fiction"},
                new Genre { Id = 5, Name = "Comedy"}
                );

            modelBuilder.Entity<MediumType>().HasData(

                new MediumType { Code = "DVD", Name = "Digital Versitale Disc"},
                new MediumType { Code = "BR", Name = "Blue Ray"},
                new MediumType { Code = "BR-3D", Name = "Blue Ray 3D"},
                new MediumType { Code = "BR-HDR", Name = "Blue Ray High Definition Res."},
                new MediumType { Code = "VHS", Name = "VHS"}
                );

            modelBuilder.Entity<Movie>().HasData(

                new Movie { Id = new Guid("0F3924AE-4753-4337-9A6B-FB597B5D85C7"), GenreId = 4, Name = "Schlimmer gehts immer", Price = 20.99m, MediumTypeCode = "BR", ReleaseDate = new DateTime(2017, 9, 14), Rating = Ratings.Great },
                new Movie { Id = new Guid("1BD83417-F42A-411D-A2FB-D35C85F5B8DC"), GenreId = 1, Name = "Stirb langsam", Price = 7.90m, MediumTypeCode = "DVD", ReleaseDate = new DateTime(1988, 11, 11), Rating = Ratings.Bad },
                new Movie { Id = new Guid("0EE99D8E-26FF-40C4-80EF-3B30AB60EE29"), GenreId = 3, Name = "Titanic", Price = 9.90m, MediumTypeCode = "BR-3D", ReleaseDate = new DateTime(1994, 10, 14), Rating = Ratings.Medium }

                );

        }

        /* Command für Initialisierung der EF-Migration
         * 1. PM Console öffnen
         * 2. add-migration Initial -startupProject WifiSD.Persistence
         * 
         * 3. update-database -startupProject WifiSD.Persistence 
         */

    }
}
