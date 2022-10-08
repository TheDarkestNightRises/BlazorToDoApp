using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class ToDo
{ 
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int OwnerID { get; set; }

    [Required, MaxLength(128)] public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public ToDo(int ownerId, string title)
    {
        OwnerID = ownerId;
        Title = title;
    }

    public ToDo()
    {
        
    }

}