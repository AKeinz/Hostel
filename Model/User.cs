using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("users")]
    public class User : INotifyPropertyChanged
    {
        /*private int id;
        private Roles role = Roles.Client;

        private string lastname;
        private string firstname;
        private string patronymic;
        private string phone;
        private string login;
        private string password;*/
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 1;
        public Roles Role { get; set; } = Roles.Client;
        public string Lastname { get; set; } = " ";
        public string Firstname { get; set; } = " ";
        public string Patronymic { get; set; } = " ";
        public string Phone { get; set; } =  " ";
        public string Login { get; set; } = " ";
        public string Password { get; set; } = " ";

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

    public enum Roles
    {
        Client,
        Admin,
        Reception,
        Service
    }
}
