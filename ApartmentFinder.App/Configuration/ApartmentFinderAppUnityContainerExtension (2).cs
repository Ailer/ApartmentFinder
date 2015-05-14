using ApartmentFinder.App.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentFinder.App.Configuration
{
    public class ApartmentFinderAppUnityContainerExtension : UnityContainerExtension
    {
        
        protected override void Initialize()
        {
            this.Container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
        }
    }
}
