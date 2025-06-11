using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для AddProblem.xaml
    /// </summary>
    public partial class AddProblem : Window
    {
        public AddProblem()
        {
            InitializeComponent();
            AddProblemViewModel addProblemViewModel = new AddProblemViewModel();
            addProblemViewModel.NotifyDisplay += Close;
            DataContext = addProblemViewModel;

            addProblemViewModel.NotifyError += ShowMessage;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
