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
        private bool isIntegrityVerificationEnabled;
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

        public bool IsIntegrityVerificationEnabled
        {
            get
            {
                return this.isIntegrityVerificationEnabled;
            }

            set
            {
                this.isIntegrityVerificationEnabled = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsIntegrityVerificationEnabled)));
            }
        }

        public ICommand LoadDllCommand
        {
            get
            {
                return new Command(async p =>
                {
                    var response = await httpClient.GetAsync("https://localhost:5001/api/Dll/load");
                    var assemblyBytes = await response.Content.ReadAsByteArrayAsync();

                    if (isIntegrityVerificationEnabled)
                    {
                        var hash = ComputeHash(assemblyBytes);

                        if (hash != 1118940902)
                        {
                            var result = MessageBox.Show("Warning, the calculated hash for the loaded assembly did not match the expected hash value of \"1118940902\". Please ensure that you trust the assembly before proceeding, otherwise terminate the execution of this program. Press OK to continue despite this warning, at your own risk of course ;)", "Warning, file hashes do not match", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                       
                            if (result != MessageBoxResult.OK)
                            {
                                return;
                            }
                        }
                    }

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

        private int ComputeHash(params byte[] data)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < data.Length; i++)
                    hash = (hash ^ data[i]) * p;

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }
    }
}
