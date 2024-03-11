using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRenting.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please Enter FirstName")]
        public string FirstName { get; set; }
        [Required (ErrorMessage ="Please Enter LastName")]
        public string LastName { get; set; }
        [Required (ErrorMessage ="Please Enter Phone Number")]
        public string Phone { get; set; }
        [Required (ErrorMessage ="Please Enter Email")]
        public string Email { get; set; }
        [Required (ErrorMessage ="Please Enter User Name")]
        public string UserName { get; set; }
        [Required (ErrorMessage ="Pleas Enter Password")]
        public string Password { get; set; }
        [Required (ErrorMessage ="Pleas Enter NationalId")]
        public long NationalId { get; set; }
        public string ProfileImageName { get; set; }

        [NotMapped]
        public IFormFile ProfileImageUrl { get; set; }

        [Required(ErrorMessage = "Pleas Enter Your BirthDate")]
        public DateTime BirthDate { get; set; }
    }
}
