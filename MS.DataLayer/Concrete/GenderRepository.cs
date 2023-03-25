using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.DataLayer.Concrete
{
    public class GenderRepository : IGenderRepository
    {
        private readonly ManagmentSystemContext _context;
        public GenderRepository(ManagmentSystemContext context)
        {
            this._context = context;
        }
        public void AddGenders()
        {
            int genderCount = _context.Genders.Count();
            if (genderCount==0)
            {
                Gender male = new Gender { Name = "Male" };
                Gender female = new Gender { Name = "Female" };
                _context.Genders.Add(male);
                _context.Genders.Add(female);
                _context.SaveChanges();
            }
        }

        public Gender GetGenderById(int id)
        {
            return _context.Genders.SingleOrDefault(g => g.Id == id);
        }
    }
}
