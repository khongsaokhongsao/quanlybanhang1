using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.ViewModels
{
    public class ImportCreateViewModel : INotifyPropertyChanged
    {
        private static int _nextId = 5;
        private Import _newImport = new Import();
        private readonly Action<Import> _onImportSaved;
        private readonly Action _navigateToList;

        public Import NewImport
        {
            get => _newImport;
            set
            {
                _newImport = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public ImportCreateViewModel(Action<Import> onImportSaved, Action navigateToList)
        {
            _onImportSaved = onImportSaved;
            _navigateToList = navigateToList;
            SaveCommand = new RelayCommand(Save);
        }

        private void Save(object obj)
        {
            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrWhiteSpace(NewImport.ProductName))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NewImport.Quantity <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewImport.Supplier))
            {
                MessageBox.Show("Nhà cung cấp không được để trống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidDateFormat(NewImport.ImportDate))
            {
                MessageBox.Show("Ngày nhập không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Cấp phát ID tự động
            NewImport.Id = _nextId++;

            // Lưu dữ liệu
          
            _onImportSaved?.Invoke(NewImport);
            _navigateToList?.Invoke();
        }

        private bool IsValidDateFormat(string date)
        {
            return DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
