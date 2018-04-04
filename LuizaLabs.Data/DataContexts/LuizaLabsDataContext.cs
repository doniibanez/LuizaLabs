using LuizaLabs.Data.Mappings;
using LuizaLabs.Domain;
using System.Data.Entity;

namespace LuizaLabs.Data.DataContexts
{
    public class LuizaLabsDataContext : DbContext
    {
        public LuizaLabsDataContext()
            : base("LuizaLabsConnectionString")
        {
            //Database.SetInitializer<LuizaLabsDataContext>(new LuizaLabsDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Employee> Employees{get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Mappings
            modelBuilder.Configurations.Add(new EmployeeMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class LuizaLabsDataContextInitializer : DropCreateDatabaseIfModelChanges<LuizaLabsDataContext>
    {
        protected override void Seed(LuizaLabsDataContext context)
        {
            context.Employees.Add(new Employee{Id=1, Email="lucadoni9@hotmail.com", Name="Lucas Donizeti", Department = "TI"});
            context.Employees.Add(new Employee{Id=1, Email="matheus@matheus.com", Name="Matheus Donizeti", Department = "Finanças"});
            context.Employees.Add(new Employee{Id=1, Email="thiago@thiago.com", Name="Thiago Donizeti", Department = "Finanças"});

            context.SaveChanges();
        }
    }
}
