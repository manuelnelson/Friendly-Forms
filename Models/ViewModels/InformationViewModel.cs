using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class InformationViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Information Access")]
        public int InformationAccess { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Information
                {
                    UserId = UserId,
                    InformationAccess = InformationAccess
                };            
        }
    }

}
