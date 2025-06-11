using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public delegate void ErrorsHandler(string message);
        public event ErrorsHandler? NotifyError;

        public delegate void DisplayWindowHandler();

    }
}
