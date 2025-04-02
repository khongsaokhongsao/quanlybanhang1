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
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Views
{
    /// <summary>
    /// Interaction logic for ImportListView.xaml
    /// </summary>
    public partial class ImportListView : UserControl
    {
        public ImportListView()
        {
            InitializeComponent();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is Import selectedImport)
            {
                MessageBox.Show($"Bạn đã chọn phiếu nhập hàng: {selectedImport.ProductName}");
            }
        }
    }
}
