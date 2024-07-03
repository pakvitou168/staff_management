using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class StaffContext:DbContext
    {
        public StaffContext(DbContextOptions<StaffContext> options) : base(options) 
        {
            
        }

        public DbSet<Staff> Staff { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.;Database =StaffManagementApiDB;Integrated Security = True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
