namespace WebApi.Controllers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]

public class ManagerController:ControllerBase
{
 private ManagerService _managerService;
 public ManagerController(ManagerService managerService)
 {
    _managerService = managerService;
 }

 [HttpGet("GetManagersDto")]
 public async Task<Response<List<GetManagerDto>>> GetManagerDto()
 {
   var result =  await _managerService.GetManagerDto();
   return result;
 }
 [HttpPost("AddManagers")]
public async Task<Response<DepartmentManager>>  AddManagers(DepartmentManager manager)
{
   var result =  await _managerService.AddDepartmentManager(manager);
   return result;
}

}
