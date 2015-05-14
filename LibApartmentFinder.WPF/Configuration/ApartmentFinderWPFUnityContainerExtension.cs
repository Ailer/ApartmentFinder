using LibApartmentFinder.WPF.ApartmentKindList.Services;
using LibApartmentFinder.WPF.ApartmentKindList.ViewModels;
using LibApartmentFinder.WPF.ApartmentTable.Services;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using LibApartmentFinder.WPF.RatingList.Services;
using LibApartmentFinder.WPF.RatingList.ViewModels;
using LibApartmentFinder.WPF.RenterTable.Services;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using LibApartmentFinder.WPF.StateList.Services;
using LibApartmentFinder.WPF.StateList.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.Configuration
{
    public class ApartmentFinderWPFUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            base.Container.RegisterType<IApartmentTableViewModel, ApartmentTableViewModel>();
            base.Container.RegisterType<IRenterTableViewModel, RenterTableViewModel>();
            base.Container.RegisterType<IApartmentKindListViewModel, ApartmentKindListViewModel>();
            base.Container.RegisterType<IRatingListViewModel, RatingListViewModel>();
            base.Container.RegisterType<IStateListViewModel, StateListViewModel>();

            base.Container.RegisterType<IApartmentKindListService, ApartmentKindListService>();
            base.Container.RegisterType<IApartmentTableService, ApartmentTableService>();
            base.Container.RegisterType<IRatingListService, RatingListService>();
            base.Container.RegisterType<IRenterTableService, RenterTableService>();
            base.Container.RegisterType<IStateListService, StateListService>();
        }
    }
}
