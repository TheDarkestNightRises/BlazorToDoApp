using Domain.Models;

namespace Domain.Interfaces;

public interface ITodoHome
{
    public Task<ICollection<ToDo>> GetAsync();
    public Task<ToDo> GetById(int id);
    public Task<ToDo> AddAsync(ToDo todo);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(ToDo todo);
}