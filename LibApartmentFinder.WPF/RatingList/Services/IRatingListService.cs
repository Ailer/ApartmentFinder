using LibApartmentFinder.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.WPF.RatingList.Services
{
    public interface IRatingListService
    {
        IList<RatingEntity> GetRatings();
    }
}
