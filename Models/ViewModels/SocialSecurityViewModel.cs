using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class SocialSecurityViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }

        public bool IsOtherParent { get; set; }
        [Display(Name = "Receive")]
        public int ReceiveSocial { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? Amount { get; set; }
        
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<SocialSecurity>();
        }
    }
}
