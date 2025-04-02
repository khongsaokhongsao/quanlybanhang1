using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuanLyBanHang.Models;
using QuanLyBanHang.Repositories;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Views;
using System.ComponentModel;
using System.Windows;

namespace QuanLyBanHang.ViewModels
{
    public class CategoryListViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        private Category _selectedCategory;
        private readonly CategoryRepository _categoryRepository;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public Category Category { get; set; }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public ICommand ShowCategoryListCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ICommand EditCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }
        public ICommand SaveCategoryCommand { get; set; }

        public CategoryListViewModel()
        {
            _categoryRepository = new CategoryRepository();

            // Khởi tạo danh sách danh mục từ cơ sở dữ liệu
            Categories = new ObservableCollection<Category>(_categoryRepository.GetAllCategories());

            // Khởi tạo các command
            ShowCategoryListCommand = new RelayCommand(ShowCategoryList);
            AddCategoryCommand = new RelayCommand(AddCategory);
            EditCategoryCommand = new RelayCommand(EditCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
            SaveCategoryCommand = new RelayCommand(SaveCategory);

            // Mặc định hiển thị danh sách danh mục
            ShowCategoryList(null);
        }

        public void ShowCategoryList(object obj)
        {
            // Tải lại danh sách danh mục từ cơ sở dữ liệu
            LoadCategories();

            var categoryListView = new CategoryListContent
            {
                DataContext = this
            };
            CurrentView = categoryListView;
        }

        public void AddCategory(object obj)
        {
            Category = new Category();
            var categoryFormView = new CategoryFormView(Category, OnCategoryFormCompleted)
            {
                DataContext = this // Sửa lại: DataContext là CategoryListViewModel để binding các Command
            };
            CurrentView = categoryFormView;
        }

        public void EditCategory(object obj)
        {
            if (obj is Category category)
            {
                Category = new Category
                {
                    Id = category.Id,
                    Name = category.Name
                };
                var categoryFormView = new CategoryFormView(Category, OnCategoryFormCompleted)
                {
                    DataContext = this // Sửa lại: DataContext là CategoryListViewModel để binding các Command
                };
                CurrentView = categoryFormView;
            }
        }

        public void DeleteCategory(object obj)
        {
            if (obj is Category category)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa danh mục '{category.Name}' không? Sản phẩm thuộc danh mục này có thể bị ảnh hưởng.",
                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _categoryRepository.DeleteCategory(category.Id);
                        Categories.Remove(category);

                        // Thông báo cho MainViewModel cập nhật danh sách danh mục
                        NotifyCategoryChanged();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa danh mục: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SaveCategory(object obj)
        {
            try
            {
                // Ghi log để kiểm tra giá trị của Category.Name
                Console.WriteLine($"Saving category: Id={Category.Id}, Name={Category.Name}");

                // Kiểm tra ràng buộc cơ bản
                if (string.IsNullOrWhiteSpace(Category.Name))
                {
                    throw new ArgumentException("Tên danh mục không được để trống.");
                }

                // Kiểm tra tên danh mục trùng lặp
                if (_categoryRepository.IsCategoryNameExists(Category.Name, Category.Id))
                {
                    throw new ArgumentException("Tên danh mục đã tồn tại.");
                }

                if (Category.Id == 0) // Thêm danh mục mới
                {
                    _categoryRepository.AddCategory(Category);
                    Categories.Add(Category);
                }
                else // Cập nhật danh mục
                {
                    _categoryRepository.UpdateCategory(Category);
                    var categoryToUpdate = Categories.FirstOrDefault(c => c.Id == Category.Id);
                    if (categoryToUpdate != null)
                    {
                        categoryToUpdate.Name = Category.Name;
                    }
                }

                // Thông báo cho MainViewModel cập nhật danh sách danh mục
                NotifyCategoryChanged();

                // Quay lại danh sách danh mục sau khi lưu
                ShowCategoryList(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnCategoryFormCompleted(bool isSaved, Category category)
        {
            if (isSaved)
            {
                SaveCategory(null);
            }
            else
            {
                ShowCategoryList(null);
            }
        }

        private void LoadCategories()
        {
            try
            {
                // Xóa danh sách hiện tại để tránh trùng lặp
                Categories.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và thêm vào danh sách
                var categoryList = _categoryRepository.GetAllCategories();
                foreach (var category in categoryList)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách danh mục: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NotifyCategoryChanged()
        {
            var mainViewModel = (Application.Current.MainWindow.DataContext as MainViewModel);
            if (mainViewModel != null)
            {
                mainViewModel.UpdateProductManagementCategories();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}