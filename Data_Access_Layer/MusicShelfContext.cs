using Microsoft.EntityFrameworkCore;
using Entity_Layer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
namespace Data_Access_Layer
{
    public class MusicShelfContext : DbContext
    {
        //Program.cs ?
        public MusicShelfContext(DbContextOptions<MusicShelfContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }


        //bunun eklenme sebebi migration yapabilmek. Web API çalıştırırken silinebilir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-NM069V3\\SQLEXPRESS;Initial Catalog=music_shelf_db;Integrated Security=True;TrustServerCertificate=True");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
