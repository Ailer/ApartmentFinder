using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.RatingList.Services
{
    public class RatingListService : IRatingListService
    {
        #region - Private
        #region - Vars

        private IApartmentDBProvider _apartmentDBProvider;

        #endregion
        #endregion

        #region - Public
        #region - Ctors

        [InjectionConstructor]
        public RatingListService(IApartmentDBProvider apartmentDBProvider)
        {
            Guard.ArgumentNotNull(apartmentDBProvider, "apartmentDBProvider");

            this._apartmentDBProvider = apartmentDBProvider;
        }
        #endregion

        #region - Functions

        /// <summary>
        /// Gets the ratings.
        /// </summary>
        /// <returns></returns>
        public IList<RatingEntity> GetRatings()
        {
            return this._apartmentDBProvider.GetRatings();
        }
        #endregion
        #endregion
    }
}
