namespace WebApi.Controllers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]

public class DepartmentController:ControllerBase
{
 private DepartmentService _departmentService;
 public DepartmentController(DepartmentService departmentService)
 {
    _departmentService = departmentService;
 }

 [HttpGet("GetDepartmentsDto")]
 public async Task<Response<List<GetDepartmentsDto>>> GetDepartmentDto()
 {
   var result =  await _departmentService.GetDepartmentDto();
   return result;
 }
 [HttpGet("GetDepartmentsDtoById")]
 public async Task<Response<List<GetDepartmentsDto>>> GetDepartmentDtoById(int id)
 {
   var result =  await _departmentService.GetDepartmentDtoById(id);
   return result;
 }
 

 [HttpPost("AddDepartments")]
public async Task<Response<Department>>  AddDepartments(Department department)
{
   var result =  await _departmentService.AddDepartment(department);
   return result;
}

[HttpPut("UpdateDepartment")]
public async Task<Response<Department>> UpdateDepartments(Department department)
{
   var res = await _departmentService.UpdateDepartment(department);
   return res;
}


}
