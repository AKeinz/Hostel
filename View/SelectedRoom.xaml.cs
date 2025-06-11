using Model;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для SelectedRoom.xaml
    /// </summary>
    public partial class SelectedRoom : Window
    {
        public SelectedRoom(object room)
        {

            InitializeComponent();
            SelectedRoomViewModel selectedRoomViewModel = new SelectedRoomViewModel((Room)room);
            selectedRoomViewModel.NotifyDisplay += Close;
            DataContext = selectedRoomViewModel;
            try
            {
                selectedImage.Source = LoadImageWithoutLocking(selectedRoomViewModel.Room.Photo);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private BitmapImage LoadImageWithoutLocking(string filePath)
        {
            var bitmap = new BitmapImage();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
            }
            return bitmap;
        }
    }
}
