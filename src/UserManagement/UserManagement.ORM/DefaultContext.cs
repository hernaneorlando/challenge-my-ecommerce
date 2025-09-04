using UserManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UserManagement.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}