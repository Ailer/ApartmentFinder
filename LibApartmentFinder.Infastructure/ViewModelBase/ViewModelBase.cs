using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibApartmentFinder.Infastructure.ViewModelBase
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException("memberExpression");
            }

            var body = memberExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Lambda must return a property.");
            }

            this.OnPropertyChanged(body.Member.Name);
        }
    }
}
