using etterem_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace etterem_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendelesController : ControllerBase
    {
        private readonly IRendeles _rendeles;

        public RendelesController(IRendeles rendeles)
        {
            _rendeles = rendeles;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllData()
        {
            var requesResult = await _rendeles.GetAllRendeles();
            if (requesResult != null)
            {
                return Ok(requesResult);
            }

            return BadRequest(requesResult);
        }


    }
}
