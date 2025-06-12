using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class AddRoomLogic : BaseLogic, INotifyPropertyChanged
    {
        public readonly IRoomsRepository roomsRepository;

        private Room new_room;

        public Room New_room
        {
            get { return new_room; }
            set { new_room = value; OnPropertyChanged(nameof(New_room)); }
        }
       

        public AddRoomLogic(IRoomsRepository repository)
        {
            roomsRepository = repository;
        }
        public AddRoomLogic()
        {
            roomsRepository = new RoomsRepository();
        }


        private bool copyPhoto()
        {
            try
            {
                string sourcePath = New_room.Photo;
                if (string.IsNullOrEmpty(sourcePath))
                    return false;

                if (!File.Exists(sourcePath))
                    return false;
                string destinationDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RoomPhotos", New_room.Room_number.ToString());
                Directory.CreateDirectory(destinationDir);

                string destinationPath = Path.Combine(destinationDir, $"{New_room.Room_number}{Path.GetExtension(sourcePath)}");

                using (var sourceStream = File.Open(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var destinationStream = File.Create(destinationPath))
                    {
                        sourceStream.CopyTo(destinationStream);
                    }
                }

                New_room.Photo = destinationPath; 
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("не удалось скопировать фото");
                return false;
            }
        }

        public void AddRoom(Room param)
        {

            New_room = param;
            if (New_room.Capacity < 1 || New_room.Cost_per_day <=0)
            {
                throw new HostelException("данные не корректны");
            }
            try
            {
                New_room.Room_number = roomsRepository.GetMaxId() + 1;
            }
            catch
            {
                New_room.Room_number = 0;
            }
            if (copyPhoto())
            {
                roomsRepository.Add(New_room);
            }
            else
            {
                throw new HostelException("не удалось скопировать фото");
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event BaseLogic.ErrorsHandler? NotifyError;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
