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
                new ManufacturerModel { Id = 1, Name = "Công ty ABC", Address = "Hà Nội", Phone = "0123456789" },
                new ManufacturerModel { Id = 2, Name = "Công ty XYZ", Address = "TP.HCM", Phone = "0987654321" }
            };
        }
    }
}
