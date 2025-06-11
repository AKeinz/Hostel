using System.Windows;
using System.Windows.Input;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
            AddOrderViewModel addOrderViewModel = new AddOrderViewModel();
            addOrderViewModel.NotifyDisplay += Close;
            DataContext = addOrderViewModel;

            addOrderViewModel.NotifyError += ShowMessage;
        }
        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int people;
            if (!int.TryParse(e.Text, out people))
            {
                e.Handled = true;
            }
        }
    }
}
