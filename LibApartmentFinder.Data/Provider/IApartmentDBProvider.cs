using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApartmentDBProvider
    {
        #region - Functions

        /// <summary>
        /// Gets the apartments.
        /// </summary>
        /// <returns></returns>
        IList<ApartmentEntity> GetApartments();

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        IList<StateEntity> GetStates();

        /// <summary>
        /// Gets the renters.
        /// </summary>
        /// <returns></returns>
        IList<RenterEntity> GetRenters();

        /// <summary>
        /// Gets the ratings.
        /// </summary>
        /// <returns></returns>
        IList<RatingEntity> GetRatings();

        /// <summary>
        /// Gets the apartment kinds.
        /// </summary>
        /// <returns></returns>
        IList<ApartmentKindEntity> GetApartmentKinds();

        /// <summary>
        /// Checks if renter rent apartment.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <returns></returns>
        bool CheckIfRenterHasApartments(int renterId);

        #endregion
    }
}
