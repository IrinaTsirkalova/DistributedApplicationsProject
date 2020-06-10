using Microsoft.EntityFrameworkCore;
using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Data
{
    public class MusicLibraryDbContext : DbContext
    {
        public MusicLibraryDbContext() : base()
        {

        }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseLazyLoadingProxies()
               .UseSqlServer(@"Server=DESKTOP-AOQS9DM\SQLEXPRESS;" +
                             @"DataBase=MusicLibrary;" +
                             @"Integrated Security=true;");



            base.OnConfiguring(optionsBuilder);
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ограничение на символите на текстова колона на ниво база и задължително поле
            modelBuilder.Entity<Album>()
                           .HasOne(a => a.Artist)
                           .WithMany(al => al.Albums)
                           .HasForeignKey(al => al.ArtistId)
                           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Album>()
                          .HasOne(a => a.Genre)
                          .WithMany(al => al.Albums)
                          .HasForeignKey(al => al.GenreId)
                          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Album>()
                          .HasOne(a => a.Song)
                          .WithMany(al => al.Albums)
                          .HasForeignKey(al => al.SongId)
                          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Song>()
                           .HasOne(a => a.Artist)
                           .WithMany(al => al.Songs)
                           .HasForeignKey(al => al.ArtistId)
                           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Song>()
                          .HasOne(a => a.Genre)
                          .WithMany(al => al.Songs)
                          .HasForeignKey(al => al.GenreId)
                          .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }

}
