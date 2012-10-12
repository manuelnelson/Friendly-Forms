using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class SocialSecurityViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }

        public bool IsOtherParent { get; set; }
        [Display(Name = "Receive")]
        public int ReceiveSocial { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Amount { get; set; }
        public IFormEntity ConvertToEntity()
        {
            return new SocialSecurity()
                {
                    IsOtherParent = IsOtherParent,
                    Amount = Convert.ToInt32(Amount),
                    ReceiveSocial = ReceiveSocial,
                    UserId = UserId
                };
        }
    }
}
