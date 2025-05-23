using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class SelectedRoomLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Room> roomsRepository;

        private Room room;
        public Room Room
        {
            get { return room; }
            set { room = value; OnPropertyChanged(nameof(Room)); }
        }
        public ICommand UpdateRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }
        public ICommand OpenFileSystemCommand { get; set; }
        public SelectedRoomLogic(IRepository<Room> repository)
        {
            roomsRepository = repository;
            UpdateRoomCommand = new RelayCommand(changeRoom);
            DeleteRoomCommand = new RelayCommand(deleteRoom);
            OpenFileSystemCommand = new RelayCommand(openFileSystem);
        }

        private void changeRoom(object param)
        {
            if (copyPhoto())
            {
                roomsRepository.Update(Room);
            };
        }

        private bool copyPhoto()
        {
            try
            {
                FileInfo photoPath = new FileInfo(Room.Photo);
                if (!photoPath.Exists)
                {
                    return false;
                }
                string destinationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RoomPhotos", Room.Room_number.ToString());
                DirectoryInfo newRoomPhotos = new DirectoryInfo(destinationDirectory);
                if (!newRoomPhotos.Exists)
                {
                    newRoomPhotos.Create();
                }
                string destinationPath = Path.Combine(newRoomPhotos.FullName, Room.Room_number.ToString() + photoPath.Extension);
                photoPath.CopyTo(destinationPath, true);
                Room.Photo = destinationPath;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void deleteRoom(object param)
        {
            roomsRepository.Delete(Room);
            string photoPath = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Room.Room_number.ToString()));
            if (Directory.Exists(photoPath))
            {
                Directory.Delete(photoPath);
            }
        }

        private void openFileSystem(object param)
        {
            //в щкне
        }

        private string selectPhoto()
        {
            return "";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
