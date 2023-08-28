using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddlewaresDemo.Exceptions;

namespace MiddlewaresDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (id ==1)
            {
                int a = 1;
                int b = a / 0;
            }
            else if(id == 2)
            {
                throw new NotFoundException("Not results found");
            }
            else
            {
                throw new BadRequestException("Bad Request");
            }
            return Ok();
        }
    }
}
