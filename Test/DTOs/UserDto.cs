using System.ComponentModel.DataAnnotations;

namespace Test.DTOs
{
    public class UserDto
    {
        [Required, DataType("varchar50")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
