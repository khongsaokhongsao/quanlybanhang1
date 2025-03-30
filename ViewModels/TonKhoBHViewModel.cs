using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.ViewModels
{
    public class TonKhoBHViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TonKhoBHModel> _sanPhamTonKho;
        public ObservableCollection<TonKhoBHModel> SanPhamTonKho
        {
            get { return _sanPhamTonKho; }
            set
            {
                _sanPhamTonKho = value;
                OnPropertyChanged(nameof(SanPhamTonKho));
            }
        }

        public TonKhoBHViewModel()
        {
            // Giả lập dữ liệu ban đầu
            SanPhamTonKho = new ObservableCollection<TonKhoBHModel>
            {
                new TonKhoBHModel { MaSanPham = "SP001", TenSanPham = "Laptop Asus", SoLuong = 5, TinhTrang = "Đang bảo hành", NgayNhap = DateTime.Now.AddDays(-10), NgayHetBaoHanh = DateTime.Now.AddMonths(3) },
                new TonKhoBHModel { MaSanPham = "SP002", TenSanPham = "Điện thoại Samsung", SoLuong = 3, TinhTrang = "Hoàn thành", NgayNhap = DateTime.Now.AddDays(-5), NgayHetBaoHanh = DateTime.Now.AddMonths(1) },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TonKhoBHModel
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayNhap { get; set; }
        public DateTime NgayHetBaoHanh { get; set; }
    }
}
