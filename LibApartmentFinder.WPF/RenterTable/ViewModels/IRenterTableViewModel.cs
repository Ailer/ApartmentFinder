using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.WPF.RenterTable.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibApartmentFinder.WPF.RenterTable.ViewModels
{
    public interface IRenterTableViewModel
    {
        #region - Properties

        ObservableCollection<RenterEntity> RenterList { get; set; }

        RenterEntity SelectedRenter { get; set; }

        string SearchedText { get; set; }

        RenterTableSearchTypes RenterTableSelectedSearch { get; set; }

        #region - ICommands

        /// <summary>
        /// Gets the save renters.
        /// </summary>
        /// <value>
        /// The save renters.
        /// </value>
        ICommand SaveRenters { get; }

        /// <summary>
        /// Gets the load renters.
        /// </summary>
        /// <value>
        /// The load renters.
        /// </value>
        ICommand LoadRenters { get; }

        /// <summary>
        /// Gets the delete renter.
        /// </summary>
        /// <value>
        /// The delete renter.
        /// </value>
        ICommand DeleteRenter { get; }

        /// <summary>
        /// Gets the search renter.
        /// </summary>
        /// <value>
        /// The search renter.
        /// </value>
        ICommand SearchRenter { get; }

        /// <summary>
        /// Gets the reset search.
        /// </summary>
        /// <value>
        /// The reset search.
        /// </value>
        ICommand ResetSearch { get; }

        /// <summary>
        /// Gets the contact renter.
        /// </summary>
        /// <value>
        /// The contact renter.
        /// </value>
        ICommand ContactRenter { get; }

        #endregion
        #endregion
    }
}
