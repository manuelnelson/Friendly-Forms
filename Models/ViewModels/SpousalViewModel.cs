using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class SpousalViewModel : IViewModel        
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Spousal { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.,'_ \-\s]*$", ErrorMessage = @"Only alpha-numeric characters and [.,_-'] are allowed.")]
        [Display(Name = "Details")]
        public string SpousalDescription { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new SpousalSupport()
                {
                    UserId = UserId,
                    Spousal = Spousal,
                    SpousalDescription = SpousalDescription
                };
        }
    }
}
