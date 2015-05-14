using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.ApartmentKindList.Services
{
    public interface IApartmentKindListService
    {
        #region - Functions

        /// <summary>
        /// Gets the apartment kinds.
        /// </summary>
        /// <returns></returns>
        IList<ApartmentKindEntity> GetApartmentKinds();

        #endregion
    }
}
