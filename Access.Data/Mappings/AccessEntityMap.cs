using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Access.Data.Mappings
{
    using Models;
    public class AccessEntityMap : EntityTypeConfiguration<AccessEntity>
    {
        public AccessEntityMap()
        {
            ToTable("access");
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(s => s.Project).WithMany().HasForeignKey(s => s.ProjectId);
            HasRequired(s => s.AccessType).WithMany().HasForeignKey(s => s.AccessTypeId);
        }
    }
}
