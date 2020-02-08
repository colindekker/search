namespace D3.Tests.Core.Search.EFCore.Query.Handlers
{
    using D3.Tests.Models.Entities;
    using Microsoft.EntityFrameworkCore;

    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
    }
}
