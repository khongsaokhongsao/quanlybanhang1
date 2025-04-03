using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using QuanLyBanHang.Models;
using QuanLyBanHang.Repositories;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Views;
using System.ComponentModel;



namespace QuanLyBanHang.ViewModels
{
    public class QuyViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        private Quy _selectedQuy;
        private readonly QuyRepository _quyRepository;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ObservableCollection<Quy> Quys { get; set; }
        public Quy SelectedQuy
        {
            get => _selectedQuy;
            set
            {
                _selectedQuy = value;
                OnPropertyChanged(nameof(SelectedQuy));
            }
        }

        // Command để thêm, sửa, xóa quỹ
        public ICommand ShowQuyListCommand { get; set; }
        public ICommand AddQuyCommand { get; set; }
        //public ICommand EditQuyCommand { get; set; }
        //public ICommand DeleteQuyCommand { get; set; }
        public ICommand SaveQuyCommand { get; set; }

        public QuyViewModel()
        {
            _quyRepository = new QuyRepository();
            Quys = new ObservableCollection<Quy>(_quyRepository.GetAllQuy());

            // Khởi tạo các command
            ShowQuyListCommand = new RelayCommand(ShowQuyListView);
            AddQuyCommand = new RelayCommand(AddQuy);
            SaveQuyCommand = new RelayCommand(SaveQuy);

            // Mặc định hiển thị danh sách quỹ
            ShowQuyListView(null);
        }

        public void ShowQuyListView(object obj)
        {
            LoadQuys();
            //var quyListView = new QuyView
            //{
            //    DataContext = this
            //};
            //CurrentView = quyListView;
        }

        public void AddQuy(object obj)
        {
            SelectedQuy = new Quy();
            var quyFormView = new QuyFormView
            {
                DataContext = this
            };
            CurrentView = quyFormView;
        }



        private void SaveQuy(object obj)
        {
            try
            {
                // Kiểm tra ràng buộc cơ bản
                //SelectedQuy.Validate();

                // Kiểm tra quỹ đã tồn tại hay chưa
                var existingQuy = Quys.FirstOrDefault(q => q.Name == SelectedQuy.Name && q.Type == SelectedQuy.Type && q.Id != SelectedQuy.Id);
                if (existingQuy != null)
                {
                    throw new ArgumentException("Tên quỹ đã tồn tại.");
                }

                if (SelectedQuy.Id == 0) // Thêm quỹ mới
                {
                    //_quyRepository.AddQuy(SelectedQuy);
                    Quys.Add(SelectedQuy);
                }
                else // Cập nhật quỹ
                {
                    //_quyRepository.UpdateQuy(SelectedQuy);
                    var quyToUpdate = Quys.FirstOrDefault(q => q.Id == SelectedQuy.Id);
                    if (quyToUpdate != null)
                    {
                        quyToUpdate.Name = SelectedQuy.Name;
                        quyToUpdate.Type = SelectedQuy.Type;
                        quyToUpdate.TotalTransaction = SelectedQuy.TotalTransaction;
                        quyToUpdate.Status = SelectedQuy.Status;
                    }
                }

                // Quay lại danh sách quỹ sau khi lưu
                ShowQuyListView(null);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void LoadQuys()
        {
            try
            {
                Quys.Clear();
                var quyList = _quyRepository.GetAllQuy();
                foreach (var quy in quyList)
                {
                    Quys.Add(quy);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Lỗi khi tải danh sách quỹ: {ex.Message}", "Lỗi",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
