using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Database.Framework;

namespace Q100BioFarma.Modules.Common.Models.Datas;

public class Steps : IEntity, IEntityRegister
{
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Name { get; set; }
    
    public Guid RecipeId { get; set; }
    
    public int Ordering { get; set; }
    
    public Recipes Recipe { get; set; }
    
    public void RegisterEntities(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Steps>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.CreatedBy)
                .HasColumnName("created_by");

            entity.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by");

            entity.Property(x => x.DeletedBy)
                .HasColumnName("deleted_by");

            entity.Property(x => x.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(x => x.DeletedAt)
                .HasColumnName("deleted_at");

            entity.HasQueryFilter(x => x.DeletedAt == null);

            entity.Property(x => x.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            
            entity.Property(x => x.RecipeId)
                .HasColumnName("recipe_id");
            
            entity.HasOne(e => e.Recipe)
                .WithMany(e => e.Steps)
                .HasForeignKey(e => e.RecipeId);
            
            entity.Property(x => x.Ordering)
                .HasColumnName("ordering");

            entity.ToTable("steps");
        });
    }
}