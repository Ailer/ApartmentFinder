using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.ApartmentKindList.ViewModels
{
    public interface IApartmentKindListViewModel
    {
        /// <summary>
        /// Gets the kind of the selected apartment.
        /// </summary>
        /// <value>
        /// The kind of the selected apartment.
        /// </value>
        ApartmentKindEntity SelectedApartmentKind { get; }

        /// <summary>
        /// Gets the apartment kind list.
        /// </summary>
        /// <value>
        /// The apartment kind list.
        /// </value>
        ObservableCollection<ApartmentKindEntity> ApartmentKindList { get; }
    }
}
