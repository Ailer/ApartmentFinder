using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.WPF.ApartmentTable.Enums;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibApartmentFinder.WPF.ApartmentTable.ViewModels
{
    public interface IApartmentTableViewModel
    {
        #region - Properties

        ICollectionView ApartmentListView { get; set; }

        ObservableCollection<ApartmentEntity> ApartmentList { get; set; }

        string SearchedText { get; set; }

        ApartmenTableSearchTypes ApartmentTableSelectedSearch { get; set; }

        ApartmentEntity SelectedApartment { get; set; }

        IRenterTableViewModel RenterTableDataContext { get; set; }

        #region - ICommands

        /// <summary>
        /// Gets the save apartments.
        /// </summary>
        /// <value>
        /// The save apartments.
        /// </value>
        ICommand SaveApartments { get; }

        /// <summary>
        /// Gets the reload apartments.
        /// </summary>
        /// <value>
        /// The reload apartments.
        /// </value>
        ICommand ReloadApartments { get; }

        /// <summary>
        /// Gets the search apartments.
        /// </summary>
        /// <value>
        /// The search apartments.
        /// </value>
        ICommand SearchApartments { get; }

        /// <summary>
        /// Gets the delete apartment.
        /// </summary>
        /// <value>
        /// The delete apartment.
        /// </value>
        ICommand DeleteApartment { get; }

        /// <summary>
        /// Gets the reset search.
        /// </summary>
        /// <value>
        /// The reset search.
        /// </value>
        ICommand ResetApartmentsSearch { get; }

        /// <summary>
        /// Gets the open in browser.
        /// </summary>
        /// <value>
        /// The open in browser.
        /// </value>
        ICommand OpenInBrowser { get; }

        /// <summary>
        /// Gets the go to renter.
        /// </summary>
        /// <value>
        /// The go to renter.
        /// </value>
        ICommand GoToRenter { get; }

        #endregion
        #endregion
    }
}
