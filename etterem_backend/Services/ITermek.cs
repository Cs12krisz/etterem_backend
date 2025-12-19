using etterem_backend.Models.Dtos;

namespace etterem_backend.Services
{
    public interface ITermek
    {
        Task<object> GetAll();
        Task<object> Post(AddTermekDto addTermekDto);
        Task<object> Delete(int id);
        Task<object> Update(UpdateTermekDto updateTermekDto);
        Task<object> GetNameAndPrice();
        Task<object> GetEladottTermekekLegalabbEgyszer();



    }
}
