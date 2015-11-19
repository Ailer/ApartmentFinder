using LibApartmentFinder.Data.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Provider
{
    public abstract class ApartmentDBProviderBase
    {
        #region - Private
        #region - Vars

        private string _apartmentDBConnectionString;
        private static ApartmentDBDataContext _apartmentDBDataContext;

        #endregion
        #endregion

        #region - Public
        #region - Properties

        /// <summary>
        /// Gets or sets the apartment database connection string.
        /// </summary>
        /// <value>
        /// The apartment database connection string.
        /// </value>
        public virtual string ApartmentDBConnectionString
        {
            get
            {
                return this._apartmentDBConnectionString;
            }
            set
            {
                if (this._apartmentDBConnectionString != value)
                {
                    this._apartmentDBConnectionString = value;
                }
            }
        }

        #endregion

        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentDBProviderBase"/> class.
        /// </summary>
        /// <param name="apartmentDBConnectionString">The apartment database connection string.</param>
        /// <exception cref="System.ArgumentNullException">apartmentDBConnectionString</exception>
        [InjectionConstructor]
        public ApartmentDBProviderBase(string apartmentDBConnectionString)
        {
            if (string.IsNullOrWhiteSpace(apartmentDBConnectionString))
            {
                throw new ArgumentNullException("apartmentDBConnectionString");
            }

            this._apartmentDBConnectionString = apartmentDBConnectionString;
        }
        #endregion

        #region - Functions

        /// <summary>
        /// Creates the data context.
        /// </summary>
        /// <returns></returns>
        public virtual ApartmentDBDataContext GetDataContext()
        {
            if (_apartmentDBDataContext == null)
            {
                _apartmentDBDataContext = new ApartmentDBDataContext(this._apartmentDBConnectionString);
               
            }

            return _apartmentDBDataContext;
        }
        #endregion
        #endregion
    }
}
