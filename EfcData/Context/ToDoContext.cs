using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcData.Context;

public class ToDoContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source = D:\S3\Blazor\BlazorToDoApp\EfcData\Todo.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDo>().HasKey(todo => todo.Id);
    }
    
    // public void Seed()
    // {
    //     if (ToDos.Any()) return;
    //
    //     ToDo[] ts =
    //     {
    //         new ToDo(1, "Dishes"),
    //         new ToDo(1, "Walk the dog"),
    //         new ToDo(2, "Do DNP homework"),
    //         new ToDo(3, "Eat breakfast"),
    //         new ToDo(4, "Mow lawn"),
    //     };
    //     ToDos.AddRange(ts);
    //     SaveChanges();
    // }

}