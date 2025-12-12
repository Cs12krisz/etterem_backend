using etterem_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace etterem_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermekController : ControllerBase
    {
        private readonly ITermek _termek;

        public TermekController(ITermek termek)
        {
            _termek = termek;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllData()
        {
            var requesResult = await _termek.GetAll();
            if (requesResult != null)
            {
                return Ok(requesResult);
            }

            return BadRequest(requesResult);
        }
    }
}
