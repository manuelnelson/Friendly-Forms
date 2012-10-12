using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class PrivacyViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Need Privacy")]
        public int NeedPrivacy { get; set; }

        public string Details { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Privacy()
                {
                    Details = Details,
                    NeedPrivacy = NeedPrivacy,
                    UserId = UserId
                };
        }
    }
}
