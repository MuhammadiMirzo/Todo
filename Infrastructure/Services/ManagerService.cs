namespace Infrastructure.Services;
    using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;

public class ManagerService
{
     private DataContext.DataContext _context;
    public ManagerService(DataContext.DataContext context)
    {
        _context = context;
    }


    public async Task<Response<List<GetManagerDto>>> GetManagerDto()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetManagerDto>($"SELECT E.Id as {"ManagerId"} ,CONCAT(E.FirstName, ' ', E.LastName) AS {"ManagerFullName"}, D.Id as {"DepartmentId"}, D.Name as {"DepartmentName"}, O.FromDate,O.ToDate FROM department AS D INNER JOIN Department_employee AS O ON O.DepartmentId = D.Id INNER JOIN employee AS E ON O.EmployeeId = E.Id;");
                return new Response<List<GetManagerDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetManagerDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

     public async Task<Response<DepartmentManager>> AddDepartmentManager(DepartmentManager employee)
    {
        // Add Quotess to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Department_employee (ManagerId,DepartmentId,FromDate) values ({employee.EmployeeId},{employee.DepartmentId},'{employee.FromDate}') returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                employee.EmployeeId = id;
                return new Response<DepartmentManager>(employee);
            }
            catch (Exception ex)
            {
                return new Response<DepartmentManager>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }

    
}

