using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.ViewModels
{
    public class QuanLyChuyenKhoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ChuyenKhoModel> DanhSachChuyenKho { get; set; }

        public QuanLyChuyenKhoViewModel()
        {
            // Giả lập dữ liệu chuyển kho
            DanhSachChuyenKho = new ObservableCollection<ChuyenKhoModel>
            {
                new ChuyenKhoModel { MaSanPham = "SP001", TenSanPham = "Laptop Asus", KhoNguon = "Kho A", KhoDich = "Kho B", SoLuong = 10, NgayChuyen = DateTime.Now.AddDays(-2), TrangThai = "Hoàn tất" },
                new ChuyenKhoModel { MaSanPham = "SP002", TenSanPham = "Điện thoại Samsung", KhoNguon = "Kho C", KhoDich = "Kho D", SoLuong = 5, NgayChuyen = DateTime.Now.AddDays(-5), TrangThai = "Đang xử lý" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChuyenKhoModel
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string KhoNguon { get; set; }
        public string KhoDich { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayChuyen { get; set; }
        public string TrangThai { get; set; }
    }
}
