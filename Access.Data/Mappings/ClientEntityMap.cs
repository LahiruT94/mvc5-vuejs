using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Access.Data.Mappings
{
    using Models;
    public class ClientEntityMap : EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityMap()
        {
            ToTable("client");
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(s=>s.ProjectList).WithOptional().HasForeignKey(s => s.ClientId).WillCascadeOnDelete(true);
        }
    }
}
