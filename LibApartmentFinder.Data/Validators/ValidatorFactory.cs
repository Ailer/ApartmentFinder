using FluentValidation;
using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Validators
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(Type type)
        {
            if (type == typeof(RenterEntity))
            {
                return new RenterValidator();
            }
            else if (type == typeof(ApartmentEntity))
            {
                return new ApartmentValidator();
            }
            else
            {
                throw new ArgumentException("Wrong type");
            }
        }

        public IValidator<T> GetValidator<T>()
        {
            if (typeof(T) == typeof(RenterEntity))
            {
                return new RenterValidator() as IValidator<T>;
            }
            else if(typeof(T) == typeof(ApartmentEntity))
            {
                return new ApartmentValidator() as IValidator<T>;
            }
            else
            {
                throw new ArgumentException("Wrong type");
            }
        }
    }
}
