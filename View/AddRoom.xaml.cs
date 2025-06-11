using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        AddRoomViewModel viewModel = new AddRoomViewModel();
        public AddRoom()
        {
            InitializeComponent();
            viewModel = new AddRoomViewModel();
            viewModel.NotifyDisplay += Close;
            DataContext = viewModel;

            viewModel.NotifyError += ShowMessage;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Устанавливаем фильтр для изображений
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*";

            // Если пользователь выбрал файл и нажал OK
            if (openFileDialog.ShowDialog() == true)
            {
                // Способ 1: Без использования потока (рекомендуется)

                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // Загружает файл в память и сразу закрывает поток
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();
                selectedImage.Source = image;
                // Способ 2: Если нужно использовать поток (без using)
                /*
                var stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                selectedImage.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                stream.Close(); // Закрыть поток вручную после загрузки
                */
            }
            viewModel.Room.Photo = openFileDialog.FileName;
        }
    }
}
