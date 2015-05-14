using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.ApartmentTable.Services
{
    public interface IApartmentTableService
    {
        IList<ApartmentEntity> GetApartments();

        void SaveApartments(IList<ApartmentEntity> apartments);

        void DeleteApartment(int apartmentId);
    }
}
