namespace WebApi.Controllers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]

public class EmployeeController:ControllerBase
{
 private EmployeeService _employeeService;
 public EmployeeController(EmployeeService employeeService)
 {
    _employeeService = employeeService;
 }

 [HttpGet("GetEmployeesDto")]
 public async Task<Response<List<GetEmployeeDto>>> GetEmployeeDto()
 {
   var result =  await _employeeService.GetEmployeeDto();
   return result;
 }
 [HttpGet("GetEmployeesDtoById")]
 public async Task<Response<List<GetEmployeeDto>>> GetEmployeeDtoById(int id)
 {
   var result =  await _employeeService.GetEmployeeDtoById(id);
   return result;
 }
 

 [HttpPost("AddEmployees")]
public async Task<Response<Employee>>  AddEmployees(Employee Employee)
{
   var result =  await _employeeService.AddEmployee(Employee);
   return result;
}

[HttpPut("UpdateEmployee")]
public async Task<Response<Employee>> UpdateEmployees(Employee Employee)
{
   var res = await _employeeService.UpdateEmployee(Employee);
   return res;
}


}
