using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.StateList.Services
{
    public interface IStateListService
    {
        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <returns></returns>
        IList<StateEntity> GetStates();
    }
}
