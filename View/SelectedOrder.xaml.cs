using Model;
using System.Windows;
using System.Windows.Input;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для SelectedOrder.xaml
    /// </summary>
    public partial class SelectedOrder : Window
    {
        public SelectedOrder(object order)
        {
            InitializeComponent();
            SelectedOrderViewModel selectedOrderViewModel = new SelectedOrderViewModel((Order)order);
            selectedOrderViewModel.NotifyDisplay += Close;
            selectedOrderViewModel.NotifyError += ShowMessage;
            DataContext = selectedOrderViewModel;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int people;
            if (!int.TryParse(e.Text, out people))
            {
                e.Handled = true;
            }
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
