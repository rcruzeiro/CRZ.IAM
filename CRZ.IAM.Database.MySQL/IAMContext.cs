using CRZ.Framework.Repository;
using CRZ.IAM.Database.MySQL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CRZ.IAM.Database.MySQL
{
    public sealed class IAMContext : BaseContext
    {
        // for EF Migration only
        public IAMContext()
            : base("Server=localhost;Port=3306;Uid=root;Pwd=root;Database=iam")
        { }

        public IAMContext(IDataSource source)
            : base(source)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
