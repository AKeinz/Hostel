using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void CloseWin()
        {
            this.Close();
        }
    }
}
