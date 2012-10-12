using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class HealthViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Health { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.,'_ \-\s]*$", ErrorMessage = @"Only alpha-numeric characters and [.,_-'] are allowed.")]
        [Display(Name = "Details")]
        public string HealthDescription { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new HealthInsurance()
                {
                    Health = Health,
                    HealthDescription = HealthDescription,
                    UserId = UserId
                };
        }
    }
}
