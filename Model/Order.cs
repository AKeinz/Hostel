using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;
        public int Room { get; set; } = 0;
        public int Client { get; set; }
        public DateTime In_day { get; set; } = DateTime.Now.Date;
        public DateTime Out_day { get; set; } = DateTime.Now.AddDays(1).Date;
        public int Number_of_days { get; set; } = 0;
        public int Number_of_people { get; set; } = 0;
        public double Total_cost { get; set; } = 0;
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
