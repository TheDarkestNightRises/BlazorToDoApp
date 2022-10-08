using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private ITodoHome toDoHome;

    public TodosController(ITodoHome toDoHome)
    {
        this.toDoHome = toDoHome;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ToDo>>> GetAll()
    {
        try
        {
            ICollection<ToDo> toDos = await toDoHome.GetAsync();
            return Ok(toDos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> AddToDo([FromBody] ToDo toDo)
    {
        try
        {
            ToDo added = await toDoHome.AddAsync(toDo);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ToDo>> GetToDoByID([FromRoute] int id)
    {
        try
        {
            ToDo toDo = await toDoHome.GetById(id);
            return toDo;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteToDoByID([FromRoute] int id)
    {
        try
        {
            await toDoHome.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> Update([FromBody] ToDo toDo)
    {
        try
        {
            await toDoHome.UpdateAsync(toDo);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}