using System.Text.Json;
using Domain.Models;

namespace FileData.DataAccess;

public class FileContext
{
    private string todoFilePath = "todos.json";

    private ICollection<ToDo> todos;

    public ICollection<ToDo> Todos
    {
        get
        {
            if (todos == null)
            {
                LoadData();
            }
            
            return todos;
        }
    }

    public FileContext()
    {
        if (!File.Exists(todoFilePath))
        {
            Seed();
        }
    }

    private void Seed()
    {
        ToDo[] ts = {
            new ToDo(1, "Dishes") {
                Id = 1,
            },
            new ToDo(1, "Walk the dog") {
                Id = 1,
            },
            new ToDo(2, "Do DNP homework") {
                Id = 3,
            },
            new ToDo(3, "Eat breakfast") {
                Id = 4,
            },
            new ToDo(4, "Mow lawn") {
                Id = 5,
            },
        };
        todos = ts.ToList();
        SaveChanges();
    }

    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Todos);
        File.WriteAllText(todoFilePath,serialize);
        todos = null;
    }

    private void LoadData()
    {
        string content = File.ReadAllText(todoFilePath);
        todos = JsonSerializer.Deserialize<List<ToDo>>(content);
    } 
}