using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;

namespace BusinessLogic.Models
{
    public class AdministrationModel
    {
        public List<User> Clients { get; set; }
        public AdminEmail Email { get; set; }
    }

    public class AdminEmail
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Law Firm")]
        public string LawFirm{ get; set; }

        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
