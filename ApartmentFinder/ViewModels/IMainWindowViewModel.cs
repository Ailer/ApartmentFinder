using LibApartmentFinder.WPF.ApartmentKindList.ViewModels;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using LibApartmentFinder.WPF.RatingList.ViewModels;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using LibApartmentFinder.WPF.StateList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentFinder.App.ViewModels
{
    public interface IMainWindowViewModel
    {
        IApartmentKindListViewModel ApartmentKindListDataContext { get; }

        IApartmentTableViewModel ApartmentTableDataContext { get; }

        IRatingListViewModel RatingListDataContext { get; }

        IRenterTableViewModel RenterTableDataContext { get; }

        IStateListViewModel StateListDataContext { get; }
    }
}
