using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Data.Repository
{
    public class ApartmentDBRepository : ApartmentDBProviderBase, IApartmentDBRepository
    {
        #region - Protected

        /// <summary>
        /// Adds the renter.
        /// </summary>
        /// <param name="renter">The renter.</param>
        /// <param name="dataContext">The data context.</param>
        protected void AddRenter(RenterEntity renter, ApartmentDBDataContext dataContext)
        {
            dataContext.EF_Renter.AddObject(renter);
        }

        /// <summary>
        /// Changes the renter.
        /// </summary>
        /// <param name="renter">The renter.</param>
        /// <param name="dataContext">The data context.</param>
        protected void ChangeRenter(RenterEntity renter, ApartmentDBDataContext dataContext)
        {
            RenterEntity tmp = dataContext.EF_Renter.FirstOrDefault(w => w.RenterID == renter.RenterID);
            tmp.Telephonenumber = renter.Telephonenumber;
            tmp.Name = renter.Name;
            tmp.Mobilenumber = renter.Mobilenumber;
            tmp.EMail = renter.EMail;
        }

        protected virtual void AddApartment(ApartmentEntity apartment, ApartmentDBDataContext dataContext)
        {

        }

        protected virtual void ChangeApartment(ApartmentEntity apartment, ApartmentDBDataContext dataContext)
        {

        }
        #endregion

        #region - Public
        #region - Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApartmentDBRepository"/> class.
        /// </summary>
        /// <param name="apartmentDBConnectionString">The apartment database connection string.</param>
        public ApartmentDBRepository(string apartmentDBConnectionString)
            : base(apartmentDBConnectionString)
        { }
        #endregion

        #region - Functions

        /// <summary>
        /// Deletes the apartment.
        /// </summary>
        /// <param name="apartmentId">The apartment identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DeleteApartment(int apartmentId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the apartments.
        /// </summary>
        /// <param name="apartments">The apartments.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveApartments(IList<ApartmentEntity> apartments)
        {
            Guard.ArgumentNotNull(apartments, "apartments");
            ApartmentDBDataContext context = base.GetDataContext();

            foreach (ApartmentEntity apartment in apartments.Where(w => w.EntityState == EntityState.Modified
                                                            || w.EntityState == EntityState.Detached))
            {
                if (apartment.EntityState == EntityState.Detached)
                {
                    this.AddApartment(apartment, context);
                }
                else
                {
                    this.ChangeApartment(apartment, context);
                }
            }
        }

        /// <summary>
        /// Saves the renters.
        /// </summary>
        /// <param name="renters">The renters.</param>
        public void SaveRenters(IList<RenterEntity> renters)
        {
            Guard.ArgumentNotNull(renters, "renters");

            ApartmentDBDataContext context = base.GetDataContext();

            foreach (RenterEntity renter in renters.Where(w => w.EntityState == EntityState.Modified
                                                            || w.EntityState == EntityState.Detached))
            {
                if (renter.EntityState == EntityState.Detached)
                {
                    this.AddRenter(renter, context);
                }
                else
                {
                    this.ChangeRenter(renter, context);
                }
            }

            context.SaveChanges();

        }

        /// <summary>
        /// Deletes the renter.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        public void DeleteRenter(int renterId)
        {
            ApartmentDBDataContext context = base.GetDataContext();

            RenterEntity renter = context.EF_Renter.FirstOrDefault(f => f.RenterID == renterId);

            if (renter != null)
            {
                context.EF_Renter.DeleteObject(renter);
                context.SaveChanges();
            }
        }
        #endregion
        #endregion
    }
}
