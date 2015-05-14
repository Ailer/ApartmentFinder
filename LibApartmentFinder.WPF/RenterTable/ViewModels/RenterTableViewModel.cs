using LibApartmentFinder.Infastructure.Helpers;
using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.RenterTable.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Linq;
using LibApartmentFinder.Data.EntityFramework;
using System.Windows;
using System.Data.Entity;
using LibApartmentFinder.WPF.RenterTable.Enums;
using LibApartmentFinder.Data.Validators;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;

namespace LibApartmentFinder.WPF.RenterTable.ViewModels
{
    public class RenterTableViewModel : ViewModelBase, IRenterTableViewModel
    {
        #region - Private
        #region - Vars

        private IRenterTableService _renterTableService;
        private ObservableCollection<RenterEntity> _renterList;
        private ICollectionView _renterListView;
        private RenterEntity _selectedRenter;
        private string _searchedText;
        private ICommand _saveRenters;
        private ICommand _loadRenters;
        private ICommand _deleteRenter;
        private ICommand _searchRenter;
        private ICommand _resetSearch;
        private ICommand _contactRenter;
        protected RenterTableSearchTypes _renterTableSelectedSearch;

        #endregion
        #endregion

        #region - Protected
        #region - Functions

        /// <summary>
        /// Loads the renters execute.
        /// </summary>
        protected virtual void LoadRentersExecute()
        {
            this.RenterList = new ObservableCollection<RenterEntity>(this._renterTableService.GetRenters());
            this.RenterListView = CollectionViewSource.GetDefaultView(this.RenterList);
        }

