using System.ComponentModel.DataAnnotations;

namespace SIQbic.API.Dtos
{
    public class QuestionForRegisterDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Response { get; set; }
    }
}