//using System;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Windows.Input;
//using System.Collections.ObjectModel;
//using QuanLyBanHang.Helpers;
//using QuanLyBanHang.Models;

//namespace QuanLyBanHang.ViewModels
//{
//    public class ManufacturerFormViewModel : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;
//        private static int _nextId = 3;

//        private string _id;
//        public string Id
//        {
//            get => _id;
//            set { _id = value; OnPropertyChanged(); }
//        }

//        private string _name;
//        public string Name
//        {
//            get => _name;
//            set { _name = value; OnPropertyChanged(); }
//        }

//        private string _address;
//        public string Address
//        {
//            get => _address;
//            set { _address = value; OnPropertyChanged(); }
//        }

//        private string _phoneNumber;
//        public string PhoneNumber
//        {
//            get => _phoneNumber;
//            set { _phoneNumber = value; OnPropertyChanged(); }
//        }

//        public ICommand SaveCommand { get; }

//        private readonly Action<ManufacturerModel> _onSavedCallback;

//        public ManufacturerFormViewModel(Action<ManufacturerModel> onSavedCallback)
//        {
//            _onSavedCallback = onSavedCallback;
//            SaveCommand = new RelayCommand(SaveManufacturer);
//        }

//        private void SaveManufacturer(object obj)
//        {
//            // Kiểm tra hợp lệ
//            if (string.IsNullOrWhiteSpace(Name))
//            {
//                ShowError("Vui lòng nhập tên nhà sản xuất.");
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(Address))
//            {
//                ShowError("Vui lòng nhập địa chỉ.");
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(PhoneNumber))
//            {
//                ShowError("Vui lòng nhập số điện thoại.");
//                return;
//            }

//            if (!PhoneNumber.All(char.IsDigit))
//            {
//                ShowError("Số điện thoại chỉ được chứa chữ số.");
//                return;
//            }

//            // Nếu hợp lệ thì tạo đối tượng mới và lưu
//            var newManufacturer = new ManufacturerModel
//            {
//                Id = _nextId.ToString(),
//                Name = this.Name,
//                Address = this.Address,
//                Phone = this.PhoneNumber
//            };
//            _nextId++;

//            _onSavedCallback?.Invoke(newManufacturer);

//            // Reset form
//            Id = string.Empty;
//            Name = string.Empty;
//            Address = string.Empty;
//            PhoneNumber = string.Empty;
//        }
//        private void ShowError(string message)
//        {
//            System.Windows.MessageBox.Show(message, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
//        }


//        private void OnPropertyChanged([CallerMemberName] string prop = "")
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
//        }
//    }
//}
