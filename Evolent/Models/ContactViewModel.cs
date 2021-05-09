using System.ComponentModel.DataAnnotations;

namespace Evolent.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Required")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, ErrorMessage = "Length should not be greater than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required.")]
        [StringLength(50, ErrorMessage = "Length should not be greater than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required e-mail.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid.")]
        [StringLength(100, ErrorMessage = "Length should not be greater than 100 characters.")]
        public string Email { get; set; }

        public bool Active { get; set; }
    }
}