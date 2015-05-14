using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibApartmentFinder.Data.EntityFramework;
using System.Collections.ObjectModel;

namespace LibApartmentFinder.WPF.StateList.ViewModels
{
    public interface IStateListViewModel
    {
        StateEntity SelectedState { get; }
        ObservableCollection<StateEntity> StateList { get; }
    }
}
