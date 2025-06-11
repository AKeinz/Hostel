using Logic;
using System.Windows;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        RegistrationLogic registrationLogic = new RegistrationLogic();
        public Registration()
        {
            registrationLogic.NotifyMessage += DisplayMessage;
            registrationLogic.NotifyClose += Close;
            DataContext = registrationLogic;
            InitializeComponent();
        }

        private void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
