using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.ViewModels
{
   public class DanhSachTraBHViewModel : INotifyPropertyChanged
   {
        public ObservableCollection<TraBHModel> DanhSachTraBH { get; set; }

        public DanhSachTraBHViewModel()
        {
            // Giả lập dữ liệu danh sách trả bảo hành
            DanhSachTraBH = new ObservableCollection<TraBHModel>
            {
                new TraBHModel { MaSanPham = "SP001", TenSanPham = "Laptop Asus", KhachHang = "Nguyễn Văn A", NgayTra = DateTime.Now, TrangThai = "Đã trả khách" },
                new TraBHModel { MaSanPham = "SP002", TenSanPham = "Điện thoại iPhone", KhachHang = "Trần Thị B", NgayTra = DateTime.Now.AddDays(-3), TrangThai = "Đã trả khách" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
   }

    public class TraBHModel
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string KhachHang { get; set; }
        public DateTime NgayTra { get; set; }
        public string TrangThai { get; set; }
    }
}
