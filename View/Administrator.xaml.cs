using DatabaseLayer;
using Logic;
using System.Windows;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator(int id)
        {
            InitializeComponent();
            CurrentUser currentUser = new CurrentUser(id);
            //UserGrid.DataContext = currentUser;
            //MessageBox.Show(usersGrid.Items.Count.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
