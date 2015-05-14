using FluentValidation;
using LibApartmentFinder.Data.Provider;
using LibApartmentFinder.Data.Repository;
using LibApartmentFinder.Data.Validators;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Configuration
{
    public class ApartmentFinderDataUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            IApartmentDBProvider apartmentDBProvider = new ApartmentDBProvider(ConfigurationManager.ConnectionStrings["ApartmentDB"].ConnectionString);
            IApartmentDBRepository apartmentDBRepository = new ApartmentDBRepository(ConfigurationManager.ConnectionStrings["ApartmentDB"].ConnectionString);

            base.Container.RegisterInstance(apartmentDBProvider);
            base.Container.RegisterInstance(apartmentDBRepository);
            base.Container.RegisterType<IValidatorFactory, ValidatorFactory>();
        }
    }
}
