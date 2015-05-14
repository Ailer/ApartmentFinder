using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentFinder.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region - Private
        #region - Vars

        private IApartmentTableViewModel _apartmentTableDataContext;
        private IRenterTableViewModel _renterTableDataContext;

        #endregion
        #endregion

        #region - Public
        #region - Properties

        /// <summary>
        /// Gets or sets the apartment table data context.
        /// </summary>
        /// <value>
        /// The apartment table data context.
        /// </value>
        public IApartmentTableViewModel ApartmentTableDataContext
        {
            get
            {
                return this._apartmentTableDataContext;
            }
            set
            {
                if (this._apartmentTableDataContext != value)
                {
                    this._apartmentTableDataContext = value;
                    base.OnPropertyChanged("ApartmentTableDataContext");
                }
            }
        }

        /// <summary>
        /// Gets or sets the renter table data context.
        /// </summary>
        /// <value>
        /// The renter table data context.
        /// </value>
        public IRenterTableViewModel RenterTableDataContext
        {
            get
            {
                return this._renterTableDataContext;
            }
            set
            {
                if (this._renterTableDataContext != value)
                {
                    this._renterTableDataContext = value;
                    base.OnPropertyChanged("RenterTableDataContext");
                }
            }
        }
        #endregion

        #region - Ctors

        [InjectionConstructor]
        public MainWindowViewModel(IApartmentTableViewModel apartmentTableDataContext, IRenterTableViewModel renterTableDataContext)
        {
            Guard.ArgumentNotNull(apartmentTableDataContext, "apartmentTableDataContext");
            Guard.ArgumentNotNull(renterTableDataContext, "renterTableDataContext");

            this.ApartmentTableDataContext = apartmentTableDataContext;
            this.RenterTableDataContext = renterTableDataContext;
        }


        public MainWindowViewModel()
        {
            this.ApartmentTableDataContext = ServiceLocator.Current.GetInstance<IApartmentTableViewModel>();
            this.RenterTableDataContext = ServiceLocator.Current.GetInstance<IRenterTableViewModel>();
        }
        #endregion
        #endregion
    }
}
