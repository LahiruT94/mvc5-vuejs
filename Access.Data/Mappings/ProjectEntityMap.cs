using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Access.Data.Mappings
{
    using Models;
    public class ProjectEntityMap : EntityTypeConfiguration<ProjectEntity>
    {
        public ProjectEntityMap()
        {
            ToTable("project");
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(s => s.Client).WithMany().HasForeignKey(s => s.ClientId);
            HasMany(s=> s.AccessList).WithOptional().HasForeignKey(s => s.ProjectId).WillCascadeOnDelete(true);
        }
    }
}
