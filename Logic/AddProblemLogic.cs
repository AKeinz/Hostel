using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class AddProblemLogic : BaseLogic, INotifyPropertyChanged
    {
        public readonly IRepository<Problem> problemsRepository;
        public readonly IUsersRepository usersRepository;
        public readonly IOrdersRepository ordersRepository;
        public readonly IRepository<Room> roomsRepository;

        private int client_id;
        private string description;
        private List<int> room_numbers = new List<int>();
        private int room_number;

        public List<int> Room_numbers
        {
            get { return room_numbers; }
            set { room_numbers = value; OnPropertyChanged("Room_numbers"); }
        }
        public int Room_number
        {
            get { return room_number; }
            set { room_number = value; OnPropertyChanged("Room_number"); }
        }
        public int Client_id
        {
            get { return client_id; }
            set { client_id = value; OnPropertyChanged("User_id"); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        public AddProblemLogic(IRepository<Problem> problemsRepository, IOrdersRepository ordersRepository, IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            this.problemsRepository = problemsRepository;
            this.ordersRepository = ordersRepository;
        }

        public AddProblemLogic(int id)
        {
            Client_id = id;
            this.problemsRepository = new ProblemsRepository();
            this.ordersRepository = new OrdersRepository();
            this.roomsRepository = new RoomsRepository();
            this.usersRepository = new UsersRepository();

            getRoomNumbers();
        }

        public AddProblemLogic()
        {
            this.problemsRepository = new ProblemsRepository();
            this.ordersRepository = new OrdersRepository();
            this.roomsRepository = new RoomsRepository();
            this.usersRepository = new UsersRepository();

            getRoomNumbers();
        }

        private int getCurrentRoom()
        {
            int room = ordersRepository.GetCurrentRoom(client_id);
            if (room == -1)
            {
                NotifyError?.Invoke("Вы не проживаете в гостинице");
            }
            return room;
        }

        public void AddProblem()
        {
            int room = Room_number;
            Roles role = usersRepository.GetById(Client_id).Role;
            if (role.Equals(Roles.Client)) { room = getCurrentRoom(); }
            if (room != -1)
            {
                problemsRepository.Add(new Problem()
                {
                    Status = ProblemStatuses.OnQuery,
                    Room = room,
                    Description = Description,
                    User = client_id,
                });
                NotifyError?.Invoke("Успешно!");
                Description = "";
            }
        }

        public void AddProblem(int id, string desc, int room)
        {
            Client_id = id;
            Description = desc;
            Roles role = usersRepository.GetById(Client_id).Role;
            if (role.Equals(Roles.Client)) { room = getCurrentRoom(); }
            if (room != -1)
            {
                problemsRepository.Add(new Problem()
                {
                    Status = ProblemStatuses.OnQuery,
                    Room = room,
                    Description = Description,
                    User = client_id,
                });
                NotifyError?.Invoke("Успешно!");
                Description = "";
                changeRoomStatus(room);
            }
        }
        public void AddProblem(Problem problem)
        {
            problemsRepository.Add(problem);
            if (problem.Status != ProblemStatuses.Done)
            {
                changeRoomStatus(problem.Room);
            }
        }

        private void changeRoomStatus(int room)
        {
            Room roo = roomsRepository.GetById(room);
            roo.Status = RoomStatuses.Broken;
            roomsRepository.Update(roo);
        }


        public List<int> getRoomNumbers()
        {
            Room_numbers.Clear();
            List<Room> rooms = roomsRepository.GetAll().ToList();
            foreach (Room room in rooms)
            {
                Room_numbers.Add(room.Room_number);
            }
            return Room_numbers;
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
