using Domain.Entities;
using Domain.Wrapper;
using Npgsql;
using Dapper;
namespace Infrastructure.Services;

public class TodoService
{
    private string _connectionString;
    public TodoService()
    {
        _connectionString = "Server=127.0.0.1;Port=5432;Database=TodoDb;User Id=postgres;Password=1234;";
    }

    public async Task<Response<List<Todo>>>  GetTodo()
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                var res = await connection.QueryAsync<Todo>($"select * from Todo;");
                return new Response<List<Todo>>(res.ToList());
            }
            catch (Exception ex)
            {
                
                return new Response<List<Todo>>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
        }
    }
    public async Task<Response<Todo>>  AddTodo(Todo todo)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                var sql = $"insert into Todo(Title,Status)values('{todo.Title}',{(int)todo.Status});";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                todo.Id = id;
                return new Response<Todo>(todo);
            }
            catch (Exception ex)
            {
                
                return new Response<Todo>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
        }
    }
    public async Task<Response<Todo>>  UpdateTodo(Todo todo)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                var sql = $"UPDATE Todo SET Title='{todo.Title}',Status={todo.Status}  WHERE id = {todo.Id} returning id;";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                todo.Id = id;
                return new Response<Todo>(todo);
            }
            catch (Exception ex)
            {
                
                return new Response<Todo>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
        }
    }
    public async Task<Response<int>> DeleteTodo(int id)
    {
        // Add contact to database
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                
                string sql = $"delete from Todo where id = '{id}';";
                var response = await connection.ExecuteAsync(sql);
                return new Response<int>(response);
            }
            catch (System.Exception ex)
            {

                return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
