using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Views;
using QuanLyBanHang.Models;
using QuanLyBanHang.Repositories;
using System.Collections.ObjectModel;

namespace QuanLyBanHang.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
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
        public ICommand ShowHomeCommand { get; }
        public ICommand ShowUserManagementCommand { get; }
        public ICommand ShowProductListCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand ShowManufacturerListCommand { get; set; }
        public ICommand ShowCategoryListCommand { get; set; }
        public ICommand ShowProductListForManagementCommand { get; set; }
        public ICommand AddProductForManagementCommand { get; set; }
        public ICommand OpenManufacturerFormCommand { get; }
        public ObservableCollection<UserModel> Users { get; set; }

        private UserManagementView userManagementView;
        private UserManagementViewModel userManagementViewModel;

        private ProductManagementView _productManagementView;
        private ProductManagementViewModel _productManagementViewModel;
        private ManufacturerListView _manufacturerListView;
        private ManufacturerListViewModel _manufacturerListViewModel;


        public MainViewModel()
        {
            // Khởi tạo ProductManagementView và ViewModel
            _productManagementViewModel = new ProductManagementViewModel();
            _productManagementView = new ProductManagementView
            {
                DataContext = _productManagementViewModel
            };

            // Khởi tạo các Command
            OpenManufacturerFormCommand = new RelayCommand(OpenManufacturerForm);
            ShowUserManagementCommand = new RelayCommand(ShowUserManagement);
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowProductListCommand = new RelayCommand(ShowProductList);
            AddProductCommand = new RelayCommand(AddProduct);
            ShowManufacturerListCommand = new RelayCommand(ShowManufacturerList);

            ShowCategoryListCommand = new RelayCommand(ShowCategoryList);
            ShowProductListForManagementCommand = new RelayCommand(ShowProductListForManagement);
            AddProductForManagementCommand = new RelayCommand(AddProductForManagement);

            // Mặc định hiển thị Home
            ShowHome(null);

            Users = new ObservableCollection<UserModel>();

            ShowHomeCommand = new RelayCommand(o =>
            {
                CurrentView = new WelcomeView();
            });

            ShowUserManagementCommand = new RelayCommand(o =>
            {
                if (userManagementView == null)
                {
                    userManagementViewModel = new UserManagementViewModel();
                    userManagementView = new UserManagementView
                    {
                        DataContext = userManagementViewModel
                    };
                }
                CurrentView = userManagementView;
            });

            CurrentView = new WelcomeView();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowUserManagement(object obj)
        {
            CurrentView = new UserManagementView();
        }

        private void ShowHome(object obj)
        {
            CurrentView = null; // Thay thế bằng giao diện Home nếu có
        }

        private void ShowProductList(object obj)
        {
            // Chức năng này cần InventoryManagementView
            // Nếu không cần, có thể bỏ hoặc thay thế
            throw new NotImplementedException("Chức năng Quản lý nhập hàng chưa được triển khai.");
        }

        private void AddProduct(object obj)
        {
            // Chức năng này cần InventoryManagementView
            throw new NotImplementedException("Chức năng Quản lý nhập hàng chưa được triển khai.");
        }

        private void ShowManufacturerList(object obj)
        {
            if (_manufacturerListView == null)
            {
                _manufacturerListViewModel = new ManufacturerListViewModel();
                _manufacturerListView = new ManufacturerListView
                {
                    DataContext = _manufacturerListViewModel
                };
            }

            CurrentView = _manufacturerListView;
        }


        private void ShowCategoryList(object obj)
        {
            // Chức năng này cần InventoryManagementView
            throw new NotImplementedException("Chức năng Quản lý nhập hàng chưa được triển khai.");
        }

        private void ShowProductListForManagement(object obj)
        {
            CurrentView = _productManagementView;
            _productManagementViewModel.ShowProductList(null); // Dòng 136
        }

        private void AddProductForManagement(object obj)
        {
            CurrentView = _productManagementView;
            _productManagementViewModel.AddProduct(null); // Dòng 142
        }
        private void OpenManufacturerForm(object obj)
        {
            var view = new ManufacturerFormView();
            view.DataContext = new ManufacturerFormViewModel();
            CurrentView = view;
        }
    }
}