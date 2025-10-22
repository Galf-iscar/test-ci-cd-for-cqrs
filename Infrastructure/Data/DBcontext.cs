using Microsoft.EntityFrameworkCore;
using CQRS_App.Domain.Models;

namespace CQRS_App.Infrastructure.Data;

public class DBcontext : DbContext
{
    public DBcontext(DbContextOptions<DBcontext> options)
        : base(options) { }

    public DbSet<Employee> Employees { get; set; }
}
