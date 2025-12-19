using etterem_backend.Models.Dtos;

namespace etterem_backend.Services
{
    public interface IRendeles
    {
        Task<object> GetAll();
        Task<object> Post(AddRendelesDto addRendelesDto);
        Task<object> Delete(int id);
        Task<object> Update(UpdateRendelesDto updateRendelesDto);
        Task<object> KartyasRendelesek();
        Task<object> RendelesTetelek();
        Task<object> RendelesTetelekSorbaRendezve();
        Task<object> GetKolaRendelesek();
        Task<object> GetRendelesekTetelSzama();
        Task<object> GetKettesRendelesOsszErteket();

    }
}
