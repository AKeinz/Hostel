using Microsoft.Win32;
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
    public partial class ChangeRoom : Window
    {
        SelectedRoomViewModel selectedRoomViewModel;
        public ChangeRoom(object room)
        {
            InitializeComponent();
            selectedRoomViewModel = new SelectedRoomViewModel((Room)room);
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
            selectedRoomViewModel.NotifyError += ShowMessage;
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

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {

                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();
                selectedImage.Source = image;

            }
            if (!String.IsNullOrEmpty(openFileDialog.FileName))
            {
                selectedRoomViewModel.Room.Photo = openFileDialog.FileName;
            }

        }
    }
}
