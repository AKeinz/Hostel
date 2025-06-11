using DatabaseLayer;
using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        StatusService orderStatusService = new StatusService();
        private RoomsLogic roomsLogic = new RoomsLogic();
        private AddProblemLogic addProblemLogic = new AddProblemLogic();

        public User CurrentUser { get; set; }

        public ClientViewModel()
        {
            StatusService.UpdateOrdersStatus();
            Rooms = roomsLogic.GetRooms();
            AddProblemCommand = new RelayCommand<object>(addProblem);
            DisplaySelectRoomCommand = new RelayCommand<Room>(displaySelectedRoom);

            addProblemLogic.NotifyError += ShowMessage;
        }

        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

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
        public ICommand AddProblemCommand { get; set; }
        public ICommand DisplaySelectRoomCommand { get; set; }

        private void addProblem(object param)
        {
            addProblemLogic.Client_id = CurrentUser.Id;
            try
            {
                addProblemLogic.AddProblem(CurrentUser.Id, Description, -1);
            }
            catch (Exception ex)
            {
                ShowMessage("Ошибка добавления неисправности. Не оставляйте описание пустым");
            }
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
