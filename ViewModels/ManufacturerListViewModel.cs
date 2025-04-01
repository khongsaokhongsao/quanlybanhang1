using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.ViewModels
{
    public class ManufacturerListViewModel
    {
        public ObservableCollection<ManufacturerModel> Manufacturers { get; set; }

        public ManufacturerListViewModel()
        {
            // Dữ liệu mẫu (hoặc load từ DB)
            Manufacturers = new ObservableCollection<ManufacturerModel>
            {
                 new ManufacturerModel
                {
                    Id = "1",
                    Name = "Công ty TNHH ABC",
                    Address = "123 Đường A, Quận B, TP.HCM",
                    PhoneNumber = "0909123456"
                },
                new ManufacturerModel
                {
                    Id = "2",
                    Name = "Công ty cổ phần XYZ",
                    Address = "456 Đường C, Quận D, Hà Nội",
                    PhoneNumber = "0987654321"
                }
            };
        }
    }
}
