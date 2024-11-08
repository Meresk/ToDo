using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class User_lw5_02
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        public List<Note_lw5_02> Notes { get; set; } = new List<Note_lw5_02>();
    }
}
