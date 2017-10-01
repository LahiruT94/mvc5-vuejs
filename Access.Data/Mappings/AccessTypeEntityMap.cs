using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Access.Data.Mappings
{
    using Models;

    public class AccessTypeEntityMap : EntityTypeConfiguration<AccessTypeEntity>
    {
        public AccessTypeEntityMap()
        {
            ToTable("access_type");
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(s => s.AccessList).WithRequired(s => s.AccessType).HasForeignKey(s => s.AccessTypeId);
        }
    }
}
