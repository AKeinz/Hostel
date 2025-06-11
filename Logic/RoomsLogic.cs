using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class RoomsLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Room> roomsRepository;

        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

        public RoomsLogic(IRepository<Room> rep)
        {
            roomsRepository = rep;
            Rooms = GetRooms();
        }

        public RoomsLogic()
        {
            roomsRepository = new RoomsRepository();
            Rooms = GetRooms();
        }

        public List<int> GetRoomIds()
        {
            List<int> ids = new List<int>();
            foreach (Room room in Rooms)
            {
                ids.Add(room.Room_number);
            }
            return ids;
        }

        public List<Room> GetRooms()
        {
            Rooms = roomsRepository.GetAll().ToList();
            return Rooms;
        }


        public delegate void RoomsHandler();
        public event RoomsHandler? Notify;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
