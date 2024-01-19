using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.ViewModels
{
	public class RegisterWindowViewModel : ObservableObject, IDataErrorInfo
    {
        private double _weight = 0;
        private string? _from = null;
        private string? _to = null;

        private readonly RelayCommand _saveCommand;
        private readonly RelayCommand _cancelCommand;

        public RegisterWindowViewModel()
        {
            _saveCommand = new RelayCommand(Save, CanSave);
            _cancelCommand = new RelayCommand(Cancel);

        }

        public Action<NewOrderDto?>? CloseDelegate { get; set; }
        public List<LocationDto> AvailableLocations { get; set; }
        public double MaxAvailableTransportVolume { get; set; }
        public NewOrderDto CurrentOrder { get; set; }



        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public List<string> Locations
        {
            get => AvailableLocations.Select(x => x.Name).ToList();
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

        public string? From
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

        public string? To
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
                    if (Weight <= 0 || Weight > MaxAvailableTransportVolume)
                    {
                        validationMessage = Error;
                    }
                    break;
                case nameof(From):
                case nameof(To):
                    if (To == From)
                    {
                        validationMessage = Error;
                    }
                    break;
            }
            
            return validationMessage;
        }

        private void Save()
        {
            NewOrderDto order;

            if (CurrentOrder != null)
            {
                order = CurrentOrder;
                order.Weight = Weight;
                order.From = From!;
                order.To = To!;

            }
            else 
            {
                order = new NewOrderDto
                {
                    Weight = Weight,
                    From = From,
                    To = To
                };
            }

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
