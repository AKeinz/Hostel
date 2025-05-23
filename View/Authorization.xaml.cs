using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseLayer;
using Logic;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AuthorizationLogic Logic = new AuthorizationLogic();
            Logic.NotifyRegistration += DisplayRegistration;
            Logic.NotifyMain += DisplayMain;
            DataContext = Logic;
            InitializeComponent();
        }

        private void DisplayRegistration()
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void DisplayMain()
        {
            ClientMain registration = new ClientMain();
            registration.Show();
        }
    }
}