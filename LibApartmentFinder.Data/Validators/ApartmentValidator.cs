using FluentValidation;
using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Validators
{
    public class ApartmentValidator : AbstractValidator<ApartmentEntity>, IValidator<ApartmentEntity>
    {
        public ApartmentValidator()
        {
            base.RuleFor(r => r.PLZ).Length(5);
            base.RuleFor(r => r.Place).NotEmpty();
            base.RuleFor(r => r.Street).NotEmpty();
            base.RuleFor(r => r.Streetnumber).NotEmpty();
            base.RuleFor(r => r.Streetnumber).Length(1, 5);
            base.RuleFor(r => r.ApartmentKindID).GreaterThan(0);
            base.RuleFor(r => r.RenterID).GreaterThan(0);
        }
    }
}
