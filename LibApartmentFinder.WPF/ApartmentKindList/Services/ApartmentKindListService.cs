using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.ApartmentKindList.Services
{
    public class ApartmentKindListService : IApartmentKindListService
    {
        #region - Private
        #region - Vars

        private IApartmentDBProvider _apartmentDBProvider;

        #endregion
        #endregion

        #region - Public
        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentKindListService"/> class.
        /// </summary>
        /// <param name="apartmentDBProvider">The apartment database provider.</param>
        [InjectionConstructor]
        public ApartmentKindListService(IApartmentDBProvider apartmentDBProvider)
        {
            Guard.ArgumentNotNull(apartmentDBProvider, "apartmentDBProvider");

            this._apartmentDBProvider = apartmentDBProvider;
        }
        #endregion

        #region - Functions

        /// <summary>
        /// Gets the apartment kinds.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<ApartmentKindEntity> GetApartmentKinds()
        {
            return this._apartmentDBProvider.GetApartmentKinds();
        }
        #endregion
        #endregion
    }
}
