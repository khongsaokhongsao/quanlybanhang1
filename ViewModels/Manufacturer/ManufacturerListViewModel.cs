using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Views;
using QuanLyBanHang.Repositories;
using System.ComponentModel;
using System.Windows;

namespace QuanLyBanHang.ViewModels
{
    public class ManufacturerListViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        private ManufacturerModel _selectedManufacturer;
        private readonly ManufacturerRepository _manufacturerRepository;
        private readonly MainViewModel _mainViewModel;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
                // Cập nhật CurrentView của MainViewModel
                if (_mainViewModel != null)
                {
                    _mainViewModel.CurrentView = _currentView;
                }
            }
        }

        public ObservableCollection<ManufacturerModel> Manufacturers { get; set; }
        public ManufacturerModel Manufacturer { get; set; }

        public ManufacturerModel SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged(nameof(SelectedManufacturer));
            }
        }

        // Thuộc tính cho lọc nhà sản xuất
        private string _searchName;
        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
            }
        }

        private string _searchPhone;
        public string SearchPhone
        {
            get => _searchPhone;
            set
            {
                _searchPhone = value;
                OnPropertyChanged(nameof(SearchPhone));
            }
        }

        public ICommand ShowManufacturerListCommand { get; set; }
        public ICommand AddManufacturerCommand { get; set; }
        public ICommand EditManufacturerCommand { get; set; }
        public ICommand DeleteManufacturerCommand { get; set; }
        public ICommand SaveManufacturerCommand { get; set; }
        public ICommand FilterManufacturersCommand { get; set; }

        public ManufacturerListViewModel(MainViewModel mainViewModel = null)
        {
            _mainViewModel = mainViewModel;
            _manufacturerRepository = new ManufacturerRepository();
            Manufacturers = new ObservableCollection<ManufacturerModel>(_manufacturerRepository.GetAllManufacturers());

            ShowManufacturerListCommand = new RelayCommand(ShowManufacturerList);
            AddManufacturerCommand = new RelayCommand(AddManufacturer);
            EditManufacturerCommand = new RelayCommand(EditManufacturer);
            DeleteManufacturerCommand = new RelayCommand(DeleteManufacturer);
            SaveManufacturerCommand = new RelayCommand(SaveManufacturer);
            FilterManufacturersCommand = new RelayCommand(_ => FilterManufacturers(null));

            ShowManufacturerList(null);
        }

        public void ShowManufacturerList(object obj)
        {
            // Tải lại danh sách nhà sản xuất từ cơ sở dữ liệu
            LoadManufacturers();

            var manufacturerListView = new ManufacturerListView
            {
                DataContext = this
            };
            CurrentView = manufacturerListView;
        }

        public void AddManufacturer(object obj)
        {
            Manufacturer = new ManufacturerModel();
            var manufacturerFormView = new ManufacturerFormView
            {
                DataContext = this
            };
           
            CurrentView = manufacturerFormView;
            
        }

        public void EditManufacturer(object obj)
        {
            if (obj is ManufacturerModel manufacturer) // Kiểm tra obj là ManufacturerModel
            {
                Manufacturer = new ManufacturerModel
                {
                    Id = manufacturer.Id,
                    Name = manufacturer.Name,
                    Address = manufacturer.Address,
                    Phone = manufacturer.Phone
                };
                var manufacturerFormView = new ManufacturerFormView
                {
                    DataContext = this
                };
                CurrentView = manufacturerFormView;
            }
        }
        //public void EditManufacturer(object obj)
        //{
        //    MessageBox.Show("Nút Sửa được nhấn!", "Test", MessageBoxButton.OK, MessageBoxImage.Information);
        //}

        public void DeleteManufacturer(object obj)
        {
            if (obj is ManufacturerModel manufacturer)
            {
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà sản xuất '{manufacturer.Name}' không?",
                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _manufacturerRepository.DeleteManufacturer(manufacturer.Id);
                        Manufacturers.Remove(manufacturer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa nhà sản xuất: {ex.Message}", "Lỗi",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SaveManufacturer(object obj)
        {
            try
            {
                // Kiểm tra ràng buộc cơ bản
                Manufacturer.Validate();

                // Kiểm tra tên nhà sản xuất trùng lặp
                var existingManufacturer = Manufacturers.FirstOrDefault(m => m.Name == Manufacturer.Name && m.Id != Manufacturer.Id);
                if (existingManufacturer != null)
                {
                    throw new ArgumentException("Tên nhà sản xuất đã tồn tại.");
                }

                if (Manufacturer.Id == 0) // Thêm nhà sản xuất mới
                {
                    _manufacturerRepository.AddManufacturer(Manufacturer);
                    Manufacturers.Add(Manufacturer);
                }
                else // Cập nhật nhà sản xuất
                {
                    _manufacturerRepository.UpdateManufacturer(Manufacturer);
                    var manufacturerToUpdate = Manufacturers.FirstOrDefault(m => m.Id == Manufacturer.Id);
                    if (manufacturerToUpdate != null)
                    {
                        manufacturerToUpdate.Name = Manufacturer.Name;
                        manufacturerToUpdate.Address = Manufacturer.Address;
                        manufacturerToUpdate.Phone = Manufacturer.Phone;
                    }
                    
            }
                MessageBox.Show("Lưu nhà sản xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                // Quay lại danh sách nhà sản xuất sau khi lưu
                ShowManufacturerList(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadManufacturers()
        {
            try
            {
                // Xóa danh sách hiện tại để tránh trùng lặp
                Manufacturers.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và thêm vào danh sách
                var manufacturerList = _manufacturerRepository.GetAllManufacturers();
                foreach (var manufacturer in manufacturerList)
                {
                    Manufacturers.Add(manufacturer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhà sản xuất: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterManufacturers(object obj)
        {
            try
            {
                var filteredManufacturers = _manufacturerRepository.GetAllManufacturers();

                // Lọc theo tên
                if (!string.IsNullOrWhiteSpace(SearchName))
                {
                    filteredManufacturers = filteredManufacturers
                        .Where(m => m.Name.ToLower().Contains(SearchName.ToLower()))
                        .ToList();
                }

                // Lọc theo số điện thoại
                if (!string.IsNullOrWhiteSpace(SearchPhone))
                {
                    filteredManufacturers = filteredManufacturers
                        .Where(m => m.Phone != null && m.Phone.Contains(SearchPhone))
                        .ToList();
                }

                // Cập nhật danh sách hiển thị
                Manufacturers.Clear();
                foreach (var manufacturer in filteredManufacturers)
                {
                    Manufacturers.Add(manufacturer);
                }

                // Ghi log để kiểm tra
                Console.WriteLine($"SearchName: {SearchName}, SearchPhone: {SearchPhone}");
                foreach (var manufacturer in filteredManufacturers)
                {
                    Console.WriteLine($"Kết quả: {manufacturer.Name} - {manufacturer.Address} - {manufacturer.Phone}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc nhà sản xuất: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}