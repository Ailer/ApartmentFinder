using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.StateList.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.StateList.ViewModels
{
    public class StateListViewModel : ViewModelBase, IStateListViewModel
    {
        #region - Private
        #region - Vars

        private IStateListService _stateListService;
        private StateEntity _selectedState;
        private ObservableCollection<StateEntity> _stateList;

        #endregion
        #endregion
        #region - Protected
        #region - Functions

        /// <summary>
        /// Called when [state changed].
        /// </summary>
        protected virtual void OnStateChanged()
        {
            if (this.SelectedStateChanged != null)
            {
                this.SelectedStateChanged();
            }
        }

        protected virtual void LoadStates()
        {
            this.StateList = new ObservableCollection<StateEntity>(this._stateListService.GetStates());
        }
        #endregion
        #endregion

        #region - Public
        #region - Vars

        public delegate void StateChangedEventHandler();
        public StateChangedEventHandler SelectedStateChanged;

        #endregion

        #region - Properties

        /// <summary>
        /// Gets or sets the state of the selected.
        /// </summary>
        /// <value>
        /// The state of the selected.
        /// </value>
        public StateEntity SelectedState
        {
            get
            {
                return this._selectedState;
            }
            set
            {
                if (this._selectedState != value)
                {
                    this._selectedState = value;
                    base.OnPropertyChanged(() => this.SelectedState);
                    this.OnStateChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the state list.
        /// </summary>
        /// <value>
        /// The state list.
        /// </value>
        public ObservableCollection<StateEntity> StateList
        {
            get
            {
                return this._stateList;
            }
            protected set
            {
                if (this._stateList != value)
                {
                    this._stateList = value;
                    base.OnPropertyChanged(() => this.StateList);
                }
            }
        }
        #endregion 

        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="StateListViewModel"/> class.
        /// </summary>
        /// <param name="stateListService">The state list service.</param>
        [InjectionConstructor]
        public StateListViewModel(IStateListService stateListService)
        {
            Guard.ArgumentNotNull(stateListService, "stateListService");

            this._stateListService = stateListService;
            this.LoadStates();
        }
        #endregion
        #endregion
    }
}
