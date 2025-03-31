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
        public ICommand ShowImportCreateCommand { get; }
        public ICommand ShowImportListCommand { get; }
        public ICommand ShowHomeCommand { get; }
        public ICommand ShowUserManagementCommand { get; }
        public ICommand ShowProductListCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand ShowManufacturerListCommand { get; set; }
        public ICommand ShowCategoryListCommand { get; set; }
        public ICommand ShowProductListForManagementCommand { get; set; }
        public ICommand AddProductForManagementCommand { get; set; }
        public ICommand ShowDanhmuctaothanhsanphamCommand { get; set; }
        public ICommand ShowTonKhoCommand { get; set; }
        public ICommand OpenManufacturerFormCommand { get; }
        public ICommand ShowTimkiemsanphamViewCommand { get; set; }
       






        public ObservableCollection<UserModel> Users { get; set; }

        private UserManagementView userManagementView;
        private UserManagementViewModel userManagementViewModel;
        private ImportListView _importListView;
        private ImportListViewModel _importListViewModel;

        private ProductManagementView _productManagementView;
        private ProductManagementViewModel _productManagementViewModel;
        private ManufacturerListView _manufacturerListView;
        private ManufacturerListViewModel _manufacturerListViewModel;
        private DanhmuctaothanhsanphamView _danhmuctaothanhsanphamView;
        private DanhmuctaothanhsanphamViewModel _danhmuctaothanhsanphamViewModel;
        private TonKhoView _tonKhoView;
        private TonKhoViewModel _tonkhoViewModel;
        private TimkiemsanphamView _timkiemsanphamView;
        private TimkiemsanphamViewModel _timkiemsanphamViewModel;
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
            ShowImportListCommand = new RelayCommand(_ => ShowImportList());
            ShowImportCreateCommand = new RelayCommand(_ => ShowImportCreate());
            ShowCategoryListCommand = new RelayCommand(ShowCategoryList);
            ShowProductListForManagementCommand = new RelayCommand(ShowProductListForManagement);
            AddProductForManagementCommand = new RelayCommand(AddProductForManagement);
            ShowDanhmuctaothanhsanphamCommand = new RelayCommand(ShowDanhmuctaothanhsanpham);
            ShowTonKhoCommand = new RelayCommand(ShowTonKho);
            ShowTimkiemsanphamViewCommand = new RelayCommand(ShowTimkiemsanpham);




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
        private void ShowTimkiemsanpham(object obj)
        {
            if (_timkiemsanphamView == null)
            {
                _timkiemsanphamViewModel = new TimkiemsanphamViewModel();
                _timkiemsanphamView = new TimkiemsanphamView
                {
                    DataContext = _timkiemsanphamViewModel
                };
            }
            CurrentView = _timkiemsanphamView;
        }
        private void ShowHome(object obj)
        {
            CurrentView = null; // Thay thế bằng giao diện Home nếu có
        }

        private void ShowProductList(object obj)
        {
            CurrentView = _productManagementView;
            _productManagementViewModel.ShowProductList(null);
        }

        private void AddProduct(object obj)
        {
            CurrentView = _productManagementView;
            _productManagementViewModel.AddProduct(null);
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
        private void ShowDanhmuctaothanhsanpham(object obj)
        {
            if (_danhmuctaothanhsanphamView == null)
            {
                _danhmuctaothanhsanphamViewModel = new DanhmuctaothanhsanphamViewModel();
                _danhmuctaothanhsanphamView = new DanhmuctaothanhsanphamView
                {
                    DataContext = _danhmuctaothanhsanphamViewModel
                };
            }
            CurrentView = _danhmuctaothanhsanphamView;
        }
        private void ShowTonKho(object obj)
        {
            if (_tonKhoView == null)
            {
                _tonkhoViewModel = new TonKhoViewModel();
                _tonKhoView = new TonKhoView
                {
                    DataContext = _tonkhoViewModel
                };
            }
            CurrentView = _tonKhoView;
        }

        private void OpenManufacturerForm(object obj)
        {
            var viewModel = new ManufacturerFormViewModel(OnManufacturerSaved);
            var view = new ManufacturerFormView
            {
                DataContext = viewModel
            };
            CurrentView = view;
        }
        private void OnManufacturerSaved(ManufacturerModel newManufacturer)
{
    if (_manufacturerListViewModel == null)
    {
        _manufacturerListViewModel = new ManufacturerListViewModel();
        _manufacturerListView = new ManufacturerListView
        {
            DataContext = _manufacturerListViewModel
        };
    }

    _manufacturerListViewModel.Manufacturers.Add(newManufacturer);

    CurrentView = _manufacturerListView;
}
        private void ShowImportList()
        {
            if (_importListView == null)
            {
                _importListViewModel = new ImportListViewModel();
                _importListView = new ImportListView
                {
                    DataContext = _importListViewModel
                };
            }

            CurrentView = _importListView;
        }

        private void ShowImportCreate()
        {
            CurrentView = new ImportCreateView();
        }
        
    }
}