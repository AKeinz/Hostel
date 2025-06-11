using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("problems")]
    public class Problem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;
        public int User { get; set; } = 0;
        public int Room { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public ProblemStatuses Status { get; set; }
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
