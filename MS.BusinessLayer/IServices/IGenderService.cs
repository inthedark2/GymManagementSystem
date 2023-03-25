using MS.DataLayer.Entities;

namespace MS.BusinessLayer.IServices
{
    public interface IGenderService
    {
        Gender GetGenderById(int id);
        void AddGenders();
    }
}
