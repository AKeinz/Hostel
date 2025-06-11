using Model;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для SelectedProblem.xaml
    /// </summary>
    public partial class SelectedProblem : Window
    {
        public SelectedProblem(object problem)
        {
            InitializeComponent();
            SelectedProblemViewModel selectedProblemViewModel = new SelectedProblemViewModel((Problem)problem);
            selectedProblemViewModel.NotifyDisplay += Close;
            DataContext = selectedProblemViewModel;
            selectedProblemViewModel.NotifyError += ShowMessage;
        }
        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
