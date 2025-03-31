using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.ViewModels
{
    public class ImportListViewModel
    {
        private static int _nextId = 1; // ID tự động tăng

        public ObservableCollection<Import> Imports { get; set; }

        public ImportListViewModel()
        {
            Imports = new ObservableCollection<Import>
            {
                new Import { Id = _nextId++, ProductName = "Bàn phím cơ", Quantity = 10, Supplier = "Công ty ABC", ImportDate = "20/03/2024" },
                new Import { Id = _nextId++, ProductName = "Chuột gaming", Quantity = 15, Supplier = "Công ty XYZ", ImportDate = "22/03/2024" },
                new Import { Id = _nextId++, ProductName = "Màn hình 24 inch", Quantity = 5, Supplier = "Nhà phân phối DEF", ImportDate = "25/03/2024" },
                new Import { Id = _nextId++, ProductName = "Tai nghe không dây", Quantity = 20, Supplier = "Nhà cung cấp GHI", ImportDate = "28/03/2024" }
            };
        }
    }
}
