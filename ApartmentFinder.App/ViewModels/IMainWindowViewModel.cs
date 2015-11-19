using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentFinder.App.ViewModels
{
    public interface IMainWindowViewModel
    {
        #region - Properties

        ApartmentTableViewModel ApartmentTableDataContext { get; }

        RenterTableViewModel RenterTableDataContext { get; }

        #endregion
    }
}
