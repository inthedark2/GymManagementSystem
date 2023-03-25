using MS.BusinessLayer.IServices;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.Services
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderService(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public Gender GetGenderById(int id)
        {
            return _genderRepository.GetGenderById(id);
        }

        public void AddGenders()
        {
            _genderRepository.AddGenders();
        }
    }
}
