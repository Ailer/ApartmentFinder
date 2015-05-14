using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using LibApartmentFinder.Data.Repository;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.ApartmentTable.Services
{
    public class ApartmentTableService : IApartmentTableService
    {
        #region - Private
        #region - Vars

        private IApartmentDBProvider _apartmentDBProvider;
        private IApartmentDBRepository _apartmentDBRepository;

        #endregion
        #endregion

        #region - Public
        #region - Ctors

        public ApartmentTableService(IApartmentDBProvider apartmentDBProvider, IApartmentDBRepository apartmentDBRepository)
        {
            Guard.ArgumentNotNull(apartmentDBProvider, "apartmentDBProvider");
            Guard.ArgumentNotNull(apartmentDBRepository, "apartmentDBRepository");

            this._apartmentDBProvider = apartmentDBProvider;
            this._apartmentDBRepository = apartmentDBRepository;
        }
        #endregion

        #region - Functions

        public IList<ApartmentEntity> GetApartments()
        {
            return this._apartmentDBProvider.GetApartments();
        }

        public void SaveApartments(IList<ApartmentEntity> apartments)
        {
            throw new NotImplementedException();
        }

        public void DeleteApartment(int apartmentId)
        {
            this._apartmentDBRepository.DeleteApartment(apartmentId);
        }
        #endregion
        #endregion
    }
}
