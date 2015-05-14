using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Infastructure.ViewModelBase;
using LibApartmentFinder.WPF.RatingList.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.RatingList.ViewModels
{
    public class RatingListViewModel : ViewModelBase, IRatingListViewModel
    {
        #region - Private
        #region - Vars

        private IRatingListService _ratingListService;
        private RatingEntity _selectedRating;
        private ObservableCollection<RatingEntity> _ratingList;

        #endregion
        #endregion

        #region - Protected
        #region - Functions

        protected virtual void OnRatingChanged()
        {
            if (this.SelectedRatingChanged != null)
            {
                this.SelectedRatingChanged();
            }
        }
        #endregion
        #endregion

        #region - Public
        #region - Vars

        public delegate void RatingChangedEventHandler();
        public event RatingChangedEventHandler SelectedRatingChanged;

        #endregion

        #region - Properties

        /// <summary>
        /// Gets or sets the selected rating.
        /// </summary>
        /// <value>
        /// The selected rating.
        /// </value>
        public RatingEntity SelectedRating
        {
            get
            {
                return this._selectedRating;
            }
            set
            {
                if (this._selectedRating != value)
                {
                    this._selectedRating = value;
                    base.OnPropertyChanged(() => this.SelectedRating);
                    this.OnRatingChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the rating list.
        /// </summary>
        /// <value>
        /// The rating list.
        /// </value>
        public ObservableCollection<RatingEntity> RatingList
        {
            get
            {
                return this._ratingList;
            }
            protected set
            {
                if (this._ratingList != value)
                {
                    this._ratingList = value;
                    base.OnPropertyChanged(() => this._ratingList);
                }
            }
        }
        #endregion
        #region - Ctors

        [InjectionConstructor]
        public RatingListViewModel(IRatingListService ratingListService)
        {
            Guard.ArgumentNotNull(ratingListService, "ratingListService");

            this._ratingListService = ratingListService;
            this.RatingList = new ObservableCollection<RatingEntity>(this._ratingListService.GetRatings());         
        }
        #endregion
        #endregion
    }
}
