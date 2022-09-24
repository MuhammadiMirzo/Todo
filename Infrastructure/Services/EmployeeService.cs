using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services;

public class EmployeeService
{
     private DataContext.DataContext _context;
    public EmployeeService(DataContext.DataContext context)
    {
        _context = context;
    }


    public async Task<Response<List<GetEmployeeDto>>> GetEmployeeDto()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetEmployeeDto>($"SELECT E.Id,CONCAT(E.FirstName, ' ', E.LastName) AS {"FullName"}, D.Id as {"DepartmentId"}, D.Name as {"DepartmentName"} FROM department AS D INNER JOIN Department_employee AS O ON O.DepartmentId = D.Id INNER JOIN employee AS E ON O.EmployeeId = E.Id;");
                return new Response<List<GetEmployeeDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<List<GetEmployeeDto>>> GetEmployeeDtoById(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetEmployeeDto>($"SELECT E.Id,CONCAT(E.FirstName, ' ', E.LastName) AS {"FullName"}, D.Id as {"DepartmentId"}, D.Name as {"DepartmentName"} FROM department AS D INNER JOIN Department_employee AS O ON O.DepartmentId = D.Id INNER JOIN employee AS E ON O.EmployeeId = E.Id where E.Id = {id};");
                return new Response<List<GetEmployeeDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Employee>> AddEmployee(Employee employee)
    {
        // Add Quotess to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Employee (BirthDate,FirstName,LastName, HireDate,Gender) values ('{employee.BirthDate}','{employee.FirstName}','{employee.LastName}','{employee.HireDate}',{employee.Gender}) returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                employee.Id = id;
                return new Response<Employee>(employee);
            }
            catch (Exception ex)
            {
                return new Response<Employee>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
    public async Task<Response<Employee>> UpdateEmployee(Employee employee)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Employee SET BirthDate='{employee.BirthDate}',FirstName='{employee.FirstName}',LastName='{employee.LastName}', HireDate='{employee.HireDate}',Gender={employee.Gender} WHERE id = {employee.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            employee.Id = id;
            return new Response<Employee>(employee);   
            }
            catch (Exception ex)
            {
                return new Response<Employee>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }

}