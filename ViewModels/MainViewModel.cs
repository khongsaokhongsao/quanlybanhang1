using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Models;
using QuanLyBanHang.Repositories;
using QuanLyBanHang.Views;

namespace QuanLyBanHang.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowUserManagementCommand { get; }
        public ICommand ShowProductListCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand ShowManufacturerListCommand { get; }
        public ICommand ShowCategoryListCommand { get; }
        public ICommand ShowProductListForManagementCommand { get; }
        public ICommand AddProductForManagementCommand { get; }
        public ICommand ShowImportCreateCommand { get; }
        public ICommand ShowImportListCommand { get; }
        public ICommand ShowDanhmuctaothanhsanphamCommand { get; set; }
        public ICommand ShowTonKhoCommand { get; set; }

        public ICommand OpenManufacturerFormCommand { get; }
        public ICommand OpenTonKhoBHCommand { get; }
        public ICommand OpenDanhSachNhanBHCommand { get; }
        public ICommand OpenDanhSachGuiBHCommand { get; }
        public ICommand OpenDanhSachTraBHCommand { get; }
        public ICommand OpenQuanLyChuyenKhoBanCommand { get; }
        public ICommand OpenQuanLyChuyenKhoHuyCommand { get; }
        public ICommand OpenQuanLyChuyenKhoSuDungCommand { get; }
      


        public ObservableCollection<UserModel> Users { get; set; }

        private UserManagementView userManagementView;
        private UserManagementViewModel userManagementViewModel;
        private ImportListView _importListView;
        private ImportListViewModel _importListViewModel;

        private ProductManagementView _productManagementView;
        private ProductManagementViewModel _productManagementViewModel;
        private ManufacturerListView _manufacturerListView;
        private ManufacturerListViewModel _manufacturerListViewModel;
        private ManufacturerFormViewModel _manufacturerFormViewModel;
        private ManufacturerFormView _manufacturerFormView;
        private TonKhoBHView _tonKhoBHView;
        private TonKhoBHViewModel _tonKhoBHViewModel;
        private DanhSachNhanBHView _danhSachNhanBHView;
        private DanhSachNhanBHViewModel _danhSachNhanBHViewModel;
        private DanhSachGuiBH  _danhSachGuiBHView;
        private DanhSachGuiBHViewModel _danhSachGuiBHViewModel;
        private DanhSachTraBHView _danhSachTraBHView;
        private DanhSachTraBHViewModel _danhSachTraBHViewModel;
        private ChuyenKhoBanView _quanLyChuyenKhoBanView;
        private QuanLyChuyenKhoBanViewModel _quanLyChuyenKhoBanViewModel;
        private ChuyenKhoHuyView _quanLyChuyenKhoHuyView;
        private QuanLyChuyenKhoHuyViewModel _quanLyChuyenKhoHuyViewModel;
        private ChuyenKhoSuDungView _quanLyChuyenKhoSuDungView;
        private QuanLyChuyenKhoViewModel _quanLyChuyenKhoSuDungViewModel;
        private DanhmuctaothanhsanphamView _danhmuctaothanhsanphamView;
        private DanhmuctaothanhsanphamViewModel _danhmuctaothanhsanphamViewModel;
        private TonKhoView _tonKhoView;
        private TonKhoViewModel _tonkhoViewModel;



        public MainViewModel()
        {
            _productManagementViewModel = new ProductManagementViewModel();
            _productManagementView = new ProductManagementView { DataContext = _productManagementViewModel };

            OpenTonKhoBHCommand = new RelayCommand(o => OpenTonKhoBH());
            OpenDanhSachNhanBHCommand = new RelayCommand(o => OpenDanhSachNhanBH());
            OpenDanhSachGuiBHCommand = new RelayCommand(o => OpenDanhSachGuiBH());
            OpenDanhSachTraBHCommand = new RelayCommand(o => OpenDanhSachTraBH());
            OpenQuanLyChuyenKhoBanCommand = new RelayCommand(o => OpenQuanLyChuyenKhoBan());
            OpenQuanLyChuyenKhoHuyCommand = new RelayCommand(o => OpenQuanLyChuyenKhoHuy());
            OpenQuanLyChuyenKhoSuDungCommand = new RelayCommand(o => OpenQuanLyChuyenKhoSuDung());
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

            Users = new ObservableCollection<UserModel>();
            CurrentView = new WelcomeView(); // Giao diện mặc định
        }
        private void OpenManufacturerForm(object obj)
        {
            var viewModel = new ManufacturerFormViewModel(OnManufacturerSaved);
            CurrentView = new ManufacturerFormView { DataContext = viewModel };
        }

        private void OnManufacturerSaved(ManufacturerModel newManufacturer)
        {
            var viewModel = new ManufacturerListViewModel();
            viewModel.Manufacturers.Add(newManufacturer);
            CurrentView = new ManufacturerListView { DataContext = viewModel };
        }

        private void OpenTonKhoBH()
        {
            if (_tonKhoBHView == null)
            {
                _tonKhoBHViewModel = new TonKhoBHViewModel();
                _tonKhoBHView = new TonKhoBHView { DataContext = _tonKhoBHViewModel };
            }
            CurrentView = _tonKhoBHView;
        }

        private void OpenDanhSachNhanBH()
        {
            if (_danhSachNhanBHView == null)
            {
                _danhSachNhanBHViewModel = new DanhSachNhanBHViewModel();
                _danhSachNhanBHView = new DanhSachNhanBHView { DataContext = _danhSachNhanBHViewModel };
            }
            CurrentView = _danhSachNhanBHView;
        }

        private void OpenDanhSachGuiBH()
        {
            if (_danhSachGuiBHView == null)
            {
                _danhSachGuiBHViewModel = new DanhSachGuiBHViewModel();
                _danhSachGuiBHView = new DanhSachGuiBH { DataContext = _danhSachGuiBHViewModel };
            }
            CurrentView = _danhSachGuiBHView;
        }
        private void OpenDanhSachTraBH()
        {
            if (_danhSachTraBHView == null)
            {
                _danhSachTraBHViewModel = new DanhSachTraBHViewModel();
                _danhSachTraBHView = new DanhSachTraBHView { DataContext = _danhSachTraBHViewModel };
            }
            CurrentView = _danhSachTraBHView;
        }
        private void OpenQuanLyChuyenKhoBan()
        {
            if (_quanLyChuyenKhoBanView == null)
            {
                _quanLyChuyenKhoBanViewModel = new QuanLyChuyenKhoBanViewModel();
                _quanLyChuyenKhoBanView = new ChuyenKhoBanView { DataContext = _quanLyChuyenKhoBanViewModel };
            }
            CurrentView = _quanLyChuyenKhoBanView;
        }
        private void OpenQuanLyChuyenKhoHuy()
        {
            if (_quanLyChuyenKhoHuyView == null)
            {
                _quanLyChuyenKhoHuyViewModel = new QuanLyChuyenKhoHuyViewModel();
                _quanLyChuyenKhoHuyView = new ChuyenKhoHuyView { DataContext = _quanLyChuyenKhoHuyViewModel };
            }
            CurrentView = _quanLyChuyenKhoHuyView;
        }
        private void OpenQuanLyChuyenKhoSuDung()
        {
            if (_quanLyChuyenKhoSuDungView == null)
            {
                _quanLyChuyenKhoSuDungViewModel = new QuanLyChuyenKhoViewModel();
                _quanLyChuyenKhoSuDungView = new ChuyenKhoSuDungView { DataContext = _quanLyChuyenKhoSuDungViewModel };
            }
            CurrentView = _quanLyChuyenKhoSuDungView;
        }


        private void ShowUserManagement(object obj)
        {
            if (userManagementView == null)
            {
                userManagementViewModel = new UserManagementViewModel();
                userManagementView = new UserManagementView { DataContext = userManagementViewModel };
            }
            CurrentView = userManagementView;
        }

        private void ShowHome(object obj)
        {
            CurrentView = new WelcomeView();
        }

        private void ShowProductList(object obj)
        {
            throw new NotImplementedException("Chức năng Quản lý nhập hàng chưa được triển khai.");
            CurrentView = _productManagementView;
            _productManagementViewModel.ShowProductList(null);

        }

        private void AddProduct(object obj)
        {
            throw new NotImplementedException("Chức năng Quản lý nhập hàng chưa được triển khai.");
            CurrentView = _productManagementView;
            _productManagementViewModel.AddProduct(null);
        }

        private void ShowManufacturerList(object obj)
        {
            if (_manufacturerListView == null)
            {
                _manufacturerListViewModel = new ManufacturerListViewModel();
                _manufacturerListView = new ManufacturerListView { DataContext = _manufacturerListViewModel };
            }
            CurrentView = _manufacturerListView;
        }

        private void ShowCategoryList(object obj)
        {
            throw new NotImplementedException("Chức năng Quản lý nhập hàng chưa được triển khai.");
        }

        private void ShowProductListForManagement(object obj)
        {
            CurrentView = _productManagementView;
            _productManagementViewModel.ShowProductList(null);
        }

        private void AddProductForManagement(object obj)
        {
            CurrentView = _productManagementView;
            _productManagementViewModel.AddProduct(null);
        }

      

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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