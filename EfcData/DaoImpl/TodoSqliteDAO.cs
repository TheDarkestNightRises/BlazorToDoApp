using Domain.Interfaces;
using Domain.Models;
using EfcData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData.DaoImpl;

public class TodoSqliteDao : ITodoHome
{
    private readonly ToDoContext context;

    public TodoSqliteDao(ToDoContext context)
    {
        this.context = context;
    }

    public async Task<ICollection<ToDo>> GetAsync()
    {
        ICollection<ToDo> todos = await context.ToDos.ToListAsync();
        return todos;
    }

    public Task<ToDo> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ToDo> AddAsync(ToDo todo)
    {
        EntityEntry<ToDo> added = await context.AddAsync(todo);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        ToDo? existing = await context.ToDos.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find Todo with id {id}. Nothing was deleted");
        }

        context.ToDos.Remove(existing);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(ToDo todo)
    {
        context.ToDos.Update(todo);
        return context.SaveChangesAsync();
    }   
}