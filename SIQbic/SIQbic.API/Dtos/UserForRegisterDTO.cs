using System.ComponentModel.DataAnnotations;

namespace SIQbic.API.Dtos
{
    public class UserForRegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength= 4, ErrorMessage="Invalid length, password must between 4 and 8")]
        public string Password { get; set; }
        
        public string DisplayName { get; set; }        
        
    }
}