using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFTools
{
    public class PhoneControlViewModel : INotifyPropertyChanged
    {
        private string number;

        public PhoneControlViewModel()
        {
            this.number = "";
        }

        public ICommand CallNumberCommand { get
            {
                return new Command((_) =>
                {
                    MessageBox.Show($"Nummer {this.Number} wird angerufen..");
                },
                _=> !string.IsNullOrWhiteSpace(this.Number));
            } 
        }

        public string Number
        {
            get
            {
                return this.number;
            }

            set
            {
                this.number = value;
                this.RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
