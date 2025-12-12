using etterem_backend.Models.Dtos;
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

        [HttpPost]
        public async Task<ActionResult> SetData(AddTermekDto addTermekDto)
        {
            var requestResult = await _termek.Post(addTermekDto);
            var request = requestResult as ResponseDto;

            if (request.Result != null) 
            {
                return StatusCode(201, request);
            }

            return BadRequest(request);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateData(UpdateTermekDto updateTermekDto)
        {
            var requestResult = await _termek.Update(updateTermekDto);
            var request = requestResult as ResponseDto;

            if (request.Result != null)
            {
                return StatusCode(201, request);
            }

            return NotFound(request);
        }
    }
}
