using System.Text;
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
using QuanLyBanHang.Views;


namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
        private void ToggleProductMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ProductSubMenu.Visibility == Visibility.Visible)
            {
                ProductSubMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                ProductSubMenu.Visibility = Visibility.Visible;
            }
        }
        private void ToggleImportMenu_Click(object sender, RoutedEventArgs e)
        {
            if (ImportSubMenu.Visibility == Visibility.Visible)
            {
                ImportSubMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                ImportSubMenu.Visibility = Visibility.Visible;
            }
        }
        private void ToggleSellMenu_Click(object sender, RoutedEventArgs e)
        {
            if (SellSubMenu.Visibility == Visibility.Visible)
            {
                SellSubMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                SellSubMenu.Visibility = Visibility.Visible;
            }
        }
        private void ToggleWarehouseMenu_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseSubMenu.Visibility == Visibility.Visible)
            {
                WarehouseSubMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                WarehouseSubMenu.Visibility = Visibility.Visible;
            }
        }
        private void ToggleFundMenu_Click(object sender, RoutedEventArgs e)
        {
            FundSubMenu.Visibility = FundSubMenu.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        private void ToggleDebtMenu_Click(object sender, RoutedEventArgs e)
        {
            DebtSubMenu.Visibility = DebtSubMenu.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        private void ToggleWarrantyMenu_Click(object sender, RoutedEventArgs e)
        {
            WarrantySubMenu.Visibility = WarrantySubMenu.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        private void ToggleAssetMenu_Click(object sender, RoutedEventArgs e)
        {
            AssetSubMenu.Visibility = AssetSubMenu.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        private void ToggleReportMenu_Click(object sender, RoutedEventArgs e)
        {
            ReportSubMenu.Visibility = ReportSubMenu.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        private void ToggleOrderMenu_Click(object sender, RoutedEventArgs e)
        {
            OrderSubMenu.Visibility = OrderSubMenu.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
       

        private ManufacturerListView _manufacturerListView;
        private ManufacturerListViewModel _manufacturerListViewModel;

    }
}