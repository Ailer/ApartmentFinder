using FluentValidation;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.EntityFramework
{
    public partial class ApartmentEntity
    {
        public void CopyApartment(ApartmentEntity source)
        {
            this.PLZ = source.PLZ;
            this.Place = source.Place;
            this.Street = source.Street;
            this.Price = source.Price;
            this.Size = source.Size;
            this.Startdate = source.Startdate;
            this.Enddate = source.Enddate;
            this.Comment = source.Comment;
            this.StateID = source.StateID;
            this.ApartmentKindID = source.ApartmentKindID;
            this.RenterID = source.RenterID;
            this.RatingID = source.RatingID;
            this.Streetnumber = source.Streetnumber;
            this.Source = source.Source;
        }

        public bool IsValid()
        {
            return this.IsValid(ServiceLocator.Current.GetInstance<IValidatorFactory>().GetValidator<ApartmentEntity>());
        }

        public bool IsValid(IValidator validator)
        {
            return validator.Validate(this).IsValid;
        }
    }
}
