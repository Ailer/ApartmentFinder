using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.RenterTable.Services
{
    public interface IRenterTableService
    {
        #region - Functions

        /// <summary>
        /// Gets the renters.
        /// </summary>
        /// <returns></returns>
        IList<RenterEntity> GetRenters();

        /// <summary>
        /// Saves the renters.
        /// </summary>
        /// <param name="renters">The renters.</param>
        void SaveRenters(IList<RenterEntity> renters);

        /// <summary>
        /// Deletes the renter.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        void DeleteRenter(int renterId);

        /// <summary>
        /// Checks if renter has apartments.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <returns></returns>
        bool CheckIfRenterHasApartments(int renterId);

        #endregion
    }
}
