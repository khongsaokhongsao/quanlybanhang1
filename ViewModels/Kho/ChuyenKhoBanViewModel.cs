using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.ViewModels
{
    public class QuanLyChuyenKhoBanViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ChuyenKhoBanModel> DanhSachChuyenKho { get; set; }

        public QuanLyChuyenKhoBanViewModel()
        {
            // Giả lập dữ liệu danh sách chuyển kho bán
            DanhSachChuyenKho = new ObservableCollection<ChuyenKhoBanModel>
            {
                new ChuyenKhoBanModel { MaChuyenKho = "CK001", KhoXuat = "Kho A", KhoNhan = "Kho B", LiDoHuy = "Lỗi", NgayChuyen = DateTime.Now.AddDays(-2), TrangThai = "Đang xử lý", NguoiChuyen = "Nhân viên chuyển" },
                new ChuyenKhoBanModel { MaChuyenKho = "CK002", KhoXuat = "Kho B", KhoNhan = "Kho C", LiDoHuy = "Lỗi", NgayChuyen = DateTime.Now.AddDays(-5), TrangThai = "Hoàn thành", NguoiChuyen = "Nhân viên chuyển" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChuyenKhoBanModel
    {
        public string MaChuyenKho { get; set; }
        public string KhoXuat { get; set; }
        public string KhoNhan { get; set; }
        public DateTime NgayChuyen { get; set; }
        public string TrangThai { get; set; }
        public string NguoiChuyen { get; set; }
        public string LiDoHuy { get; set; }

    }
}
