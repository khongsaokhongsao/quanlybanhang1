using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Views;
using System.ComponentModel;

namespace QuanLyBanHang.ViewModels
{
    public class ProductManagementViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public Product Product { get; set; }

        public ICommand ShowProductListCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }

        public ProductManagementViewModel()
        {
            // Khởi tạo dữ liệu mẫu
            Categories = new ObservableCollection<Category>
            {
                new Category { Id = 1, Name = "Điện tử" },
                new Category { Id = 2, Name = "Thời trang" }
            };

            Products = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Điện thoại", CategoryId = 1, Price = 10000000, Stock = 50, IsActive = true },
                new Product { Id = 2, Name = "Áo thun", CategoryId = 2, Price = 200000, Stock = 100, IsActive = false }
            };

            // Khởi tạo các command
            ShowProductListCommand = new RelayCommand(ShowProductList);
            AddProductCommand = new RelayCommand(AddProduct);
            SaveProductCommand = new RelayCommand(SaveProduct);

            // Mặc định hiển thị danh sách sản phẩm
            ShowProductList(null);
        }

        // Thêm từ khóa public
        public void ShowProductList(object obj)
        {
            var productListView = new ProductListView
            {
                DataContext = this
            };
            CurrentView = productListView;
        }

        // Thêm từ khóa public
        public void AddProduct(object obj)
        {
            Product = new Product();
            var productFormView = new ProductFormView
            {
                DataContext = this
            };
            CurrentView = productFormView;
        }

        private void SaveProduct(object obj)
        {
            try
            {
                // Kiểm tra ràng buộc cơ bản
                Product.Validate();

                // Kiểm tra tên sản phẩm trùng lặp trong cùng danh mục
                if (Products.Any(p => p.Name == Product.Name && p.CategoryId == Product.CategoryId))
                {
                    throw new ArgumentException("Tên sản phẩm đã tồn tại trong danh mục này.");
                }

                // Gán ID mới (giả lập)
                Product.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
                Products.Add(Product);

                // Quay lại danh sách sản phẩm sau khi lưu
                ShowProductList(null);
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                System.Windows.MessageBox.Show(ex.Message, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }
    }
}