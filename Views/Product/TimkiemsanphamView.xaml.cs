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

namespace QuanLyBanHang.Views
{
    /// <summary>
    /// Interaction logic for TimkiemsanphamView.xaml
    /// </summary>
    public partial class TimkiemsanphamView : UserControl
    {
        public TimkiemsanphamView()
        {
            InitializeComponent();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && tb.Text == "Nhập tên sản phẩm...")
            {
                tb.Text = "";
                tb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Nhập tên sản phẩm...";
                tb.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string productName = txtProductName.Text;
            string productId = txtProductId.Text;
            string category = cbCategory.SelectedItem?.ToString();
            MessageBox.Show($"Tìm kiếm: Tên={productName}, Mã={productId}, Loại={category}");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtProductName.Text = "Nhập tên sản phẩm...";
            txtProductName.Foreground = System.Windows.Media.Brushes.Gray;
            txtProductId.Text = "";
            cbCategory.SelectedIndex = -1;
            dgProducts.ItemsSource = null;
        }
    }
}