using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("rooms")]
    public class Room
    {
        /*private int room_number { get; set; }
        private int capacity { get; set; }
        private double cost_per_day { get; set; }
        private string description { get; set; }
        private RoomStatuses status { get; set; }
        private string photo { get; set; }*/
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
