using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class CommunicationViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Allow Communication")]
        public int AllowCommunication { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool Other { get; set; }
        [StringLength(100, ErrorMessage = "Field must be less than 100 characters")]
        [Display(Name = "Other Methods")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string OtherMethod { get; set; }
        [Required]
        public int Limitations { get; set; }
        [StringLength(100, ErrorMessage = "Field must be less than 100 characters")]
        [Display(Name = "Father Communication")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string FatherCommunicate { get; set; }
        [StringLength(100, ErrorMessage = "Field must be less than 100 characters")]
        [Display(Name = "Mother Communication")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string MotherCommunicate { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Communication()
                {
                    AllowCommunication = AllowCommunication,
                    Email = Email,
                    FatherCommunicate = FatherCommunicate,
                    Limitations = Limitations,
                    MotherCommunicate = MotherCommunicate,
                    Other = Other,
                    OtherMethod = OtherMethod,
                    Telephone = Telephone,
                    UserId = UserId
                };
        }
    }
}
