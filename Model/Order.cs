using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("orders")]
    public class Order
    {
        /* private int order_id { get; set; }
         private int room { get; set; }
         private int client { get; set; }
         private DateTime in_day { get; set; }
         private DateTime out_day { get; set; }
         private int number_of_days { get; set; }
         private int number_of_people { get; set; }
         private int total_cost { get; set; }
         private OrderStatuses status { get; set; }*/
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_id { get; set; } = 0;
        public int Room { get; set; } = 0;
        public int Client { get; set; } = 0;
        public DateTime In_day { get; set; } = DateTime.Now;
        public DateTime Out_day { get; set; } = DateTime.Now;
        public int Number_of_days { get; set; } = 0;
        public int Number_of_people { get; set; } = 0;
        public int Total_cost { get; set; } = 0;
        public OrderStatuses Status { get; set; } = OrderStatuses.InProgress;
        [ForeignKey("Client")]
        public User UserForeignKey { get; set; }
        [ForeignKey("Room")]
        public Room RoomForeignKey { get; set; }
    }

    public enum OrderStatuses
    {
        Completed,
        InProgress,
        Reservation
    }
}
