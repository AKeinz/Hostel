using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class StatusService
    {
        public static void UpdateOrdersStatus()
        {
            using (var _dbContext = new HostelDBContext())
            {
                List<int> notFreeRooms = new List<int>();
                var orders = _dbContext.Orders.ToList();
                foreach (Order order in orders)
                {
                    if (order.Out_day < DateTime.Now.Date)
                    {
                        order.Status = OrderStatuses.Completed;
                        UpdateRoomStatus(order.Room, RoomStatuses.Free, _dbContext);
                    }
                    if (order.In_day <= DateTime.Now.Date && order.Out_day >= DateTime.Now.Date)
                    {
                        order.Status = OrderStatuses.InProgress;
                        UpdateRoomStatus(order.Room, RoomStatuses.Busy, _dbContext);
                        notFreeRooms.Add(order.Room);
                    }
                    if (order.In_day > DateTime.Now.Date && order.Out_day > DateTime.Now.Date)
                    {
                        order.Status = OrderStatuses.Reservation;
                    }
                    
                    _dbContext.SaveChanges();
                }
                UpdateFreeRoomStatus(notFreeRooms, _dbContext);
            }
            UpdateRoomsByProblemsStatus();

        }

        public static void UpdateRoomsByProblemsStatus()
        {
            using (var _dbContext = new HostelDBContext())
            {
                var orders = _dbContext.Problems.ToList();
                foreach (Problem problem in orders)
                {
                    if (!problem.Status.Equals(ProblemStatuses.Done))
                    {
                        UpdateRoomStatus(problem.Room, RoomStatuses.Broken, _dbContext);
                        break;
                    }
                }
            }

        }


        public static void UpdateRoomStatus(int roomId, RoomStatuses newStatus, HostelDBContext _dbContext)
        {
            var room = _dbContext.Rooms.Find(roomId);

            room.Status = newStatus;
            RoomsRepository roomsRepository = new RoomsRepository();
            roomsRepository.Update(room);
            _dbContext.SaveChanges();
        }

        public static void UpdateFreeRoomStatus(List<int> notFreeRooms, HostelDBContext _dbContext)
        {
            List<Room> allRooms = new RoomsRepository().GetAll().ToList();
            RoomsRepository roomsRepository = new RoomsRepository();
            foreach (Room room in allRooms)
            {
                if (!notFreeRooms.Contains(room.Room_number))
                {
                    room.Status = RoomStatuses.Free;
                    roomsRepository.Update(room);
                }
            }
            _dbContext.SaveChanges();
        }

    }
}
