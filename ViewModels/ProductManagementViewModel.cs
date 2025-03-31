using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Views;
using QuanLyBanHang.Repositories;
using System.ComponentModel;

namespace QuanLyBanHang.ViewModels
{
    public class ProductManagementViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        private Product _selectedProduct;
        private readonly ProductRepository _productRepository;

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

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        // Thuộc tính cho lọc sản phẩm
        private int _selectedCategoryId;
        public int SelectedCategoryId
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                OnPropertyChanged(nameof(SelectedCategoryId));
            }
        }

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }

        private decimal? _minPrice;
        public decimal? MinPrice
        {
            get => _minPrice;
            set
            {
                _minPrice = value;
                OnPropertyChanged(nameof(MinPrice));
            }
        }

        private decimal? _maxPrice;
        public decimal? MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
            }
        }

        // Danh sách tùy chọn trạng thái
        public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string>
        {
            "Tất cả", "Kích hoạt", "Không kích hoạt"
        };

        public ICommand ShowProductListCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }
        public ICommand FilterProductsCommand { get; set; }
        public ICommand ShowProductSearchCommand { get; set; }

        public ProductManagementViewModel()
        {
            _productRepository = new ProductRepository();

            // Khởi tạo danh sách sản phẩm và danh mục từ cơ sở dữ liệu
            Products = new ObservableCollection<Product>(_productRepository.GetAllProducts());
            Categories = new ObservableCollection<Category>(_productRepository.GetAllCategories());

            // Khởi tạo các command
            ShowProductListCommand = new RelayCommand(ShowProductList);
            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            SaveProductCommand = new RelayCommand(SaveProduct);
            FilterProductsCommand = new RelayCommand(_ => FilterProducts(null));
            ShowProductSearchCommand = new RelayCommand(ShowProductSearch);

            // Mặc định hiển thị danh sách sản phẩm
            ShowProductList(null);
        }


        private void ShowProductSearch(object obj)
        {
            CurrentView = new ProductSearchView();
        }

        public void ShowProductList(object obj)
        {
            // Tải lại danh sách sản phẩm từ cơ sở dữ liệu
            LoadProducts();

            var productListView = new ProductListView
            {
                DataContext = this
            };
            CurrentView = productListView;
        }

        public void AddProduct(object obj)
        {
            Product = new Product();
            var productFormView = new ProductFormView
            {
                DataContext = this
            };
            CurrentView = productFormView;
        }

        public void EditProduct(object obj)
        {
            if (obj is Product product)
            {
                Product = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Stock = product.Stock,
                    IsActive = product.IsActive
                };
                var productFormView = new ProductFormView
                {
                    DataContext = this
                };
                CurrentView = productFormView;
            }
        }

        public void DeleteProduct(object obj)
        {
            if (obj is Product product)
            {
                var result = System.Windows.MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm '{product.Name}' không?",
                    "Xác nhận xóa", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    try
                    {
                        _productRepository.DeleteProduct(product.Id);
                        Products.Remove(product);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}", "Lỗi",
                            System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SaveProduct(object obj)
        {
            try
            {
                // Kiểm tra ràng buộc cơ bản
                Product.Validate();

                // Kiểm tra tên sản phẩm trùng lặp trong cùng danh mục
                var existingProduct = Products.FirstOrDefault(p => p.Name == Product.Name && p.CategoryId == Product.CategoryId && p.Id != Product.Id);
                if (existingProduct != null)
                {
                    throw new ArgumentException("Tên sản phẩm đã tồn tại trong danh mục này.");
                }

                if (Product.Id == 0) // Thêm sản phẩm mới
                {
                    _productRepository.AddProduct(Product);
                    Products.Add(Product);
                }
                else // Cập nhật sản phẩm
                {
                    _productRepository.UpdateProduct(Product);
                    var productToUpdate = Products.FirstOrDefault(p => p.Id == Product.Id);
                    if (productToUpdate != null)
                    {
                        productToUpdate.Name = Product.Name;
                        productToUpdate.CategoryId = Product.CategoryId;
                        productToUpdate.Price = Product.Price;
                        productToUpdate.Stock = Product.Stock;
                        productToUpdate.IsActive = Product.IsActive;
                    }
                }

                // Quay lại danh sách sản phẩm sau khi lưu
                ShowProductList(null);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                // Xóa danh sách hiện tại để tránh trùng lặp
                Products.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và thêm vào danh sách
                var productList = _productRepository.GetAllProducts();
                foreach (var product in productList)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void FilterProducts(object obj)
        {
            try
            {
                var filteredProducts = _productRepository.GetAllProducts();

                // Lọc theo danh mục
                if (SelectedCategoryId > 0)
                {
                    filteredProducts = filteredProducts.Where(p => p.CategoryId == SelectedCategoryId).ToList();
                }

                // Lọc theo trạng thái
                if (!string.IsNullOrWhiteSpace(SelectedStatus) && SelectedStatus != "Tất cả")
                {
                    bool isActive = SelectedStatus == "Kích hoạt";
                    filteredProducts = filteredProducts.Where(p => p.IsActive == isActive).ToList();
                }

                // Lọc theo khoảng giá
                if (MinPrice.HasValue)
                {
                    filteredProducts = filteredProducts.Where(p => p.Price >= MinPrice.Value).ToList();
                }
                if (MaxPrice.HasValue)
                {
                    filteredProducts = filteredProducts.Where(p => p.Price <= MaxPrice.Value).ToList();
                }

                // Cập nhật danh sách hiển thị
                Products.Clear();
                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }

                // Ghi log để kiểm tra
                Console.WriteLine($"SelectedCategoryId: {SelectedCategoryId}, SelectedStatus: {SelectedStatus}, MinPrice: {MinPrice}, MaxPrice: {MaxPrice}");
                foreach (var product in filteredProducts)
                {
                    Console.WriteLine($"Kết quả: {product.Name} - {product.CategoryId} - {product.Price} - {product.IsActive}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Lỗi khi lọc sản phẩm: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}