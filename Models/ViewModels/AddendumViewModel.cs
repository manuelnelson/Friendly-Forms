using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class AddendumViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int HasAddendum { get; set; }
        
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Details")]
        public string AddendumDetails { get; set; }
        
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Addendum>();
        }
    }
}
