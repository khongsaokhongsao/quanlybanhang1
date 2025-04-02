using System;
using System.Windows.Controls;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Views
{
    public partial class CategoryFormView : UserControl
    {
        private readonly Category _category;
        private readonly Action<bool, Category> _onCompleted;

        public CategoryFormView(Category category, Action<bool, Category> onCompleted)
        {
            InitializeComponent();
            _category = category;
            _onCompleted = onCompleted;
            DataContext = _category;
        }
    }
}