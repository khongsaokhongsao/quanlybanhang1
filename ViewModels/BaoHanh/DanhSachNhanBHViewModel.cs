using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.ViewModels
{
    public class DanhSachNhanBHViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BaoHanhModel> DanhSachNhanBH { get; set; }

        public DanhSachNhanBHViewModel()
        {
            DanhSachNhanBH = new ObservableCollection<BaoHanhModel>
            {
                new BaoHanhModel { MaSP = "SP001", TenSP = "Laptop Dell", LyDoNhan = "Lỗi màn hình", TenKhachHang = "Nguyễn Văn A", NgayNhan = "2025-03-28", TrangThai = "Đang xử lý" },
                new BaoHanhModel { MaSP = "SP002", TenSP = "Bàn phím cơ", LyDoNhan = "Kẹt phím", TenKhachHang = "Trần Thị B", NgayNhan = "2025-03-26", TrangThai = "Hoàn thành" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
