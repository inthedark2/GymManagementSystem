using MS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.DataLayer.Abstract
{
    public interface IGenderRepository
    {
        Gender GetGenderById(int id);
        void AddGenders();
    }
}
