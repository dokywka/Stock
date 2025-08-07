using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Заголовок должен быть от 5 символов.")]
        [MaxLength(280,ErrorMessage ="Заголовок не может быть больше 280 символов.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Контент должен быть от 5 символов.")]
        [MaxLength(280, ErrorMessage = "Контент не может быть больше 280 символов.")]
        public string Content { get; set; } = string.Empty;
    }
}
