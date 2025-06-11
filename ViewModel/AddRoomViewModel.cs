using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class AddRoomViewModel : BaseViewModel
    {
        AddRoomLogic addRoomLogic = new AddRoomLogic();

        private Room room = new Room();
        public Room Room
        {
            get { return room; }
            set { room = value; OnPropertyChanged(nameof(Room)); }
        }

        public static List<RoomStatuses> Statuses { get; } = new List<RoomStatuses> {
            RoomStatuses.Busy,
            RoomStatuses.Broken,
            RoomStatuses.Free};


        public ICommand AddCommand { get; set; }
        public ICommand OpenFileSystemCommand { get; set; }

        public AddRoomViewModel()
        {
            AddCommand = new RelayCommand<object>(add);
            addRoomLogic.NotifyError += ShowMessage;
            OpenFileSystemCommand = new RelayCommand<object>(openFileSystem);
        }

        private void add(object param)
        {
            try
            {
                addRoomLogic.AddRoom(Room);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (HostelException ex)
            {
                NotifyError?.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Ошибка добавления");
            }
        }

        private void openFileSystem(object param)
        {
            NotifyDisplayFileSystem?.Invoke();
        }

        public event ErrorsHandler? NotifyError;
        public event DisplayWindowHandler? NotifyDisplay;
        public event DisplayWindowHandler? NotifyDisplayFileSystem;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
    }
}
