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

        public async Task<object> GetKettesRendelesOsszErteket()
        {
            try
            {
                var kettesrendeloOsszErtek = _context.Rendelestetels
                .Include(r => r.Rendeles)
                .Include(r => r.Termek)
                .Where(r => r.Rendeles.RendelesId == 2)
                .Sum(r => r.Termek.Ar);

                _responseDto.Messsage = "Sikeres";
                _responseDto.Result = kettesrendeloOsszErtek;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Messsage = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
            
        }

        public async Task<object> GetKolaRendelesek()
        {
            try
            {
                var kolaRendelesek = await _context.Rendelestetels
                    .Include(r => r.Rendeles)
                    .Include(r => r.Termek)
                    .Where(r => r.Termek.TermekNev == "Kóla")
                    .Select(r => r.Rendeles)
                    .ToArrayAsync();

                if (kolaRendelesek != null)
                {
                    _responseDto.Result = kolaRendelesek;
                    _responseDto.Messsage = "Sikeres";
                    return _responseDto;
                }

                _responseDto.Result = null;
                _responseDto.Messsage = "Sikertelen";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Result = null;
                _responseDto.Messsage = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> GetRendelesekTetelSzama()
        {
            try
            {
                var rendelesekTetelszama = await _context.Rendeles.Select(r => new { Rendeles = r, TetelSzama = r.Rendelestetels.Count}).ToArrayAsync();

                _responseDto.Messsage = "Sikeres";
                _responseDto.Result = rendelesekTetelszama;
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
                var kartyasRendelesek = await _context.Rendeles.Where(r => r.FizetesMod == "Kártya").ToArrayAsync();
                
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
                _responseDto.Result = null;
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

        public async Task<object> RendelesTetelek()
        {
            try {

                var tetelek = _context.Rendelestetels.Include(t => t.Termek).Select(t =>  new { t.RendelesId, t.Termek.TermekNev, t.Termek.Ar } );

                if (tetelek != null)
                {
                    _responseDto.Result = tetelek;
                    _responseDto.Messsage = "Sikeres lekérdezés";
                    return _responseDto;
                }

                _responseDto.Result = null;
                _responseDto.Messsage = "Nincsen rendelés tételek";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Result = null;
                _responseDto.Messsage = ex.Message;
                return _responseDto;
            }

        }

        public async Task<object> RendelesTetelekSorbaRendezve()
        {
            try
            {
                var rendelesTetelekSorbaRendezve = _context.Rendeles.Include(r => r.Rendelestetels).OrderBy(r => r.RendelesId);

                if (rendelesTetelekSorbaRendezve != null)
                {
                    _responseDto.Result = rendelesTetelekSorbaRendezve;
                    _responseDto.Messsage = "Sikeres lekérdezés";
                    return _responseDto;
                }
                _responseDto.Result = null;
                _responseDto.Messsage = "Nincsen tételek";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Result = null;
                _responseDto.Messsage = ex.Message;
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
