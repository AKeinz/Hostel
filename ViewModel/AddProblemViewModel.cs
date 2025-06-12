using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class AddProblemViewModel : BaseViewModel
    {
        AddProblemLogic addProblemLogic = new AddProblemLogic();
        UsersLogic usersLogic = new UsersLogic();
        RoomsLogic roomsLogic = new RoomsLogic();

        private Problem problem = new Problem();
        public Problem Problem
        {
            get { return problem; }
            set { problem = value; OnPropertyChanged(nameof(Problem)); }
        }

        public static List<ProblemStatuses> Statuses { get; } = new List<ProblemStatuses> { ProblemStatuses.Done, ProblemStatuses.OnQuery, ProblemStatuses.Active };
        private List<int> users = new List<int>();
        public List<int> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged(nameof(Users)); }
        }

        private List<int> rooms = new List<int>();
        public List<int> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

        public ICommand AddProblemCommand { get; set; }

        public AddProblemViewModel()
        {
            AddProblemCommand = new RelayCommand<object>(addProblem);
            addProblemLogic.NotifyError += ShowMessage;
            Users = usersLogic.GetUserIds();
            Rooms = roomsLogic.GetRoomIds();
        }

        private void addProblem(object param)
        {
            try
            {
                addProblemLogic.AddProblem(problem);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
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
