using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class AddUserViewModel : BaseViewModel
    {
        AddUserLogic addUserLogic = new AddUserLogic();

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



        public ICommand AddCommand { get; set; }

        public AddUserViewModel()
        {
            AddCommand = new RelayCommand<object>(add);
            addUserLogic.NotifyError += ShowMessage;
        }

        private void add(object param)
        {
            try
            {
                addUserLogic.checkData(user);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (HostelException ex)
            {
                NotifyError?.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("не удалось сохранить изменения");
            }
        }

        public event ErrorsHandler? NotifyError;
        public event DisplayWindowHandler? NotifyDisplay;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
    }
}
