using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("rooms")]
    public class Room
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Room_number { get; set; } = 0;
        public int Capacity { get; set; } = 0;
        public double Cost_per_day { get; set; } = 0;
        public string Description { get; set; } = "";
        public RoomStatuses Status { get; set; } = RoomStatuses.Broken;
        public string Photo { get; set; } = "";
    }

    public enum RoomStatuses
    {
        Busy,
        Free,
        Broken
    }
}
