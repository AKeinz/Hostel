using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class ChangeUserLogic : INotifyPropertyChanged
    {
        public readonly IUsersRepository usersRepository;

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(nameof(User)); }
        }

        public ChangeUserLogic(IUsersRepository UsersRepository)
        {
            this.usersRepository = UsersRepository;
        }
        public ChangeUserLogic()
        {
            this.usersRepository = new UsersRepository();
        }


        public void changeUser(User user)
        {
            User = user;
            usersRepository.Update(User);
        }



        public void deleteUser(User user)
        {
            User = user;
            usersRepository.Delete(User);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
