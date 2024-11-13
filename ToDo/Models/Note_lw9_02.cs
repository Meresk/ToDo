using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Note_lw9_02
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        // Навигационное свойство:
        public User_lw9_02 User { get; set; } // Это свойство для отношения с моделью пользователя
    }
}
