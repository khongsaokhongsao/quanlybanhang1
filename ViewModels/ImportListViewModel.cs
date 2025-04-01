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
        public ObservableCollection<Import> Imports { get; set; }

        public ImportListViewModel()
        {
            // Dữ liệu mẫu
            Imports = new ObservableCollection<Import>
            {
                new Import { Id = 1, ProductName = "Bút bi", Quantity = 100, ImportDate = DateTime.Now, Supplier = "Nhà cung cấp A" },
                new Import { Id = 2, ProductName = "Tập vở", Quantity = 50, ImportDate = DateTime.Now.AddDays(-1), Supplier = "Nhà cung cấp B" },
            };
        }
    }
}
