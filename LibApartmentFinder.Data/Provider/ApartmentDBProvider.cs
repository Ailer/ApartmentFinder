using LibApartmentFinder.Data.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Provider
{
    public class ApartmentDBProvider : ApartmentDBProviderBase, IApartmentDBProvider
    {
        #region - Public
        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentDBProvider"/> class.
        /// </summary>
        /// <param name="apartmentDBConnectionString">The apartment database connection string.</param>
        [InjectionConstructor]
        public ApartmentDBProvider(string apartmentDBConnectionString)
            : base(apartmentDBConnectionString)
        { }
        #endregion

        #region - Functions

        /// <summary>
        /// Gets the apartments.
        /// </summary>
        /// <returns></returns>
        public IList<ApartmentEntity> GetApartments()
        {
            // TODO: Andere Lösung für dispose Problem?
            ApartmentDBDataContext dataContext = base.GetDataContext();
            IList<ApartmentEntity> tmp = dataContext.EF_Apartment.ToList();
            dataContext.Refresh(RefreshMode.StoreWins, tmp);

            return tmp;
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        public IList<StateEntity> GetStates()
        {
            ApartmentDBDataContext dataContext = base.GetDataContext();
            IList<StateEntity> tmp = dataContext.EF_State.ToList();
            dataContext.Refresh(RefreshMode.StoreWins, tmp);

            return tmp;
        }

        /// <summary>
        /// Gets the renters.
        /// </summary>
        /// <returns></returns>
        public IList<RenterEntity> GetRenters()
        {
            ApartmentDBDataContext dataContext = base.GetDataContext();
            IList<RenterEntity> tmp = dataContext.EF_Renter.ToList();
            dataContext.Refresh(RefreshMode.StoreWins, tmp);

            return base.GetDataContext().EF_Renter.ToList();
        }

        /// <summary>
        /// Gets the ratings.
        /// </summary>
        /// <returns></returns>
        public IList<RatingEntity> GetRatings()
        {
            ApartmentDBDataContext dataContext = base.GetDataContext();
            IList<RatingEntity> tmp = dataContext.EF_Rating.ToList();
            dataContext.Refresh(RefreshMode.StoreWins, tmp);

            return base.GetDataContext().EF_Rating.ToList();
        }

        /// <summary>
        /// Gets the apartment kinds.
        /// </summary>
        /// <returns></returns>
        public IList<ApartmentKindEntity> GetApartmentKinds()
        {
            ApartmentDBDataContext dataContext = base.GetDataContext();
            IList<ApartmentKindEntity> tmp = dataContext.EF_ApartmentKind.ToList();
            dataContext.Refresh(RefreshMode.StoreWins, tmp);

            return base.GetDataContext().EF_ApartmentKind.ToList();
        }

        /// <summary>
        /// Checks if renter rent apartment.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <returns></returns>
        public bool CheckIfRenterHasApartments(int renterId)
        {
            return base.GetDataContext().EF_Apartment.FirstOrDefault(f => f.RenterID == renterId) != null;
        }
        #endregion
        #endregion
    }
}
