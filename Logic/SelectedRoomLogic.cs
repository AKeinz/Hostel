using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class SelectedRoomLogic : BaseLogic, INotifyPropertyChanged
    {
        public readonly IRepository<Room> roomsRepository;

        private Room room;
        public Room Room
        {
            get { return room; }
            set { room = value; OnPropertyChanged(nameof(Room)); }
        }

        public SelectedRoomLogic(IRepository<Room> repository)
        {
            roomsRepository = repository;
        }

        public SelectedRoomLogic()
        {
            roomsRepository = new RoomsRepository();
        }

        public void ChangeRoom(object param)
        {
            if (copyPhoto())
            {
                roomsRepository.Update(Room);
            }
            else
            {
                NotifyError?.Invoke("Фото не найдено");
                return;
            }
        }

        public void ChangeRoom(Room param)
        {
            Room = param;
            if (copyPhoto())
            {
                roomsRepository.Update(Room);
            }
            else
            {
                throw new HostelException("Фото не найдено");
            }
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
                if (photoPath.FullName.Equals(destinationPath))
                {
                    return true;
                }
                photoPath.CopyTo(destinationPath, true);
                Room.Photo = destinationPath;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DeleteRoom(object param)
        {
            roomsRepository.Delete(Room);
            string photoPath = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RoomPhotos", Room.Room_number.ToString()));
            if (Directory.Exists(photoPath))
            {
                Directory.Delete(photoPath, true);
            }
        }

        public void DeleteRoom(Room param)
        {
            Room = param;

            roomsRepository.Delete(Room);
            string photoPath = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RoomPhotos", Room.Room_number.ToString()));

            if (Directory.Exists(photoPath))
            {
                try
                {

                    foreach (string file in Directory.GetFiles(photoPath))
                    {
                        DeleteFileWithRetry(file);
                    }


                    Directory.Delete(photoPath, recursive: true);
                    NotifyError?.Invoke("Папка успешно удалена");
                }
                catch (Exception ex)
                {
                    NotifyError?.Invoke($"Ошибка при удалении: {ex.Message}");
                }
            }


        }

        private void DeleteFileWithRetry(string filePath, int maxRetries = 3, int delayMs = 100)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    File.Delete(filePath);
                    return;
                }
                catch (IOException) when (i < maxRetries - 1)
                {
                    Thread.Sleep(delayMs);
                }
            }
            throw new IOException($"Не удалось удалить файл {filePath} после {maxRetries} попыток.");
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
