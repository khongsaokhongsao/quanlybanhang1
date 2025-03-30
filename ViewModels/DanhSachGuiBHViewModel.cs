using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.ViewModels
{
    public class DanhSachGuiBHViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DanhSachGuiBHModel> DanhSachGuiBH { get; set; }

        public DanhSachGuiBHViewModel()
        {
            // Giả lập dữ liệu ban đầu
            DanhSachGuiBH = new ObservableCollection<DanhSachGuiBHModel>
            {
                new DanhSachGuiBHModel { MaSanPham = "SP001", TenSanPham = "Laptop Dell", SoLuong = 2, TinhTrang = "Lỗi phần cứng", NgayGui = DateTime.Now.AddDays(-5), TrangThaiBaoHanh = "Đang xử lý", NguoiGui = "Cty LanPhuong" },
                new DanhSachGuiBHModel { MaSanPham = "SP002", TenSanPham = "iPhone 13", SoLuong = 1, TinhTrang = "Không nhận sạc", NgayGui = DateTime.Now.AddDays(-2), TrangThaiBaoHanh = "Đã hoàn thành", NguoiGui = "Cty KimOanh" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DanhSachGuiBHModel
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayGui { get; set; }
        public string TrangThaiBaoHanh { get; set; }
        public string NguoiGui { get; set; }

    }
}
