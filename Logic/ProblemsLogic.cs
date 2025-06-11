using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class ProblemsLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Problem> problemsRepository;
        public readonly IRepository<Room> roomsRepository;
        public IRepository<User> usersRepository = new UsersRepository();

        private List<Problem> problems;
        public List<Problem> Problems
        {
            get { return problems; }
            set { problems = value; OnPropertyChanged(nameof(Problems)); }
        }
        public List<int> Room_numbers;
        private int user_id;
        private string description;
        private int room;

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; OnPropertyChanged("User_id"); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        public int Room
        {
            get { return room; }
            set { room = value; OnPropertyChanged(nameof(Room)); }
        }

        public ProblemsLogic(IRepository<Problem> problemsRepository, IRepository<Room> roomsRepository)
        {
            this.problemsRepository = problemsRepository;
            this.roomsRepository = roomsRepository;
            Room_numbers = GetRoomNumbers();
            Problems = GetProblems();
        }

        public ProblemsLogic()
        {
            this.problemsRepository = new ProblemsRepository();
            this.roomsRepository = new RoomsRepository();
            GetRoomNumbers();
            GetProblems();
        }

        public List<Problem> GetProblems()
        {
            Problems = problemsRepository.GetAll().ToList();
            return Problems;
        }
        public (List<Problem> probs, List<string> us) GetProblemsWithUsers()
        {
            List<string> users = new List<string>();
            Problems = problemsRepository.GetAll().ToList();
            foreach (Problem problem in Problems)
            {
                User u = usersRepository.GetById(problem.User);
                users.Add($"({u.Id}) {u.Lastname} {u.Firstname} {u.Patronymic}");
            }
            return (Problems, users);
        }

        public List<int> GetRoomNumbers()
        {
            List<int> room_numbers = new List<int>();
            List<Room> rooms = roomsRepository.GetAll().ToList();
            foreach (Room room in rooms)
            {
                room_numbers.Add(room.Room_number);
            }
            Room_numbers = room_numbers;
            return room_numbers;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
