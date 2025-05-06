using Microsoft.EntityFrameworkCore;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Database.Framework;

namespace Q100BioFarma.Modules.Common.Models.Datas;

public class SubSteps : IEntity, IEntityRegister
{
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Name { get; set; }
    
    public Guid StepId { get; set; }
    
    public int Ordering { get; set; }
    
    public Steps Steps { get; set; }
    
    public void RegisterEntities(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<SubSteps>(entity =>
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
            
            entity.Property(x => x.StepId)
                .HasColumnName("step_id");
            
            entity.HasOne(e => e.Steps)
                .WithMany(e => e.SubSteps)
                .HasForeignKey(e => e.StepId);
            
            entity.Property(x => x.Ordering)
                .HasColumnName("ordering");

            entity.ToTable("sub_steps");
        });
    }
}