using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class SelectedProblemViewModel : BaseViewModel
    {
        ChangeProblemLogic changeProblemLogic = new ChangeProblemLogic();
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

        public ICommand UpdateProblemCommand { get; set; }
        public ICommand DeleteProblemCommand { get; set; }

        public SelectedProblemViewModel()
        {
            UpdateProblemCommand = new RelayCommand<object>(updateProblem);
            DeleteProblemCommand = new RelayCommand<object>(deleteProblem);
            Users = usersLogic.GetUserIds();
            Rooms = roomsLogic.GetRoomIds();
        }
        public SelectedProblemViewModel(Problem problem)
        {
            Problem = problem;
            UpdateProblemCommand = new RelayCommand<object>(updateProblem);
            DeleteProblemCommand = new RelayCommand<object>(deleteProblem);
            Users = usersLogic.GetUserIds();
            Rooms = roomsLogic.GetRoomIds();
        }

        private void updateProblem(object param)
        {
            try
            {
                changeProblemLogic.changeProblem(Problem);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (HostelException ex)
            {
                NotifyError?.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Возникла ошибка в процессе обновления данных");
            }
        }

        private void deleteProblem(object param)
        {
            try
            {
                changeProblemLogic.deleteProblem(Problem);
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
