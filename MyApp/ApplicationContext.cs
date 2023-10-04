using Microsoft.EntityFrameworkCore;

namespace MyApp;

public class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost; user = root; password = Dudkibar88; database= Employees;"
            ,
            new MySqlServerVersion(new Version(8, 0, 34))
        );
    }
}