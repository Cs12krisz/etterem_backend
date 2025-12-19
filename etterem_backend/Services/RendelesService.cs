using etterem_backend.Models;
using etterem_backend.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace etterem_backend.Services
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

        public async Task<object> Delete(int id)
        {
            try
            {
                var torlendoRendeles = await _context.Rendeles.FirstOrDefaultAsync(r => r.RendelesId == id);

                if (torlendoRendeles != null)
                {
                    _context.Rendeles.Remove(torlendoRendeles);
                    await _context.SaveChangesAsync();

                    _responseDto.Messsage = "Sikeres törlés";
                    _responseDto.Result = torlendoRendeles;
                    return _responseDto;
                }

                _responseDto.Messsage = "Nincsen ilyen id";
                _responseDto.Result = null;
                return _responseDto;


            }
            catch (Exception ex)
            {
                _responseDto.Messsage = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
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

        public async Task<object> KartyasRendelesek()
        {
            try
            {
                var kartyasRendelesek = _context.Rendeles.Where(r => r.FizetesMod == "Kártya").ToArray();
                
                if (kartyasRendelesek != null)
                {
                    _responseDto.Result = kartyasRendelesek;
                    _responseDto.Messsage = "Sikeres lekérdezés";
                    return _responseDto;

                }
                
                _responseDto.Result = null;
                _responseDto.Messsage = "Nincsen kártyás rendelések";
                return _responseDto;
                
            }
            catch (Exception ex)
            {
                _responseDto.Result= null;
                _responseDto.Messsage = ex.Message;
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

        public async Task<object> Update(UpdateRendelesDto updateRendelesDto)
        {
            try
            {
                var modositandoRendeles = await _context.Rendeles.FirstOrDefaultAsync(r => r.RendelesId == updateRendelesDto.RendelesId);

                if (modositandoRendeles != null)
                {
                    modositandoRendeles.FizetesMod = updateRendelesDto.FizetesMod;
                    modositandoRendeles.AsztalSzam = updateRendelesDto.AsztalSzam;
                    _context.Update(modositandoRendeles);
                    await _context.SaveChangesAsync();

                    _responseDto.Messsage = "Sikeres frissítés";
                    _responseDto.Result = modositandoRendeles;
                    return _responseDto;
                }

                _responseDto.Messsage = "Nincsen ilyen id";
                _responseDto.Result = null;
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
