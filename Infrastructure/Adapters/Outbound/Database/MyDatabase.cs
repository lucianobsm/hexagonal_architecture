using Infrastructure.Adapters.Outbound.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Outbound.Database;

public class MyDatabase(DbContextOptions<MyDatabase> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    
    // public string DbPath { get; }

    // public MyDatabase()
    // {
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = Path.Join(path, "mydatabase.db");
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     =>  optionsBuilder.UseSqlite($"Data Source={DbPath}");
}