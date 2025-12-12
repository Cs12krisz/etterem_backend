using etterem_backend.Models;
using etterem_backend.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace etterem_backend.Services.Etterem
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

        public async Task<object> GetAllTermekNameAndPrice()
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
    }
}
