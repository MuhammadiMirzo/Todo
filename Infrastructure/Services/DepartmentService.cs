using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services;

public class DepartmentService
{
     private DataContext.DataContext _context;
    public DepartmentService(DataContext.DataContext context)
    {
        _context = context;
    }


    public async Task<Response<List<GetDepartmentsDto>>> GetDepartmentDto()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetDepartmentsDto>($"SELECT D.Id, D.Name, E.Id as {"ManagerId"}, CONCAT(E.FirstName, ' ', E.LastName) AS {"ManagerFullName"} FROM department AS D INNER JOIN Department_employee AS O ON O.DepartmentId = D.Id INNER JOIN employee AS E ON O.EmployeeId = E.Id;");
                return new Response<List<GetDepartmentsDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetDepartmentsDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<List<GetDepartmentsDto>>> GetDepartmentDtoById(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetDepartmentsDto>($"SELECT D.Id, D.Name, E.Id as {"ManagerId"}, CONCAT(E.FirstName, ' ', E.LastName) AS {"ManagerFullName"} FROM department AS D INNER JOIN Department_employee AS O ON O.DepartmentId = D.Id INNER JOIN employee AS E ON O.EmployeeId = E.Id where D.Id = {id};");
                return new Response<List<GetDepartmentsDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetDepartmentsDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Department>> AddDepartment(Department department)
    {
        // Add Quotess to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Department (Name) values ('{department.Name}') returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                department.Id = id;
                return new Response<Department>(department);
            }
            catch (Exception ex)
            {
                return new Response<Department>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
    public async Task<Response<Department>> UpdateDepartment(Department department)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Department SET Name='{department.Name}' WHERE id = {department.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            department.Id = id;
            return new Response<Department>(department);   
            }
            catch (Exception ex)
            {
                return new Response<Department>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }

}