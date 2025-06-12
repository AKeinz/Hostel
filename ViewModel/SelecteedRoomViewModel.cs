using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class SelectedRoomViewModel : BaseViewModel
    {
        SelectedRoomLogic selectedRoomLogic = new SelectedRoomLogic();


        private Room room = new Room();
        public Room Room
        {
            get { return room; }
            set { room = value; OnPropertyChanged(nameof(Room)); }
        }

        public static List<RoomStatuses> Statuses { get; } = new List<RoomStatuses>
        { RoomStatuses.Broken,
        RoomStatuses.Busy,
        RoomStatuses.Free};


        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand OpenFileSystemCommand { get; set; }


        public SelectedRoomViewModel(Room room)
        {
            Room = room;
            UpdateCommand = new RelayCommand<Room>(update);
            DeleteCommand = new RelayCommand<Room>(delete);
            OpenFileSystemCommand = new RelayCommand<object>(openFileSystem);
            selectedRoomLogic.NotifyError += ShowMessage;
        }

        private void update(object param)
        {
            try
            {
                selectedRoomLogic.ChangeRoom(Room);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch
            {
                NotifyError?.Invoke("не удалось сохранить изменения");
            }
        }

        private void delete(object param)
        {
            try
            {
                selectedRoomLogic.DeleteRoom(Room);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke(ex.Message);
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
