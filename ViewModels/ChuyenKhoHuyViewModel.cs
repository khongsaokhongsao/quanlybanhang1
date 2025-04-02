using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.ViewModels
{
    public class QuanLyChuyenKhoHuyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ChuyenKhoHuyModel> DanhSachChuyenKhoHuy { get; set; }

        public QuanLyChuyenKhoHuyViewModel()
        {
            // Giả lập dữ liệu danh sách chuyển kho hủy
            DanhSachChuyenKhoHuy = new ObservableCollection<ChuyenKhoHuyModel>
            {
                new ChuyenKhoHuyModel { MaPhieu = "CKH001", KhoGui = "Kho A", KhoNhan = "Kho B", NgayChuyen = DateTime.Now.AddDays(-3), TrangThai = "Hoàn thành" },
                new ChuyenKhoHuyModel { MaPhieu = "CKH002", KhoGui = "Kho C", KhoNhan = "Kho D", NgayChuyen = DateTime.Now.AddDays(-1), TrangThai = "Đang xử lý" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChuyenKhoHuyModel
    {
        public string MaPhieu { get; set; }
        public string KhoGui { get; set; }
        public string KhoNhan { get; set; }
        public DateTime NgayChuyen { get; set; }
        public string TrangThai { get; set; }
    }
}
