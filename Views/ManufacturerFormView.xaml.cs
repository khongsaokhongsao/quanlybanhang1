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
using QuanLyBanHang.ViewModels;

namespace QuanLyBanHang.Views
{
    /// <summary>
    /// Interaction logic for ManufacturerFormView.xaml
    /// </summary>
    public partial class ManufacturerFormView : UserControl
    {
        public ManufacturerFormView()
        {
            InitializeComponent();
            //DataContext = new ManufacturerFormViewModel((manufacturer) => { });

        }

    }
}
