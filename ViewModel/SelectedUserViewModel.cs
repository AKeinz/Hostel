using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class SelectedUserViewModel : BaseViewModel
    {
        ChangeUserLogic changeUserLogic = new ChangeUserLogic();


        private User user = new User();
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(nameof(User)); }
        }

        public static List<Roles> Roles { get; } = new List<Roles> {
            Model.Roles.Client,
        Model.Roles.Reception,
        Model.Roles.Service,
        Model.Roles.Admin};

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public SelectedUserViewModel(User User)
        {
            this.User = User;
            UpdateCommand = new RelayCommand<User>(update);
            DeleteCommand = new RelayCommand<User>(delete);
        }

        private void update(object param)
        {
            try
            {
                changeUserLogic.changeUser(User);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Возникла ошибка в процессе обновления данных");
            }
        }

        private void delete(object param)
        {
            try
            {
                changeUserLogic.deleteUser(User);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Возникла ошибка в процессе удаления");
            }
        }

        public event ErrorsHandler? NotifyError;
        public event DisplayWindowHandler? NotifyDisplay;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
    }
}
