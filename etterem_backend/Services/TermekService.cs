using etterem_backend.Models;
using etterem_backend.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace etterem_backend.Services
{
    public class TermekService : ITermek
    {
        private readonly EtteremContext _context;
        private readonly ResponseDto _responseDto;

        public TermekService(EtteremContext context, ResponseDto responseDto)
        {
            _context = context;
            _responseDto = responseDto;
        }

        public async Task<object> Delete(int id)
        {
            try
            {
                var torlendoTermek = await _context.Termeks.FirstOrDefaultAsync(t => t.TermekId == id);

                if (torlendoTermek != null)
                {
                    _context.Termeks.Remove(torlendoTermek);
                    await _context.SaveChangesAsync();
                    _responseDto.Messsage = "Sikeres törlés";
                    _responseDto.Result = torlendoTermek;
                    return _responseDto;

                }

                _responseDto.Messsage = "Nincs ilyen Id";
                _responseDto.Result = null;
                return _responseDto;


            }
            catch (Exception ex)
            {
                _responseDto.Result = null;
                _responseDto.Messsage = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> GetAll()
        {
            try
            {
                var Termekek = await _context.Termeks.ToArrayAsync();
                _responseDto.Messsage = "Sikeres lekérdezés";
                _responseDto.Result = Termekek;
                return _responseDto;

            }
            catch (Exception ex)
            {
                _responseDto.Messsage = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }

        public async Task<object> GetNameAndPrice()
        {
            try
            {
                var termekNevEsErtek = _context.Termeks.Select(t => new { t.TermekNev, t.Ar });
                _responseDto.Messsage = "Sikeres lelérdezés";
                _responseDto.Result = termekNevEsErtek;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Result = null;
                _responseDto.Messsage = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> Post(AddTermekDto addTermekDto)
        {
            try
            {
                if (addTermekDto != null)
                {
                    Termek ujTermek = new Termek()
                    {
                        TermekNev = addTermekDto.TermekNev,
                        Ar = addTermekDto.Ar
                    };

                    await _context.Termeks.AddAsync(ujTermek);
                    await _context.SaveChangesAsync();
                    _responseDto.Messsage = "Sikeres feltöltés";
                    _responseDto.Result = ujTermek;
                    return _responseDto;

                }

                _responseDto.Messsage = "Sikertelen feltöltés";
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


        public async Task<object> Update(UpdateTermekDto updateTermekDto)
        {
            try
            {
                var modositandoTermek = await _context.Termeks.FirstOrDefaultAsync(t => t.TermekId == updateTermekDto.TermekId);

                if (modositandoTermek != null)
                {
                    modositandoTermek.Ar = updateTermekDto.Ar;
                    modositandoTermek.TermekNev = updateTermekDto.TermekNev;
                    _context.Update(modositandoTermek);
                    await _context.SaveChangesAsync();
                    _responseDto.Messsage = "Sikeres törlés";
                    _responseDto.Result = modositandoTermek;
                    return _responseDto;

                }

                _responseDto.Messsage = "Nincs ilyen Id";
                _responseDto.Result = null;
                return _responseDto;


            }
            catch (Exception ex)
            {
                _responseDto.Result = null;
                _responseDto.Messsage = ex.Message;
                return _responseDto;
            }
        }
    }
}
