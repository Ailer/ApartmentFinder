using ApartmentFinder.App.Configuration;
using ApartmentFinder.App.ViewModels;
using LibApartmentFinder.Data.Configuration;
using LibApartmentFinder.WPF.ApartmentTable.ViewModels;
using LibApartmentFinder.WPF.Configuration;
using LibApartmentFinder.WPF.RenterTable.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApartmentFinder.App
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UnityContainer container = new UnityContainer();
            container.AddExtension(new ApartmentFinderAppUnityContainerExtension());
            container.AddExtension(new ApartmentFinderWPFUnityContainerExtension());
            container.AddExtension(new ApartmentFinderDataUnityContainerExtension());

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            this.DataContext = ServiceLocator.Current.GetInstance<IMainWindowViewModel>();
        }
    }
}
