using Microsoft.EntityFrameworkCore;

namespace Sube1.HelloMVC.Models
{
    public class OkulDbContext:DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<OgrenciDers> OgrenciDersler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-EVF85FE;Initial Catalog=OkulDbMVC;Persist Security Info=True;User ID=sa;Password=286118;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ogrenci>().ToTable("tblOgrenciler");
            modelBuilder.Entity<Ogrenci>().Property(o=>o.Ad).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Soyad).HasColumnType("varchar").HasMaxLength(40).IsRequired();

            modelBuilder.Entity<Ders>().ToTable("tblDersler");
            modelBuilder.Entity<Ders>().Property(o => o.Dersad).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Ders>().Property(o => o.Kredi).HasColumnType("tinyint").IsRequired();

            modelBuilder.Entity<OgrenciDers>().ToTable("tblOgrenciDersler"); 
            modelBuilder.Entity<OgrenciDers>().Property(o=>o.Dersid).HasColumnType("int").IsRequired();
            modelBuilder.Entity<OgrenciDers>().HasKey(od => new { od.Ogrenciid, od.Dersid });
            modelBuilder.Entity<OgrenciDers>().HasOne(od => od.Ogrenci).WithMany(o => o.OgrenciDersler).HasForeignKey(od => od.Ogrenciid);
            modelBuilder.Entity<OgrenciDers>().HasOne(od => od.Ders).WithMany(d => d.OgrenciDersler).HasForeignKey(od => od.Dersid);
        }
    }
}