        /// <summary>
        /// Saves the renters execute.
        /// </summary>
        protected virtual void SaveRentersExecute()
        {
            try
            {
                this._renterTableService.SaveRenters(this.RenterList);
                MessageBox.Show("Renters saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Saves the renters can execute.
        /// </summary>
        /// <returns></returns>
        protected virtual bool SaveRentersCanExecute()
        {
            return this.RenterList.FirstOrDefault(w => (w.EntityState == EntityState.Modified
                                           || w.EntityState == EntityState.Detached)
                                           && ServiceLocator.Current.GetInstance<IValidatorFactory>().GetValidator<RenterEntity>().Validate(w).IsValid)
                                           != null;
        }

        /// <summary>
        /// Deletes the renter execute.
        /// </summary>
        protected virtual void DeleteRenterExecute()
        {
            MessageBoxResult deleteRenter = MessageBox.Show("Do you want to delete the selected renter?", "Delete Renter", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (deleteRenter == MessageBoxResult.Yes)
            {
                if (!this._renterTableService.CheckIfRenterHasApartments(this.SelectedRenter.RenterID))
                {
                    this._renterTableService.DeleteRenter(this.SelectedRenter.RenterID);
                    this.RenterList.Remove(this.SelectedRenter);
                }
                else
                {
                    MessageBox.Show("The renter has apartments and could not be deleted.");
                }
            }
        }

        /// <summary>
        /// Deletes the renter can execute.
        /// </summary>
        /// <returns></returns>
        protected virtual bool DeleteRenterCanExecute()
        {
            return this.SelectedRenter != null;
        }

        /// <summary>
        /// Searches the renter execute.
        /// </summary>
        protected virtual void SearchRenterExecute()
        {
            this.RenterListView.Filter = FilterRenterList;
        }

        /// <summary>
        /// Searches the renter can execute.
        /// </summary>
        /// <returns></returns>
        protected virtual bool SearchRenterCanExecute()
        {
            return !string.IsNullOrWhiteSpace(this.SearchedText);
        }

        /// <summary>
        /// Filters the renter list.
        /// </summary>
        /// <param name="renter">The renter.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidCastException">No RenterEntity</exception>
        protected virtual bool FilterRenterList(object renter)
        {
            RenterEntity tmp = renter as RenterEntity;

            if (tmp == null)
            {
                throw new InvalidCastException("No RenterEntity");
            }

            switch (this.RenterTableSelectedSearch)
            {
                case RenterTableSearchTypes.Name:
                    return tmp.Name.ToLower().Replace(" ", string.Empty).Contains(this.SearchedText.ToLower().Replace(" ", string.Empty));
                case RenterTableSearchTypes.EMail:
                    return tmp.EMail.ToLower().Replace(" ", string.Empty).Contains(this.SearchedText.ToLower().Replace(" ", string.Empty));
                case RenterTableSearchTypes.Mobilenumber:
                    return tmp.Mobilenumber.ToLower().Replace(" ", string.Empty).Contains(this.SearchedText.ToLower().Replace(" ", string.Empty));
                case RenterTableSearchTypes.Telephonenumber:
                    return tmp.Telephonenumber.ToLower().Replace(" ", string.Empty).Contains(this.SearchedText.ToLower().Replace(" ", string.Empty));
                default:
                    return false;
            }
        }

        /// <summary>
        /// Resets the search execute.
        /// </summary>
        protected virtual void ResetSearchExecute()
        {
            this.RenterListView.Filter = null;
            this.SearchedText = null;
            this.RenterTableSelectedSearch = RenterTableSearchTypes.Name;
        }

        /// <summary>
        /// Resets the search can execute.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ResetSearchCanExecute()
        {
            return this.RenterListView.Filter != null;
        }

        /// <summary>
        /// Called when [renter changed].
        /// </summary>
        protected virtual void OnRenterChanged()
        {
            if (this.SelectedRenterChanged != null)
            {
                this.SelectedRenterChanged();
            }
        }
        #endregion
        #endregion

        #region - Public
        #region - Vars

        public delegate void RenterChangedEventHandler();
        public event RenterChangedEventHandler SelectedRenterChanged;

        #endregion

        #region - Properties

        /// <summary>
        /// Gets or sets the renter list.
        /// </summary>
        /// <value>
        /// The renter list.
        /// </value>
        public ObservableCollection<RenterEntity> RenterList
        {
            get
            {
                return this._renterList;
            }
            set
            {
                if (this._renterList != value)
                {
                    this._renterList = value;
                    base.OnPropertyChanged(() => this.RenterList);
                }
            }
        }

        public ICollectionView RenterListView
        {
            get
            {
                return this._renterListView;
            }
            set
            {
                if (this._renterListView != value)
                {
                    this._renterListView = value;
                    base.OnPropertyChanged(() => this.RenterListView);
                }
            }
        }

        public RenterEntity SelectedRenter
        {
            get
            {
                return this._selectedRenter;
            }
            set
            {
                if (this._selectedRenter != value)
                {
                    this._selectedRenter = value;
                    this.OnRenterChanged();
                    base.OnPropertyChanged(() => this.SelectedRenter);
                }
            }
        }

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

        public RenterTableSearchTypes RenterTableSelectedSearch
        {
            get
            {
                return this._renterTableSelectedSearch;
            }
            set
            {
                if (this._renterTableSelectedSearch != value)
                {
                    this._renterTableSelectedSearch = value;
                    base.OnPropertyChanged(() => this.RenterTableSelectedSearch);
                }
            }
        }

        #region - ICommands

        public ICommand SaveRenters
        {
            get
            {
                if (this._saveRenters == null)
                {
                    this._saveRenters = new DelegateCommand(() => this.SaveRentersExecute(), () => this.SaveRentersCanExecute());
                }

                return this._saveRenters;
            }
        }

        public ICommand LoadRenters
        {
            get
            {

                if (this._loadRenters == null)
                {
                    this._loadRenters = new DelegateCommand(() => this.LoadRentersExecute());
                }

                return this._loadRenters;
            }
        }

        public ICommand DeleteRenter
        {
            get
            {
                if (this._deleteRenter == null)
                {
                    this._deleteRenter = new DelegateCommand(() => this.DeleteRenterExecute(), () => this.DeleteRenterCanExecute());
                }

                return this._deleteRenter;
            }
        }

        /// <summary>
        /// Gets the search renter.
        /// </summary>
        /// <value>
        /// The search renter.
        /// </value>
        public ICommand SearchRenter
        {
            get
            {
                if (this._searchRenter == null)
                {
                    this._searchRenter = new DelegateCommand(() => this.SearchRenterExecute(), () => this.SearchRenterCanExecute());
                }

                return this._searchRenter;
            }
        }

        public ICommand ResetSearch
        {
            get
            {
                if (this._resetSearch == null)
                {
                    this._resetSearch = new DelegateCommand(() => this.ResetSearchExecute(), () => this.ResetSearchCanExecute());
                }

                return this._resetSearch;
            }

        }

        public ICommand ContactRenter
        {
            get
            {
                return this._contactRenter;
            }
        }
        #endregion
        #endregion

        #region - Ctors

        [InjectionConstructor]
        public RenterTableViewModel(IRenterTableService renterTableSerivce)
        {
            Guard.ArgumentNotNull(renterTableSerivce, "renterTableSerivce");

            this._renterTableService = renterTableSerivce;
            this.LoadRentersExecute();
        }
        #endregion
        #endregion
    }
}