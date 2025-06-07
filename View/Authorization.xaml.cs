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
        AuthorizationLogic Auth = new AuthorizationLogic();
        public MainWindow()
        {
            Auth.NotifyRegistration += DisplayRegistration;
            Auth.NotifyClient += DisplayClient; //передача id
            Auth.NotifyAdmin += DisplayAdmin;
            Auth.NotifyReception += DisplayReception;
            Auth.NotifyService += DisplayService;
            Auth.NotifyWrongUser += DisplayMessage;
            DataContext = Auth;
            InitializeComponent();
        }

        private void DisplayRegistration()
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void DisplayClient(int id)
        {
            ClientMain reception = new ClientMain(id);
            reception.Closed += this.Showd;
            reception.Show();
            Hide();
        }

        private void Showd(object? sender, EventArgs e)
        {
            ClearBoxes();
            Show();
        }

        private void ClearBoxes()
        {
            Auth.Password = "";
            Auth.Login = "";
        }

        private void DisplayReception(int id)
        {
            Reception reception = new Reception(id);
            reception.Closed += this.Showd;
            reception.Show();
            Hide();
        }
        private void DisplayService(int id)
        {
            Service reception = new Service();
            reception.Closed += this.Showd;
            reception.Show();
            Hide();
        }
        private void DisplayAdmin(int id)
        {
            Administrator reception = new Administrator(id);
            reception.Closed += this.Showd;
            reception.Show();
            Hide();
        }

        private void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}