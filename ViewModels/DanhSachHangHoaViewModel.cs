using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QuanLyBanHang.ViewModels
{
    public class DanhSachHangHoaViewModel : INotifyPropertyChanged
    {
        // Dữ liệu mẫu
        public ObservableCollection<ProductModel> ProductList { get; set; }
        public ProductModel SelectedProduct { get; set; }

        public DanhSachHangHoaViewModel()
        {
            // Giả lập dữ liệu sản phẩm
            ProductList = new ObservableCollection<ProductModel>
            {
                new ProductModel { ProductCode = "SP001", ProductName = "Laptop Asus", Quantity = 10, ImportPrice = 15000, ImportDate = DateTime.Now, Supplier = "Asus" },
                new ProductModel { ProductCode = "SP002", ProductName = "Điện thoại iPhone", Quantity = 20, ImportPrice = 20000, ImportDate = DateTime.Now.AddDays(-3), Supplier = "Apple" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Mô hình dữ liệu sản phẩm
    public class ProductModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }
        public DateTime ImportDate { get; set; }
        public string Supplier { get; set; }
    }
}
