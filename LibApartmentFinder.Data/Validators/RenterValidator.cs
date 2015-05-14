using FluentValidation;
using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Validators
{
    public class RenterValidator : AbstractValidator<RenterEntity>, IValidator<RenterEntity>
    {
        public RenterValidator()
        {
            base.RuleFor(r => r.Name).NotEmpty();
            base.RuleFor(r => r.Name).NotNull();
            base.RuleFor(r => r.EMail).EmailAddress();
        }
    }
}
