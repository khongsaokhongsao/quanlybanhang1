using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class Quy
    {
        public int Id { get; set; }
        public int Bank { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }
        public int TotalTransaction { get; set; }
        public string Status { get; set; }
    }
}
