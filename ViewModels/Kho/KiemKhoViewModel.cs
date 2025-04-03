using QuanLyBanHang.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace QuanLyBanHang.ViewModels.Kho
{
    public class KiemKhoViewModel : INotifyPropertyChanged
    {
        public class NguyenVatLieu
        {
            public string MaNVL { get; set; }
            public string TenNguyenVatLieu { get; set; }
            public string NhomNguyenVatLieu { get; set; }
            public string DonViTinh { get; set; }
            public string TheoSo { get; set; }
            public string KiemKe { get; set; }
            public string ChenhLech { get; set; }
            public string NguyenNhan { get; set; }
        }

        private ObservableCollection<NguyenVatLieu> _danhSachNVL;
        public ObservableCollection<NguyenVatLieu> DanhSachNVL
        {
            get => _danhSachNVL;
            set
            {
                _danhSachNVL = value;
                OnPropertyChanged(nameof(DanhSachNVL));
            }
        }

        public ICommand ThemDongCommand { get; }
        public ICommand XoaDongCommand { get; }

        public KiemKhoViewModel()
        {
            DanhSachNVL = new ObservableCollection<NguyenVatLieu>
            {
                new NguyenVatLieu { MaNVL = "NguyenVanThang", TenNguyenVatLieu = "VuTrucLam", NhomNguyenVatLieu = "LyTieuHan", DonViTinh = "2PhanDucLoi", TheoSo = "NguyenHoangViet", KiemKe = "NguyenThiKimOanh", ChenhLech = "DangNguyenMinhNguet", NguyenNhan = "HongDiep" },
                new NguyenVatLieu { MaNVL = "HEEE", TenNguyenVatLieu = "Bia Sai GOn", NhomNguyenVatLieu = "Bia hoi", DonViTinh = "Cái", TheoSo = "1.877,00", KiemKe = "1.877,00", ChenhLech = "0,00", NguyenNhan = "Quản chữa nhập kho" },
                new NguyenVatLieu { MaNVL = "BHYETHE", TenNguyenVatLieu = "Bia hoi Vật Hà", NhomNguyenVatLieu = "Bia hoi", DonViTinh = "Cái", TheoSo = "1.895,00", KiemKe = "1.895,00", ChenhLech = "0,00", NguyenNhan = "Quản chữa nhập kho" },
                new NguyenVatLieu { MaNVL = "BTM", TenNguyenVatLieu = "Bia hoi Hà Phong", NhomNguyenVatLieu = "Bia tuoi", DonViTinh = "Cái", TheoSo = "1.500,00", KiemKe = "1.500,00", ChenhLech = "0,00", NguyenNhan = "" },
                new NguyenVatLieu { MaNVL = "UJDF", TenNguyenVatLieu = "Bia vàng 0.3L", NhomNguyenVatLieu = "Bia tuoi", DonViTinh = "Cái", TheoSo = "978,00", KiemKe = "978,00", ChenhLech = "0,00", NguyenNhan = "" }
            };
            System.Diagnostics.Debug.WriteLine($"Số lượng phần tử trong DanhSachNVL: {DanhSachNVL.Count}");

            ThemDongCommand = new RelayCommand(ThemDong);

            ThemDongCommand = new RelayCommand(ThemDong);
            XoaDongCommand = new RelayCommand(XoaDong);
        }

        private void ThemDong(object parameter)
        {
            DanhSachNVL.Add(new NguyenVatLieu
            {
                MaNVL = "",
                TenNguyenVatLieu = "",
                NhomNguyenVatLieu = "",
                DonViTinh = "",
                TheoSo = "0,00",
                KiemKe = "0,00",
                ChenhLech = "0,00",
                NguyenNhan = ""
            });
        }

        private void XoaDong(object parameter)
        {
            if (parameter is NguyenVatLieu selectedItem && DanhSachNVL.Contains(selectedItem))
            {
                DanhSachNVL.Remove(selectedItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
