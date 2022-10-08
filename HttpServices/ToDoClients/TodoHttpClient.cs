using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;

namespace HttpServices.ToDoClients;

public class TodoHttpClient : ITodoHome
{
    public async Task<ICollection<ToDo>> GetAsync() 
    {
        using HttpClient client = new (); 
        HttpResponseMessage response = await client.GetAsync("https://localhost:7254/Todos"); 
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) 
        { 
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        ICollection<ToDo> todos = JsonSerializer.Deserialize<ICollection<ToDo>>(content,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!; 
        return todos; 
    }
    
    public async Task<ToDo> AddAsync(ToDo todo)
    {
        using HttpClient client = new();
        string toDoAsJson = JsonSerializer.Serialize(todo);
        StringContent content = new(toDoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://localhost:7254/todos", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        ToDo returned = JsonSerializer.Deserialize<ToDo>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
        return returned;
    }
    
    public Task<ToDo> GetById(int id)
    {
        throw new NotImplementedException();
    }


    public async Task DeleteAsync(int id)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7254/todos/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }

    public async Task UpdateAsync(ToDo todo)
    {
        using HttpClient client = new();

        string todoAsJson = JsonSerializer.Serialize(todo);

        StringContent content = new(todoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("https://localhost:7254/todos", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    }
}