using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.StateList.Services
{
    public class StateListService : IStateListService
    {
        #region - Private

        private IApartmentDBProvider _apartmentDBProvider;

        #endregion

        #region - Public
        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="StateListService"/> class.
        /// </summary>
        /// <param name="apartmentDBProvider">The apartment database provider.</param>
        [InjectionConstructor]
        public StateListService(IApartmentDBProvider apartmentDBProvider)
        {
            Guard.ArgumentNotNull(apartmentDBProvider, "apartmentDBProvider");

            this._apartmentDBProvider = apartmentDBProvider;
        }
        #endregion

        #region - Functions

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        public IList<StateEntity> GetStates()
        {
            return this._apartmentDBProvider.GetStates();
        }
        #endregion
        #endregion
    }
}
