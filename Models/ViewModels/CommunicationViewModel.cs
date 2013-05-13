using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class CommunicationViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
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

        [Display(Name = "Limitation Details")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string LimitationDetails { get; set; }

        [Required]
        public int Notification { get; set; }
        
        [Required]
        [Display(Name = "Access Of Rights")]
        public int AccessOfRights { get; set; }
        
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Details")]
        public string AccessOfRightsDetails { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Communication>();
        }
    }
}
