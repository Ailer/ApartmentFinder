using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.ApartmentTable.Enums;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using LibApartmentFinder.WPF.RenterTable.Enums;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApartmentFinder.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        #region - Private
        #region - Vars

        private ApartmentTableViewModel _apartmentTableDataContext;
        private RenterTableViewModel _renterTableDataContext;
        private const int RenterTabIndex = 1;
        private const int ApartmentTabIndex = 0;
        private int _selectedTab;

        #endregion

        #region - Functions

        private void Init()
        {
            this.SelectedTab = 0;
            this.ApartmentTableDataContext.OnGoToRenter += this.OnGoToRenter;
            this.RenterTableDataContext.OnShowApartments += RenterTableDataContext_OnShowApartments;
        }

        private void RenterTableDataContext_OnShowApartments(RenterEntity renter)
        {
            if (renter != null)
            {
                this.SelectedTab = ApartmentTabIndex;
                this.ApartmentTableDataContext.ApartmentTableSelectedSearch = ApartmenTableSearchTypes.Renter;
                this.ApartmentTableDataContext.SearchedText = renter.Name;
                this.ApartmentTableDataContext.SearchApartments.Execute(null);
                
            }
            else
            {
                MessageBox.Show("No renter");
            }
        }

        private void OnGoToRenter(RenterEntity renter)
        {
            if (renter != null) 
            {
                this.RenterTableDataContext.RenterTableSelectedSearch = RenterTableSearchTypes.Name;
                this.RenterTableDataContext.SearchedText = renter.Name;
                this.RenterTableDataContext.SearchRenter.Execute(null);
                this.SelectedTab = RenterTabIndex; 
            }
            else
            {
                MessageBox.Show("No renter");
            }
        }
        #endregion
        #endregion

        #region - Public
        #region - Properties

        public int SelectedTab
        {
            get
            {
                return this._selectedTab;
            }
            set
            {
                if (this._selectedTab != value)
                {
                    this._selectedTab = value;
                    base.OnPropertyChanged(() => this.SelectedTab);
                }
            }
        }

        /// <summary>
        /// Gets or sets the apartment table data context.
        /// </summary>
        /// <value>
        /// The apartment table data context.
        /// </value>
        public ApartmentTableViewModel ApartmentTableDataContext
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
        public RenterTableViewModel RenterTableDataContext
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
        public MainWindowViewModel(ApartmentTableViewModel apartmentTableDataContext, RenterTableViewModel renterTableDataContext)
        {
            Guard.ArgumentNotNull(apartmentTableDataContext, "apartmentTableDataContext");
            Guard.ArgumentNotNull(renterTableDataContext, "renterTableDataContext");

            this.ApartmentTableDataContext = apartmentTableDataContext;
            this.RenterTableDataContext = renterTableDataContext;
            this.Init();
        }


        public MainWindowViewModel()
        {
            this.ApartmentTableDataContext = ServiceLocator.Current.GetInstance<ApartmentTableViewModel>();
            this.RenterTableDataContext = ServiceLocator.Current.GetInstance<RenterTableViewModel>();
            this.Init();
        }
        #endregion
        #endregion
    }
}
