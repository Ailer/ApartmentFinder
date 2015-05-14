using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using LibApartmentFinder.Data.Repository;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.RenterTable.Services
{
    public class RenterTableService : IRenterTableService
    {
        #region - Private
        #region - Vars

        private IApartmentDBProvider _apartmentDBProvider;
        private IApartmentDBRepository _apartmentDBRepository;

        #endregion
        #endregion

        #region - Public
        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterTableService"/> class.
        /// </summary>
        /// <param name="apartmentDBProvider">The apartment database provider.</param>
        /// <param name="apartmentDBRepository">The apartment database repository.</param>
        [InjectionConstructor]
        public RenterTableService(IApartmentDBProvider apartmentDBProvider, IApartmentDBRepository apartmentDBRepository)
        {
            Guard.ArgumentNotNull(apartmentDBProvider, "apartmentDBProvider");
            Guard.ArgumentNotNull(apartmentDBRepository, "apartmentDBRepository");

            this._apartmentDBProvider = apartmentDBProvider;
            this._apartmentDBRepository = apartmentDBRepository;
        }
        #endregion

        #region - Functions

        /// <summary>
        /// Gets the renters.
        /// </summary>
        /// <returns></returns>
        public IList<RenterEntity> GetRenters()
        {
            return this._apartmentDBProvider.GetRenters();
        }

        /// <summary>
        /// Saves the renters.
        /// </summary>
        /// <param name="renters">The renters.</param>
        public void SaveRenters(IList<RenterEntity> renters)
        {
            Guard.ArgumentNotNull(renters, "renters");

            this._apartmentDBRepository.SaveRenters(renters);
        }

        /// <summary>
        /// Deletes the renter.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        public void DeleteRenter(int renterId)
        {
            this._apartmentDBRepository.DeleteRenter(renterId);
        }

        public bool CheckIfRenterHasApartments(int renterId)
        {
            return this._apartmentDBProvider.CheckIfRenterHasApartments(renterId);
        }
        #endregion
        #endregion
    }
}
