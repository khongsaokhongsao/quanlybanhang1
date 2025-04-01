using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class Import
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImportDate { get; set; }
        public string Supplier { get; set; }
        
    }
}
