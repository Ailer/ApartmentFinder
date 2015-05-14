using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.ApartmentKindList.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.ApartmentKindList.ViewModels
{
    public class ApartmentKindListViewModel : ViewModelBase, IApartmentKindListViewModel
    {
        #region - Private
        #region - Vars

        private IApartmentKindListService _apartmentKindListService;
        private ApartmentKindEntity _selectedApartmentKind;
        private ObservableCollection<ApartmentKindEntity> _apartmentKindList;

        #endregion
        #endregion

        #region - Protected
        #region - Functions

        /// <summary>
        /// Called when [apartment kind changed].
        /// </summary>
        protected virtual void OnApartmentKindChanged()
        {
            if (this.SelectedApartmentKindChanged != null)
            {
                this.SelectedApartmentKindChanged();
            }
        }

        protected virtual void LoadApartmentKindList()
        {
            this.ApartmentKindList = new ObservableCollection<ApartmentKindEntity>(this._apartmentKindListService.GetApartmentKinds());
        }
        #endregion
        #endregion

        #region - Public
        #region - Vars

        public delegate void SelectedApartmentKindEventHandler();
        public SelectedApartmentKindEventHandler SelectedApartmentKindChanged;

        #endregion

        #region - Properties

        /// <summary>
        /// Gets or sets the apartment kind list.
        /// </summary>
        /// <value>
        /// The apartment kind list.
        /// </value>
        public ObservableCollection<ApartmentKindEntity> ApartmentKindList
        {
            get
            {
                return this._apartmentKindList;
            }
            protected set
            {
                if (this._apartmentKindList != value)
                {
                    this._apartmentKindList = value;
                    base.OnPropertyChanged(() => this.ApartmentKindList);
                }
            }
        }

        public ApartmentKindEntity SelectedApartmentKind
        {
            get
            {
                return this._selectedApartmentKind;
            }
            set
            {
                if (this._selectedApartmentKind != value)
                {
                    this._selectedApartmentKind = value;
                    base.OnPropertyChanged(() => this.SelectedApartmentKind);
                    this.OnApartmentKindChanged();
                }
            }
        }
        #endregion

        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentKindListViewModel"/> class.
        /// </summary>
        /// <param name="apartmentKindListService">The apartment kind list service.</param>
        [InjectionConstructor]
        public ApartmentKindListViewModel(IApartmentKindListService apartmentKindListService)
        {
            Guard.ArgumentNotNull(apartmentKindListService, "apartmentKindListService");

            this._apartmentKindListService = apartmentKindListService;
            this.LoadApartmentKindList();
        }
        #endregion
        #endregion
    }
}
