using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Repos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class RegisterWindowViewModel : ObservableObject, IDataErrorInfo
    {
        private double _weight = 0;
        private Location? _from = null;
        private Location? _to = null;

        private readonly RelayCommand _saveCommand;
        private readonly RelayCommand _cancelCommand;
        private readonly ILocationsRepo _locationsRepo;


        public RegisterWindowViewModel(ILocationsRepo locationsRepo)
        {
            _saveCommand = new RelayCommand(Save, CanSave);
            _cancelCommand = new RelayCommand(Cancel);
            _locationsRepo = locationsRepo;
        }

        public Action<Order?>? CloseDelegate { get; set; }


        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public List<Location> Locations
        {
            get => _locationsRepo.GetAll();
        }

        public double Weight
        {
            get => _weight;
            set
            {
                if (SetProperty(ref _weight, value))
                {
                    _saveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public Location? From
        {

            get => _from;
            set
            {
                if (SetProperty(ref _from, value))
                {
                    _saveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public Location? To
        {
            get => _to;
            set
            {
                if (SetProperty(ref _to, value))
                {
                    _saveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand
        {
            get => _saveCommand;
        }

        public ICommand CancelCommand
        {
            get => _cancelCommand;
        }

        public string Error { get { return "Ошибка ввода!"; } }

        private string Validate(string propertyName)
        {
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case nameof(Weight):
                    if (Weight <= 0)
                    {
                        validationMessage = Error;
                    }
                    break;
                case nameof(From):
                case nameof(To):
                    if (To?.Name == From?.Name)
                    {
                        validationMessage = Error;
                    }
                    break;
            }
            
            return validationMessage;
        }

        private void Save()
        {
            var order = new Order(Weight, From!, To!);

            CloseDelegate?.Invoke(order);
        }

        private bool CanSave()
        {
            var isValid = string.IsNullOrEmpty(Validate(nameof(Weight)))
                && string.IsNullOrEmpty(Validate(nameof(From)))
                && string.IsNullOrEmpty(Validate(nameof(To)));

            return isValid;
        }

        private void Cancel()
        {
            CloseDelegate?.Invoke(null);
        }
    }
}
