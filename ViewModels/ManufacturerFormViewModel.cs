using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace QuanLyBanHang.ViewModels
{
    public class ManufacturerFormViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Manufacturer> ManufacturerList { get; set; }

        public ICommand SaveCommand { get; }

        public ManufacturerFormViewModel()
        {
            ManufacturerList = new ObservableCollection<Manufacturer>();
            SaveCommand = new RelayCommand(SaveManufacturer);
        }

        private void SaveManufacturer(object obj)
        {
            ManufacturerList.Add(new Manufacturer
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                PhoneNumber = this.PhoneNumber
            });

            // Reset form
            Id = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            PhoneNumber = string.Empty;
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Manufacturer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public event EventHandler CanExecuteChanged { add { } remove { } }
        public void Execute(object parameter) => _execute(parameter);
    }
}
