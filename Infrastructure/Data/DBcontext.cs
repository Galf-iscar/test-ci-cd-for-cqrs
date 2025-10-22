using Microsoft.EntityFrameworkCore;
using test_ci_cd_for_cqrs.Domain.Models;

namespace test_ci_cd_for_cqrs.Infrastructure.Data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
