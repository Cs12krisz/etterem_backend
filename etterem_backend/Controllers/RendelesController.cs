using etterem_backend.Models.Dtos;
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
            var requesResult = await _rendeles.GetAll();
            if (requesResult != null)
            {
                return Ok(requesResult);
            }

            return BadRequest(requesResult);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveData(int id)
        {
            var responseResult = await _rendeles.Delete(id);
            var response = responseResult as ResponseDto;
            if (response.Result != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPut]
        public async Task<ActionResult> SetData(UpdateRendelesDto updateRendelesDto)
        {
            var requesResult = await _rendeles.Update(updateRendelesDto);
            var response = requesResult as ResponseDto;
            if (updateRendelesDto != null)
            {

                if (response.Result != null)
                {
                    return StatusCode(201, response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
           

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult> SetData(AddRendelesDto addRendelesDto)
        {
            var requesResult = await _rendeles.Post(addRendelesDto);
            if (requesResult != null)
            {
                return StatusCode(201, requesResult);
            }

            return BadRequest(requesResult);
        }


    }
}
