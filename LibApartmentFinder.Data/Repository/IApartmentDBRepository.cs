using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Repository
{
    public interface IApartmentDBRepository
    {
        void DeleteApartment(int apartmentId);

        void SaveApartments(IList<ApartmentEntity> apartments);

        void SaveRenters(IList<RenterEntity> renters);

        void DeleteRenter(int renterId);
    }
}
