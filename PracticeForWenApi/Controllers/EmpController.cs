using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeForWebApi.Models;
using PracticeForWebApi.Repositories.Interfaces;

namespace PracticeForWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
            private readonly IEmpRepo empRepo;
            public EmpController(IEmpRepo _empRepo)
            {
                this.empRepo = _empRepo;
            }
            [HttpPost]
            public async Task<IActionResult> AddEmp(EmployeeModel employeeModel)
            {
                try
                {
                    var result = await empRepo.AddEmp(employeeModel);

                    if (result == 0)
                    {
                        return StatusCode(409, "The request could not be processed because of conflict in the request");
                    }
                    else
                    {
                        return StatusCode(200, string.Format("Record Inserted Successfuly with compnay Id {0}", result));
                    }
                }
                catch (Exception ex)
                {
                    //log error
                    return StatusCode(500, ex.Message);
                }
            }

        [HttpPut]
        public async Task<IActionResult> UpdateEmp(EmployeeModel employeeModel)
        {
            try
            {
                var result = await empRepo.UpdateEmp(employeeModel);

                if (result == 0)
                {
                    return StatusCode(409, "The request could not be processed because of conflict in the request");
                }
                else
                {
                    return StatusCode(200, string.Format("Record Updated Successfuly with compnay Id {0}", result));
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}

