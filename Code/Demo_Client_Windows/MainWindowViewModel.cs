using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Demo_Client_Windows
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private HttpClient httpClient;

        public MainWindowViewModel()
        {
            this.httpClient = new HttpClient();
        }

        public UserControl DynamicContent
        {
            get;
            private set;
        }

        public ICommand LoadDllCommand
        {
            get
            {
                return new Command(async p =>
                {
                    var response = await httpClient.GetAsync("https://localhost:5001/api/Dll/load");
                    var assemblyBytes = await response.Content.ReadAsByteArrayAsync();
                    var assembly = Assembly.Load(assemblyBytes);

                    var control = assembly.GetTypes().FirstOrDefault(t => t.IsAssignableTo(typeof(UserControl)));

                    var instance = Activator.CreateInstance(control) as UserControl ?? throw new Exception("CAnt create instance");

                    this.DynamicContent = instance;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DynamicContent)));
                },
                p => true);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
