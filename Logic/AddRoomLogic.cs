using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class AddRoomLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Room> roomsRepository;

        private Room new_room;
        private int room_number;
        private int capacity;
        private double cost_per_day;
        private string description;
        private RoomStatuses status;
        private string photo;

        public Room New_room
        {
            get { return new_room; }
            set { new_room = value; OnPropertyChanged(nameof(New_room)); }
        }
        public int Room_number
        {
            get { return room_number; }
            set { room_number = value; OnPropertyChanged(nameof(Room_number)); }
        }
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; OnPropertyChanged(nameof(Capacity)); }
        }
        public double Cost_per_day
        {
            get { return cost_per_day; }
            set { cost_per_day = value; OnPropertyChanged(nameof(Cost_per_day)); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }
        public RoomStatuses Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }
        public string Photo
        {
            get { return photo; }
            set { photo = value; OnPropertyChanged(nameof(Photo)); }
        }
        public ICommand AddRoomCommand { get; set; }
        public ICommand OpenFileSystemCommand { get; set; }

        public AddRoomLogic(IRepository<Room> repository)
        {
            roomsRepository = repository;
            AddRoomCommand = new RelayCommand(addRoom);
            OpenFileSystemCommand = new RelayCommand(openFileSystem);
        }


        private void openFileSystem(object param)
        {
            //в щкне
        }

        private bool copyPhoto()
        {
            try
            {
                FileInfo photoPath = new FileInfo(Photo);
                if (!photoPath.Exists)
                {
                    return false;
                }
                string destinationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RoomPhotos", Room_number.ToString());
                DirectoryInfo newRoomPhotos = new DirectoryInfo(destinationDirectory);
                if (!newRoomPhotos.Exists)
                {
                    newRoomPhotos.Create();
                }
                string destinationPath = Path.Combine(newRoomPhotos.FullName, Room_number.ToString() + photoPath.Extension);
                photoPath.CopyTo(destinationPath, true);
                Photo = destinationPath;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void addRoom(object param)
        {
            if (copyPhoto())
            {
                roomsRepository.Add(new Room()
                {
                    Room_number = Room_number,
                    Capacity = Capacity,
                    Cost_per_day = Cost_per_day,
                    Description = Description,
                    Status = Status,
                    Photo = photo,
                });
            }
        }

        private void displayRooms()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
