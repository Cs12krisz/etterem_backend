using etterem_backend.Models;
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

        [HttpGet("KartyasRendelesek")]
        public async Task<ActionResult> GetAllKartyasFizetes()
        {
            var requestResult = await _rendeles.KartyasRendelesek();
            if (requestResult != null)
            {
                return Ok(requestResult);
            }

            return NotFound(requestResult);
        }

        [HttpGet("RendelesTetelek")]
        public async Task<ActionResult> GetRendelesTetelek()
        {
            var requestResult = await _rendeles.RendelesTetelek();
            if (requestResult != null)
            {
                return Ok(requestResult);
            }

            return NotFound(requestResult);
        }

        [HttpGet("RendelesTetelekSorbaRendezve")]
        public async Task<ActionResult> GetRendelesTetelekSorban()
        {
            var requestResult = await _rendeles.RendelesTetelekSorbaRendezve();
            if (requestResult != null)
            {
                return Ok(requestResult);
            }

            return NotFound(requestResult);
        }

        [HttpGet("KolaRendelesek")]
        public async Task<ActionResult> GetKolaRendelesek()
        {
            var requesResult = await _rendeles.GetKolaRendelesek();
            if (requesResult != null)
            {
                return Ok(requesResult);
            }

            return NotFound(requesResult);
        }

        [HttpGet("RendelesekTetelszam")]
        public async Task<ActionResult> GetRendelesTeleSzam()
        {
            var requesResult = await _rendeles.GetRendelesekTetelSzama();
            if (requesResult != null)
            {
                return Ok(requesResult);
            }

            return NotFound(requesResult);
        }

        [HttpGet("KettesRendelesOsszege")]
        public async Task<ActionResult> KettesRendelesOsszege()
        {
            var requesResult = await _rendeles.GetKettesRendelesOsszErteket();
            if (requesResult != null)
            {
                return Ok(requesResult);
            }

            return NotFound(requesResult);
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
