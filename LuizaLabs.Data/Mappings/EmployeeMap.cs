using LuizaLabs.Domain;
using System.Data.Entity.ModelConfiguration;

namespace LuizaLabs.Data.Mappings
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>  
    {
        public EmployeeMap()
        {
            ToTable("Employee");

            HasKey(x => x.Id);

            Property(x => x.Name).HasMaxLength(170).IsRequired();
            Property(x => x.Email).HasMaxLength(70).IsRequired();
            Property(x => x.Department).HasMaxLength(170);
        }
    }
}
