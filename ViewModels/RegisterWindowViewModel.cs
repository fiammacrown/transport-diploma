using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Abeslamidze_Kursovaya7.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class RegisterWindowViewModel : ObservableObject, IDataErrorInfo
    {
        private double _weight = 0;
        private Location _from = Location.Unknown;
        private Location _to = Location.Unknown;
        private List<Location> _locations = Enum.GetValues(typeof(Location)).Cast<Location>().ToList();

        private RelayCommand _saveCommand;

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public List<Location> Locations
        {
            get => _locations;

        }

        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public Location From
        {

            get => _from;
            set => SetProperty(ref _from, value);
        }

        public Location To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(Save, CanSave);
                }
                return _saveCommand;
            }
        }

        public string Error { get { return "Ошибка ввода!"; } }


        private bool _isValid;

        private string Validate(string propertyName)
        {
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case nameof(Weight):
                    if (Weight < 0)
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

            _isValid = string.IsNullOrEmpty(validationMessage);
            _saveCommand.NotifyCanExecuteChanged();

            return validationMessage;
        }

        private void Save()
        {
            //  TODO:
        }

        private bool CanSave()
        {
            return _isValid;
        }
    }
}
