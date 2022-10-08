using Domain.Interfaces;
using Domain.Models;

namespace FileData.DataAccess;

public class ToDoFileDAO: ITodoHome
{
    private FileContext fileContext;

    public ToDoFileDAO(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }    
    
    public Task<ICollection<ToDo>> GetAsync()
    {
        ICollection<ToDo> todos = fileContext.Todos;
        return Task.FromResult(todos);
    }

    public Task<ToDo> GetById(int id)
    {
        ToDo byId = fileContext.Todos.First(t => t.Id == id);
        return Task.FromResult(byId);    
    }

    public Task<ToDo> AddAsync(ToDo todo)
    {
        int largestId = fileContext.Todos.Max(t => t.Id);
        int nextId = largestId + 1;
        todo.Id = nextId;
        fileContext.Todos.Add(todo);
        fileContext.SaveChanges();
        return Task.FromResult(todo);
    }

    public Task DeleteAsync(int id)
    {
        ToDo toDelete = fileContext.Todos.First(t => t.Id == id);
        fileContext.Todos.Remove(toDelete);
        fileContext.SaveChanges();
        return Task.CompletedTask;    
    }

    public Task UpdateAsync(ToDo todo)
    {
        ToDo toUpdate = fileContext.Todos.First(t => t.Id == todo.Id);
        toUpdate.IsCompleted = todo.IsCompleted;
        toUpdate.OwnerID = todo.OwnerID;
        fileContext.SaveChanges();
        return Task.CompletedTask;
    }
}