using DatabaseLayer;
using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class ServiceViewModel : BaseViewModel
    {
        StatusService orderStatusService = new StatusService();   
        private RoomsLogic roomsLogic = new RoomsLogic();
        private AddProblemLogic addProblemLogic = new AddProblemLogic();
        ProblemsLogic problemsLogic = new ProblemsLogic();
        ChangeProblemLogic changeProblemLogic = new ChangeProblemLogic();
        public User CurrentUser { get; set; }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        private List<int> room_numbers = new List<int>();
        public List<int> Room_numbers
        {
            get { return room_numbers; }
            set { room_numbers = value; OnPropertyChanged("Room_numbers"); }
        }

        private int room_number;
        public int Room_number
        {
            get { return room_number; }
            set { room_number = value; OnPropertyChanged("Room_number"); }
        }

        private List<Problem> problems;
        public List<Problem> Problems
        {
            get { return problems; }
            set { problems = value; OnPropertyChanged(nameof(Problems)); }
        }

        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

        private bool isEnabledStatusesCombobox = false;
        public bool IsEnabledStatusesCombobox
        {
            get { return isEnabledStatusesCombobox; }
            set { isEnabledStatusesCombobox = value; OnPropertyChanged(nameof(IsEnabledStatusesCombobox)); }
        }

        private string changeButtonContent = "Изменить";
        public string ChangeButtonContent
        {
            get { return changeButtonContent; }
            set { changeButtonContent = value; OnPropertyChanged("ChangeButtonContent"); }
        }

        public static List<ProblemStatuses> Statuses { get; } = new List<ProblemStatuses> { ProblemStatuses.Done, ProblemStatuses.OnQuery, ProblemStatuses.Active };
        public ServiceViewModel()
        {
            StatusService.UpdateOrdersStatus();
            AddProblemCommand = new RelayCommand<object>(addProblem);
            Room_numbers = problemsLogic.GetRoomNumbers();
            addProblemLogic.NotifyError += ShowMessage;
            Problems = problemsLogic.GetProblems();
            Rooms = roomsLogic.GetRooms();
            ChangeStatusCommand = new RelayCommand<object>(changeStatus);
            DisplaySelectRoomCommand = new RelayCommand<Room>(displaySelectedRoom);
        }

        public ICommand AddProblemCommand { get; set; }
        public ICommand ChangeStatusCommand { get; set; }
        public ICommand DisplaySelectRoomCommand { get; set; }

        private void addProblem(object param)
        {
            addProblemLogic.Client_id = CurrentUser.Id;
            try
            {
                addProblemLogic.AddProblem(CurrentUser.Id, Description, Room_number);
            }
            catch (Exception ex)
            {
                ShowMessage("Ошибка добавления неисправности. Не оставляйте описание пустым");
            }
            Update();
        }

        public void Update()
        {
            StatusService.UpdateOrdersStatus();
            Room_numbers = problemsLogic.GetRoomNumbers();
            Problems = problemsLogic.GetProblems();
            Rooms = roomsLogic.GetRooms();
        }

        private void changeStatus(object param)
        {
            if (IsEnabledStatusesCombobox)
            {
                int id = (int)param;
                Problem problem = Problems.Where(p => p.Id == id).First();
                changeProblemLogic.changeProblem(problem);
            }
            changeIsEnabled();
            Update();
        }

        private void changeIsEnabled()
        {
            IsEnabledStatusesCombobox = !IsEnabledStatusesCombobox;
            if (IsEnabledStatusesCombobox)
            {
                ChangeButtonContent = "Сохранить изменения";
            }
            else { ChangeButtonContent = "Изменить"; }
        }
        private void displaySelectedRoom(Room param)
        {
            NotifyDisplaySelectRoom?.Invoke(param);
        }



        public event ErrorsHandler? NotifyError;
        public delegate void DisplayWithParamHandler<T>(T param);
        public event DisplayWithParamHandler<Room>? NotifyDisplaySelectRoom;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
    }
}
