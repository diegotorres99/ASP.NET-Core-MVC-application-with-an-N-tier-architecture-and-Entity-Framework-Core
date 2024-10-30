using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.DAL.DataContext
{
    public partial class ArchitectureNLayerContext : DbContext
    {
        public ArchitectureNLayerContext()
        {
                
        }

        public ArchitectureNLayerContext(DbContextOptions<ArchitectureNLayerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>(entity =>
            {
                // Specify primary key and table name
                entity.HasKey(e => e.id).HasName("id");
                entity.ToTable("Items");

                // Property configurations
                entity.Property(e => e.name)
                    .HasMaxLength(300)
                    .IsUnicode(false)   
                    .IsRequired();      

                entity.Property(e => e.description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .IsRequired();       

                entity.Property(e => e.startDate)
                    .HasColumnType("datetime") 
                    .IsRequired();             

                entity.Property(e => e.finishDate)
                    .HasColumnType("datetime") 
                    .IsRequired(false);        
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
