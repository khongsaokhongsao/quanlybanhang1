using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }

        // Kiểm tra ràng buộc khi tạo hoặc cập nhật sản phẩm
        public void Validate()
        {
            // Kiểm tra tên sản phẩm
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Tên sản phẩm không được để trống.");

            // Kiểm tra giá bán
            if (Price <= 0)
                throw new ArgumentException("Giá bán phải là số dương và hợp lệ.");

            // Kiểm tra tồn kho
            if (Stock < 0)
                throw new ArgumentException("Tồn kho phải là số nguyên không âm.");
        }
    }
}

