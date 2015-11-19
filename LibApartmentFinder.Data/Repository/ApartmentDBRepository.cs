using FluentValidation;
using LibApartmentFinder.Data.EntityFramework;
using LibApartmentFinder.Data.Provider;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

            if (tmp != null)
            {
                tmp.Telephonenumber = renter.Telephonenumber;
                tmp.Name = renter.Name;
                tmp.Mobilenumber = renter.Mobilenumber;
                tmp.EMail = renter.EMail;
            }
        }

        protected virtual void AddApartment(ApartmentEntity apartment, ApartmentDBDataContext dataContext)
        {
            dataContext.EF_Apartment.AddObject(apartment);
        }

        protected virtual void ChangeApartment(ApartmentEntity apartment, ApartmentDBDataContext dataContext)
        {
            ApartmentEntity tmp = dataContext.EF_Apartment.FirstOrDefault(f => f.ApartmentID == apartment.ApartmentID);

            if (tmp != null)
            {
                tmp.CopyApartment(apartment);
            }
        }

        protected void DetachApartments(ApartmentDBDataContext context)
        {
            foreach (ObjectStateEntry apartment in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                if (apartment.EntityKey.EntitySetName == context.EF_Apartment.EntitySet.Name)
                {
                    apartment.ChangeState(EntityState.Detached);
                }
            }
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
            ApartmentDBDataContext context = base.GetDataContext();
            ApartmentEntity apartment = context.EF_Apartment.FirstOrDefault(f => f.ApartmentID == apartmentId);

            if (apartment != null)
            {
                context.EF_Apartment.DeleteObject(apartment);
                context.SaveChanges();
            }
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
                                                            || w.EntityState == EntityState.Added
                                                            || w.EntityState == EntityState.Detached))
            {
                if (ServiceLocator.Current.GetInstance<IValidatorFactory>().GetValidator<ApartmentEntity>().Validate(apartment).IsValid)
                {
                    if (apartment.EntityState == EntityState.Modified)
                    {
                        this.ChangeApartment(apartment, context);
                    }
                    else
                    {
                        this.AddApartment(apartment, context);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format("Invalid apartment with id:", apartment.ApartmentID));
                }
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Saves the renters.
        /// </summary>
        /// <param name="renters">The renters.</param>
        public void SaveRenters(IList<RenterEntity> renters)
        {
            Guard.ArgumentNotNull(renters, "renters");

            ApartmentDBDataContext context = base.GetDataContext();
            string connection = context.Connection.ConnectionString;

            foreach (RenterEntity renter in renters.Where(w => w.EntityState == EntityState.Modified
                                                            || w.EntityState == EntityState.Detached))
            {
                if (ServiceLocator.Current.GetInstance<IValidatorFactory>().GetValidator<RenterEntity>().Validate(renter).IsValid)
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
                else
                {
                    throw new ArgumentException(string.Format("Invalid renter Name: {0}", renter.Name));
                }
            }

            this.DetachApartments(context);
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
