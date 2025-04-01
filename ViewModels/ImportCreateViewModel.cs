using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Models;
using System.Windows;
namespace QuanLyBanHang.ViewModels
{
    public class ImportCreateViewModel 
    {
        public Import NewImport { get; set; }

        public ICommand SaveCommand { get; }

        public ImportCreateViewModel()
        {
            NewImport = new Import
            {
                ImportDate = DateTime.Now
            };

            SaveCommand = new RelayCommand(_ => SaveImport(), _ => CanSave());
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(NewImport.ProductName)
                && NewImport.Quantity > 0
                && !string.IsNullOrEmpty(NewImport.Supplier);
        }

        private void SaveImport()
        {
            // Ở đây bạn có thể lưu vào cơ sở dữ liệu hoặc danh sách chung
            // Giả sử bạn thêm vào danh sách tạm hoặc điều hướng về danh sách
            System.Windows.MessageBox.Show("Đã lưu phiếu nhập kho!");
        }
    }
}