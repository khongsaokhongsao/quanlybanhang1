﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class Quy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
        public double TotalTransaction { get; set; }
        public string Status { get; set; }
    }
}
