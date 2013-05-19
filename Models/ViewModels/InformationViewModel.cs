using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class InformationViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Information Access")]
        public int InformationAccess { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Information>();
        }
    }

}
