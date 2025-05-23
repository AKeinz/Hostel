using DatabaseLayer;
using MessageController;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private string code = "";
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(nameof(Code)); }
        }
        private string sentCode;
        private IMessageContrroller messageController;
        public ICommand UpdateUserCommand { get; set; }
        public ICommand CheckCodeCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ChangeUserLogic(IUsersRepository UsersRepository)
        {
            this.usersRepository = UsersRepository;
            UpdateUserCommand = new RelayCommand(checkUser);
            CheckCodeCommand = new RelayCommand(checkCode);
            DeleteUserCommand = new RelayCommand(deleteUser);
            messageController = new MessageController.MessageController();
        }

        private void checkUser(object param)
        {
            sendCode();
        }
        private void changeUser()
        {
            usersRepository.Update(user);
        }

        private void sendCode()
        {
            sentCode = generateCode();
            messageController.SendMessage(user.Phone, Code);
            displayShowCode();
        }

        public string generateCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 9999);
            sentCode = code.ToString();
            return code.ToString();
        }
        private void checkCode(object param)
        {
            if (Code.Equals(sentCode))
            {
                changeUser();
            }
            else
            {
                throw new HostelException("Неверный код");
            }
        }

        private void deleteUser(object param)
        {
            sendCode();
        }

        private void displayShowCode()
        {
            usersRepository.Delete(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
