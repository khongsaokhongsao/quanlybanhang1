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

        public ICommand ShowCategoryListCommand { get; set; }
        public ICommand ShowProductListForManagementCommand { get; set; }
        public ICommand AddProductForManagementCommand { get; set; }
        public ICommand ShowDanhmuctaothanhsanphamCommand { get; set; }
        public ICommand ShowTonKhoCommand { get; set; }
        //public ICommand OpenManufacturerFormCommand { get; }
        public ICommand ShowTimkiemsanphamViewCommand { get; set; }
        public ICommand ShowManufacturerListCommand { get; set; }
        public ICommand AddManufacturerCommand { get; set; }
        public ICommand OpenTonKhoBHCommand { get; }
        public ICommand OpenDanhSachNhanBHCommand { get; }
        public ICommand OpenDanhSachGuiBHCommand { get; }
        public ICommand OpenDanhSachTraBHCommand { get; }
        public ICommand OpenQuanLyChuyenKhoBanCommand { get; }
        public ICommand OpenQuanLyChuyenKhoHuyCommand { get; }
        public ICommand OpenQuanLyChuyenKhoSuDungCommand { get; }







        public ManufacturerListViewModel ManufacturerListViewModel { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }

        private UserManagementView userManagementView;
        private UserManagementViewModel userManagementViewModel;
        private ImportListView _importListView;
        private ImportListViewModel _importListViewModel;
        private ImportCreateView _importCreateView;
        private ImportCreateViewModel _importCreateViewModel;
        private ProductManagementView _productManagementView;
        private ProductManagementViewModel _productManagementViewModel;
        private ManufacturerListView _manufacturerListView;
        private ManufacturerListViewModel _manufacturerListViewModel;
        private DanhmuctaothanhsanphamView _danhmuctaothanhsanphamView;
        private DanhmuctaothanhsanphamViewModel _danhmuctaothanhsanphamViewModel;
        private CategoryListView _categoryListView;
        private CategoryListViewModel _categoryListViewModel;
        private TimkiemsanphamView _timkiemsanphamView;
        private TimkiemsanphamViewModel _timkiemsanphamViewModel;
        private TonKhoView _tonKhoView;
        private TonKhoViewModel _tonkhoViewModel;
        private TonKhoBHView _tonKhoBHView;
        private TonKhoBHViewModel _tonKhoBHViewModel;
        private DanhSachNhanBHView _danhSachNhanBHView;
        private DanhSachNhanBHViewModel _danhSachNhanBHViewModel;
        private DanhSachGuiBH _danhSachGuiBHView;
        private DanhSachGuiBHViewModel _danhSachGuiBHViewModel;
        private DanhSachTraBHView _danhSachTraBHView;
        private DanhSachTraBHViewModel _danhSachTraBHViewModel;
        private ChuyenKhoBanView _quanLyChuyenKhoBanView;
        private QuanLyChuyenKhoBanViewModel _quanLyChuyenKhoBanViewModel;
        private ChuyenKhoHuyView _quanLyChuyenKhoHuyView;
        private QuanLyChuyenKhoHuyViewModel _quanLyChuyenKhoHuyViewModel;
        private ChuyenKhoSuDungView _quanLyChuyenKhoSuDungView;
        private QuanLyChuyenKhoViewModel _quanLyChuyenKhoSuDungViewModel;





        public MainViewModel()
        {
            // Khởi tạo ProductManagementView và ViewModel
            _productManagementViewModel = new ProductManagementViewModel();
            _productManagementView = new ProductManagementView
            {
                DataContext = _productManagementViewModel
            };

            _categoryListViewModel = new CategoryListViewModel();
            _categoryListView = new CategoryListView
            {
                DataContext = _categoryListViewModel
            };
            _manufacturerListViewModel = new ManufacturerListViewModel();
            _manufacturerListView = new ManufacturerListView
            {
                DataContext = _manufacturerListViewModel
            };
            // Khởi tạo ManufacturerListViewModel
            ManufacturerListViewModel = new ManufacturerListViewModel(this);

            // Mặc định hiển thị danh sách nhà sản xuất
            CurrentView = ManufacturerListViewModel.CurrentView;

            // Khởi tạo các Command
            //OpenManufacturerFormCommand = new RelayCommand(OpenManufacturerForm);
            ShowUserManagementCommand = new RelayCommand(ShowUserManagement);
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowProductListCommand = new RelayCommand(ShowProductList);
            AddProductCommand = new RelayCommand(AddProduct);
            ShowManufacturerListCommand = new RelayCommand(_ => CurrentView = ManufacturerListViewModel.CurrentView);
            AddManufacturerCommand = new RelayCommand(AddManufacturer); // Thêm command mới

            ShowCategoryListCommand = new RelayCommand(ShowCategoryList);
            ShowProductListForManagementCommand = new RelayCommand(ShowProductListForManagement);
            AddProductForManagementCommand = new RelayCommand(AddProductForManagement);
            ShowDanhmuctaothanhsanphamCommand = new RelayCommand(ShowDanhmuctaothanhsanpham);
            ShowTonKhoCommand = new RelayCommand(ShowTonKho);
            ShowTimkiemsanphamViewCommand = new RelayCommand(ShowTimkiemsanpham);
            ShowImportCreateCommand = new RelayCommand(o => ShowImportCreate());
            ShowImportListCommand = new RelayCommand(o => ShowImportList());
            /////////////////////////////////////////
            OpenTonKhoBHCommand = new RelayCommand(o => OpenTonKhoBH());
            OpenDanhSachNhanBHCommand = new RelayCommand(o => OpenDanhSachNhanBH());
            OpenDanhSachGuiBHCommand = new RelayCommand(o => OpenDanhSachGuiBH());
            OpenDanhSachTraBHCommand = new RelayCommand(o => OpenDanhSachTraBH());
            OpenQuanLyChuyenKhoBanCommand = new RelayCommand(o => OpenQuanLyChuyenKhoBan());
            OpenQuanLyChuyenKhoHuyCommand = new RelayCommand(o => OpenQuanLyChuyenKhoHuy());
            OpenQuanLyChuyenKhoSuDungCommand = new RelayCommand(o => OpenQuanLyChuyenKhoSuDung());




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
        // Phương thức công khai để cập nhật danh sách danh mục trong ProductManagementViewModel
        public void UpdateProductManagementCategories()
        {
            if (_productManagementViewModel != null)
            {
                var categoryRepository = new CategoryRepository();
                _productManagementViewModel.Categories.Clear();
                var updatedCategories = categoryRepository.GetAllCategories();
                foreach (var category in updatedCategories)
                {
                    _productManagementViewModel.Categories.Add(category);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        private void OpenDanhSachNhanBH()
        {
            if (_danhSachNhanBHView == null)
            {
                _danhSachNhanBHViewModel = new DanhSachNhanBHViewModel();
                _danhSachNhanBHView = new DanhSachNhanBHView { DataContext = _danhSachNhanBHViewModel };
            }
            CurrentView = _danhSachNhanBHView;
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
        private void ShowUserManagement(object obj)
        {
            CurrentView = new UserManagementView();
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
            CurrentView = _manufacturerListView;
            _manufacturerListViewModel.ShowManufacturerList(null); // Gọi để đảm bảo hiển thị danh sách
        }

        private void AddManufacturer(object obj)
        {
            // Tạo một instance mới của ManufacturerListViewModel nếu chưa có
            //if (_manufacturerListViewModel == null)
            //{
            //    _manufacturerListViewModel = new ManufacturerListViewModel();
            //    _manufacturerListView = new ManufacturerListView
            //    {
            //        DataContext = _manufacturerListViewModel
            //    };
            //}

            // Mở trực tiếp ManufacturerFormView
            var manufacturer = new ManufacturerModel();
            var formView = new ManufacturerFormView
            {
                DataContext = _manufacturerListViewModel
            };
            _manufacturerListViewModel.Manufacturer = manufacturer; // Gán đối tượng để binding
            CurrentView = formView;
        }


        private void ShowCategoryList(object obj)
        {
            CurrentView = _categoryListView;
            _categoryListViewModel.ShowCategoryList(null); // Gọi để đảm bảo hiển thị danh sách
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
            if (
                _tonKhoView == null)
            {
                _tonkhoViewModel = new TonKhoViewModel();
                _tonKhoView = new TonKhoView
                {
                    DataContext = _tonkhoViewModel
                };
            }
            CurrentView = _tonKhoView;
        }

        //private void OpenManufacturerForm(object obj)
        //{
        //    var viewModel = new ManufacturerFormViewModel(OnManufacturerSaved);
        //    var view = new ManufacturerFormView
        //    {
        //        DataContext = viewModel
        //    };
        //    CurrentView = view;
        //}
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
        // Hàm để quay về danh sách nhập kho
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
            _importCreateViewModel = new ImportCreateViewModel(OnImportSaved, ShowImportList);
            _importCreateView = new ImportCreateView
            {
                DataContext = _importCreateViewModel
            };
            CurrentView = _importCreateView;
        }
        // Hàm này sẽ được gọi khi lưu phiếu nhập
        private void OnImportSaved(Import newImport)
        {
            if (_importListViewModel == null)
            {
                _importListViewModel = new ImportListViewModel();
                _importListView = new ImportListView
                {
                    DataContext = _importListViewModel
                };
            }

            _importListViewModel.Imports.Add(newImport);
        }

    }
}