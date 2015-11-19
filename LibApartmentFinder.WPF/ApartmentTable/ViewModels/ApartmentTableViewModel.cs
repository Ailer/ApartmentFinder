using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.ApartmentKindList.ViewModels;
using LibApartmentFinder.WPF.ApartmentTable.Enums;
using LibApartmentFinder.WPF.ApartmentTable.Services;
using LibApartmentFinder.WPF.RatingList.ViewModels;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using LibApartmentFinder.WPF.StateList.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LibApartmentFinder.Infastructure.Helpers;
using System.Windows;
using Microsoft.Practices.ObjectBuilder2;
using System.Diagnostics;
using System.Net;

namespace LibApartmentFinder.WPF.ApartmentTable.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ApartmentTableViewModel : ViewModelBase, IApartmentTableViewModel
    {
        #region - Private
        #region - Vars

        private IApartmentTableService _apartmentTableService;
        private IApartmentKindListViewModel _apartmentKindListDataContext;
        private IRatingListViewModel _ratingListDataContext;
        private IRenterTableViewModel _renterTableDataContext;
        private IStateListViewModel _stateListDataContext;
        private ICollectionView _apartmentListView;
        private ObservableCollection<ApartmentEntity> _apartmentList;
        private ApartmentEntity _selectedApartment;
        private string _searchedText;
        private ApartmenTableSearchTypes _apartmentTableSelectedSearch;
        private ICommand _reloadApartments;
        private ICommand _searchApartments;
        private ICommand _goToRenter;
        private ICommand _deleteApartment;
        private ICommand _resetSearch;
        private ICommand _openInBrowser;
        private ICommand _saveApartments;

        #endregion

        #region - Functions

        private bool IsUrlValid(string url)
        {
            try
            {
                Uri adress = new Uri(url, UriKind.RelativeOrAbsolute);
                return adress.Host != null;
            }
            catch
            {
                return false;
            }
        }

        private void RaiseOnGoToRenter()
        {
            if (this.OnGoToRenter != null)
            {
                this.OnGoToRenter(this.SelectedApartment.Renter);
            }
        }
        #endregion
        #endregion

        #region - Protected
        #region - Functions

        /// <summary>
        /// Reloads the apartments execute.
        /// </summary>
        protected void ReloadApartmentsExecute()
        {
            this.ApartmentList = new ObservableCollection<ApartmentEntity>(this._apartmentTableService.GetApartments());
            this.ApartmentListView = CollectionViewSource.GetDefaultView(this.ApartmentList);
        }

        protected void DeleteApartmentExecute()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete the selected apartment?", "Delete Apartment", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this._apartmentTableService.DeleteApartment(this._selectedApartment.ApartmentID);
                this._apartmentList.Remove(this._selectedApartment);
            }
        }

        protected bool DeleteApartmentCanExecute()
        {
            return this._selectedApartment != null;
        }

        protected void SearchApartmentExecute()
        {
            this.ApartmentListView.Filter = this.ApartmentFilter;
        }


        protected bool ResetApartmentSearchCanExecute()
        {
            return this.ApartmentListView.Filter != null;
        }

        private void ResetApartmentSearchExecute()
        {
            this.ApartmentListView.Filter = null;
            this.SearchedText = null;
        }

        protected bool ApartmentFilter(object obj)
        {
            ApartmentEntity apartment = obj as ApartmentEntity;

            if (apartment == null)
            {
                throw new InvalidCastException("No RenterEntity");
            }

            switch (this.ApartmentTableSelectedSearch)
            {

                case ApartmenTableSearchTypes.PLZ:
                    return apartment.PLZ.ToLower().Contains(this.SearchedText.ToLower());
                case ApartmenTableSearchTypes.Place:
                    return apartment.Place.ToLower().Contains(this.SearchedText.ToLower());
                case ApartmenTableSearchTypes.Renter:
                    return apartment.Renter.Name.ToLower().Contains(this.SearchedText.ToLower());
                case ApartmenTableSearchTypes.State:
                    return apartment.State.Description.ToLower().Contains(this.SearchedText.ToLower());
                case ApartmenTableSearchTypes.Kind:
                    return apartment.ApartmentKind.Description.ToLower().Contains(this.SearchedText.ToLower());
                default:
                    return false;
            }
        }

        protected bool SearchApartmentsCanExecute()
        {
            return !string.IsNullOrWhiteSpace(this.SearchedText);
        }

        protected bool OpenInBrowserCanExecute()
        {
            if (this.SelectedApartment != null
                && !string.IsNullOrWhiteSpace(this.SelectedApartment.Source))
            {
                return this.IsUrlValid(this.SelectedApartment.Source);
            }

            return false;
            //return this.SelectedApartment != null;
        }

        protected void OpenInBrowserExecute()
        {
            try
            {
                Process.Start(this.SelectedApartment.Source);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open Address", "Open in browser");
            }
        }

        protected virtual bool GoToRenterCanExecute()
        {
            return this.SelectedApartment != null;
        }

        protected virtual void GoToRenterExecute()
        {
            this.RaiseOnGoToRenter();
        }

        /// <summary>
        /// Saves the apartments execute.
        /// </summary>
        protected void SaveApartmentsExecute()
        {
            try
            {
                this._apartmentTableService.SaveApartments(this.ApartmentList);
                MessageBox.Show("Apartments saved.", "Apartments");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected bool SaveApartmentsCanExecute()
        {
            //foreach (ApartmentEntity apartment in this.ApartmentList)
            //{
            //    if (!apartment.IsValid())
            //    {
            //        return false;
            //    }
            //}

            return ApartmentList.FirstOrDefault(f => f.EntityState == EntityState.Detached
                                                  || f.EntityState == EntityState.Modified
                                                  || f.EntityState == EntityState.Added) != null;
        }
        #endregion

        #region - EventHandler

        /// <summary>
        /// Apartments the table view model_ selected rating changed.
        /// </summary>
        protected virtual void ApartmentTableViewModel_SelectedRatingChanged()
        {
            this.SelectedApartment.Rating = this.RatingListDataContext.SelectedRating;
        }

        /// <summary>
        /// Renters the table data context_ selected renter changed.
        /// </summary>
        protected virtual void RenterTableDataContext_SelectedRenterChanged()
        {
            this.SelectedApartment.Renter = this.RenterTableDataContext.SelectedRenter;
        }

        /// <summary>
        /// States the list data context_ selected state changed.
        /// </summary>
        protected virtual void StateListDataContext_SelectedStateChanged()
        {
            this.SelectedApartment.State = this.StateListDataContext.SelectedState;
        }

        /// <summary>
        /// Apartments the kind list data context_ selected apartment kind changed.
        /// </summary>
        protected virtual void ApartmentKindListDataContext_SelectedApartmentKindChanged()
        {
            this.SelectedApartment.ApartmentKind = this.ApartmentKindListDataContext.SelectedApartmentKind;
        }
        #endregion
        #endregion

        #region - Public
        #region - EventHandler

        public delegate void GoToRenterEventHandler(RenterEntity renter);
        public GoToRenterEventHandler OnGoToRenter;

        #endregion

        #region - Properties

        /// <summary>
        /// Gets or sets the apartment ListView.
        /// </summary>
        /// <value>
        /// The apartment ListView.
        /// </value>
        public ICollectionView ApartmentListView
        {
            get
            {
                return this._apartmentListView;
            }
            set
            {
                if (this._apartmentListView != value)
                {
                    this._apartmentListView = value;
                    base.OnPropertyChanged(() => this.ApartmentListView);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected apartment.
        /// </summary>
        /// <value>
        /// The selected apartment.
        /// </value>
        public ApartmentEntity SelectedApartment
        {
            get
            {
                return this._selectedApartment;
            }
            set
            {
                if (this._selectedApartment != value)
                {
                    this._selectedApartment = value;
                    base.OnPropertyChanged(() => this.SelectedApartment);
                }
            }
        }

        /// <summary>
        /// Gets or sets the searched text.
        /// </summary>
        /// <value>
        /// The searched text.
        /// </value>
        public string SearchedText
        {
            get
            {
                return this._searchedText;
            }
            set
            {
                if (this._searchedText != value)
                {
                    this._searchedText = value;
                    base.OnPropertyChanged(() => this.SearchedText);
                }
            }
        }

        /// <summary>
        /// Gets or sets the apartment table selected search.
        /// </summary>
        /// <value>
        /// The apartment table selected search.
        /// </value>
        public ApartmenTableSearchTypes ApartmentTableSelectedSearch
        {
            get
            {
                return this._apartmentTableSelectedSearch;
            }
            set
            {
                if (this._apartmentTableSelectedSearch != value)
                {
                    this._apartmentTableSelectedSearch = value;
                    base.OnPropertyChanged(() => this.ApartmentTableSelectedSearch);
                }
            }
        }

        /// <summary>
        /// Gets or sets the apartment list.
        /// </summary>
        /// <value>
        /// The apartment list.
        /// </value>
        public ObservableCollection<ApartmentEntity> ApartmentList
        {
            get
            {
                return this._apartmentList;
            }
            set
            {
                if (this._apartmentList != value)
                {
                    this._apartmentList = value;
                    base.OnPropertyChanged(() => this.ApartmentList);
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
                    base.OnPropertyChanged(() => this.RenterTableDataContext);
                }
            }
        }

        /// <summary>
        /// Gets or sets the rating list data context.
        /// </summary>
        /// <value>
        /// The rating list data context.
        /// </value>
        public IRatingListViewModel RatingListDataContext
        {
            get
            {
                return this._ratingListDataContext;
            }
            set
            {
                if (this._ratingListDataContext != value)
                {
                    this._ratingListDataContext = value;
                    base.OnPropertyChanged(() => this.RatingListDataContext);
                }
            }
        }

        /// <summary>
        /// Gets or sets the apartment kind list data context.
        /// </summary>
        /// <value>
        /// The apartment kind list data context.
        /// </value>
        public IApartmentKindListViewModel ApartmentKindListDataContext
        {
            get
            {
                return this._apartmentKindListDataContext;
            }
            set
            {
                if (this._apartmentKindListDataContext != value)
                {
                    this._apartmentKindListDataContext = value;
                    base.OnPropertyChanged(() => this.ApartmentKindListDataContext);
                }
            }
        }

        /// <summary>
        /// Gets or sets the state list data context.
        /// </summary>
        /// <value>
        /// The state list data context.
        /// </value>
        public IStateListViewModel StateListDataContext
        {
            get
            {
                return this._stateListDataContext;
            }
            set
            {
                if (this._stateListDataContext != value)
                {
                    this._stateListDataContext = value;
                    base.OnPropertyChanged(() => this.StateListDataContext);
                }
            }
        }

        #region - ICommands

        public ICommand SaveApartments
        {
            get
            {
                if (this._saveApartments == null)
                {
                    this._saveApartments = new DelegateCommand(() => this.SaveApartmentsExecute(), () => this.SaveApartmentsCanExecute());
                }

                return this._saveApartments;
            }
        }

        /// <summary>
        /// Gets the reload apartments.
        /// </summary>
        /// <value>
        /// The reload apartments.
        /// </value>
        public ICommand ReloadApartments
        {
            get
            {
                if (this._reloadApartments == null)
                {
                    this._reloadApartments = new DelegateCommand(() => this.ReloadApartmentsExecute());
                }

                return this._reloadApartments;
            }
        }

        /// <summary>
        /// Gets the search apartments.
        /// </summary>
        /// <value>
        /// The search apartments.
        /// </value>
        public ICommand SearchApartments
        {
            get
            {
                if (this._searchApartments == null)
                {
                    this._searchApartments = new DelegateCommand(() => this.SearchApartmentExecute(), () => this.SearchApartmentsCanExecute());
                }

                return this._searchApartments;
            }
        }

        /// <summary>
        /// Gets the delete apartment.
        /// </summary>
        /// <value>
        /// The delete apartment.
        /// </value>
        public ICommand DeleteApartment
        {
            get
            {
                if (this._deleteApartment == null)
                {
                    this._deleteApartment = new DelegateCommand(() => this.DeleteApartmentExecute(), () => this.DeleteApartmentCanExecute());
                }

                return this._deleteApartment;
            }
        }

        /// <summary>
        /// Gets the reset search.
        /// </summary>
        /// <value>
        /// The reset search.
        /// </value>
        public ICommand ResetApartmentsSearch
        {
            get
            {
                if (this._resetSearch == null)
                {
                    this._resetSearch = new DelegateCommand(() => this.ResetApartmentSearchExecute(), () => this.ResetApartmentSearchCanExecute());
                }

                return this._resetSearch;
            }
        }

        /// <summary>
        /// Gets the open in browser.
        /// </summary>
        /// <value>
        /// The open in browser.
        /// </value>
        public ICommand OpenInBrowser
        {
            get
            {
                if (this._openInBrowser == null)
                {
                    this._openInBrowser = new DelegateCommand(() => this.OpenInBrowserExecute(), () => this.OpenInBrowserCanExecute());
                }

                return this._openInBrowser;
            }
        }

        /// <summary>
        /// Gets the go to renter.
        /// </summary>
        /// <value>
        /// The go to renter.
        /// </value>
        public ICommand GoToRenter
        {
            get
            {
                if (this._goToRenter == null)
                {
                    this._goToRenter = new DelegateCommand(() => this.GoToRenterExecute(), () => this.GoToRenterCanExecute());
                }

                return this._goToRenter;
            }
        }
        #endregion
        #endregion

        #region - Ctors

        [InjectionConstructor]
        public ApartmentTableViewModel(IApartmentTableService apartmentTableService,
                                       IApartmentKindListViewModel apartmentKindListDataContext,
                                       IRatingListViewModel ratingListDataContext,
                                       IRenterTableViewModel renterTableDataContext,
                                       IStateListViewModel stateListDataContext)
        {
            Guard.ArgumentNotNull(apartmentTableService, "apartmentTableService");
            Guard.ArgumentNotNull(apartmentKindListDataContext, "apartmentKindListDataContext");
            Guard.ArgumentNotNull(ratingListDataContext, "ratingListDataContext");
            Guard.ArgumentNotNull(renterTableDataContext, "renterTableDataContext");
            Guard.ArgumentNotNull(stateListDataContext, "stateListDataContext");

            this._apartmentTableService = apartmentTableService;
            this.ApartmentKindListDataContext = apartmentKindListDataContext;
            this.RatingListDataContext = ratingListDataContext;
            this.RenterTableDataContext = renterTableDataContext;
            this.StateListDataContext = stateListDataContext;

            ((RenterTableViewModel)this.RenterTableDataContext).SelectedRenterChanged += this.RenterTableDataContext_SelectedRenterChanged;
            ((RatingListViewModel)this.RatingListDataContext).SelectedRatingChanged += this.ApartmentTableViewModel_SelectedRatingChanged;
            ((StateListViewModel)this.StateListDataContext).SelectedStateChanged += this.StateListDataContext_SelectedStateChanged;
            ((ApartmentKindListViewModel)this.ApartmentKindListDataContext).SelectedApartmentKindChanged += this.ApartmentKindListDataContext_SelectedApartmentKindChanged;
            this.ReloadApartmentsExecute();
        }
        #endregion
        #endregion
    }
}
