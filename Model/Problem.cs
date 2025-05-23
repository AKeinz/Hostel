using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("problems")]
    public class Problem
    {
        /*private int id { get; set; }
        private int user { get; set; }
        private int room { get; set; }
        private string description { get; set; }
        private ProblemStatuses status = ProblemStatuses.OnQuery;*/
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;
        public int User { get; set; } = 0;
        public int Room { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public ProblemStatuses Status { get; set; } = ProblemStatuses.OnQuery;
        [ForeignKey("User")]
        public User UserForeignKey { get; set; }
        [ForeignKey("Room")]
        public Room RoomForeignKey { get; set; }
    }

    public enum ProblemStatuses
    {
        Active,
        OnQuery,
        Done
    }
}
