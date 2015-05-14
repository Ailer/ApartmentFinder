using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibApartmentFinder.Data.EntityFramework;
using System.Collections.ObjectModel;

namespace LibApartmentFinder.WPF.RatingList.ViewModels
{
    public interface IRatingListViewModel
    {
        RatingEntity SelectedRating { get; }

        ObservableCollection<RatingEntity> RatingList { get; }
    }
}
