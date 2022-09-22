using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TodoController
{
    private TodoService _todoServise;
    public TodoController()
    {
        _todoServise = new TodoService();
    }
    [HttpGet("GetTodo")]
    public async Task<Response<List<Todo>>> GetTodo()
    {
        return await _todoServise.GetTodo();
    }
    [HttpPost("AddTodo")]
    public async Task<Response<Todo>> AddTodo(Todo todo)
    {
        return await _todoServise.AddTodo(todo);
    }
    [HttpPut("UpdateQuotes")]
    public async Task<Response<Todo>> UpdateQuotes(Todo todo)
    {
        var res = await _todoServise.UpdateTodo(todo);
        return res;
    }
    [HttpDelete("DeleteTodo")]
    public async Task<Response<int>> DeleteTodo(int id)
    {
        return await _todoServise.DeleteTodo(id);
    }
}

