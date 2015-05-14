using LibApartmentFinder.WPF.RenterTable.ViewModels;
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

namespace LibApartmentFinder.WPF.RenterTable
{
    public class RenterTable : Control
    {
        public static DependencyProperty RenterTableDataContextProperty = DependencyProperty.Register("RenterTableDataContext", typeof(IRenterTableViewModel), typeof(RenterTable));

        public IRenterTableViewModel RenterTableDataContext
        {
            get
            {
                return (IRenterTableViewModel)this.GetValue(RenterTable.RenterTableDataContextProperty);
            }
            set
            {
                this.SetValue(RenterTable.DataContextProperty, (IRenterTableViewModel)value);
                this.SetValue(RenterTable.RenterTableDataContextProperty, (IRenterTableViewModel)value);
            }
        }

        static RenterTable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RenterTable), new FrameworkPropertyMetadata(typeof(RenterTable)));
        }
    }
}
