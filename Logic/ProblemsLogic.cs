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
    public class ProblemsLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Problem> problemsRepository;
        public readonly IRepository<Room> roomsRepository;

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
        public ICommand AddProblemCommand { get; set; }
        public ICommand ChangeProblemCommand { get; set; }

        public ProblemsLogic(IRepository<Problem> problemsRepository, IRepository<Room> roomsRepository)
        {
            this.problemsRepository = problemsRepository;
            this.roomsRepository = roomsRepository;
            Room_numbers = getRoomNumbers();
            Problems = getProblems();
            AddProblemCommand = new RelayCommand(addProblem);
            ChangeProblemCommand = new RelayCommand(displayChangeProblem);
        }

        private List<Problem> getProblems()
        {
            return problemsRepository.GetAll().ToList();
        }

        private List<int> getRoomNumbers()
        {
            List<int> room_numbers = new List<int>();
            List<Room> rooms = roomsRepository.GetAll().ToList();
            foreach (Room room in rooms)
            {
                room_numbers.Add(room.Room_number);
            }
            return room_numbers;
        }

        private void addProblem(object param)
        {
            problemsRepository.Add(new Problem()
            {
                Room = Room,
                Description = Description,
                User = User_id,
                Status = ProblemStatuses.OnQuery,
            });
        }
        private void displayChangeProblem(object param)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
