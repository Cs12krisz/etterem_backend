using etterem_backend.Models;
using etterem_backend.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace etterem_backend.Services.Etterem
{
    public class RendelesService : IRendeles
    {
        private readonly EtteremContext _context;
        private readonly ResponseDto _responseDto;

        public RendelesService(EtteremContext context, ResponseDto responseDto)
        {
            _context = context;
            _responseDto = responseDto;
        }

        public async Task<object> GetAll()
        {
            try
            {
                var rendelesek = await _context.Rendeles.ToArrayAsync();
                _responseDto.Messsage = "Sikeres lekérdezés";
                _responseDto.Result = rendelesek;
                return _responseDto;

            }
            catch (Exception ex)
            {
                _responseDto.Messsage = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }


        public async Task<object> Post(AddRendelesDto addRendelesDto)
        {
            try
            {
                var rendeles = new Rendele()
                {
                    AsztalSzam = addRendelesDto.AsztalSzam,
                    FizetesMod = addRendelesDto.FizetesMod
                };

                await _context.Rendeles.AddAsync(rendeles);
                await _context.SaveChangesAsync();

                _responseDto.Messsage = "Sikeres hozzáadás";
                _responseDto.Result = rendeles;
                return _responseDto;


            }
            catch (Exception ex)
            {
                _responseDto.Messsage = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }
    }
}
