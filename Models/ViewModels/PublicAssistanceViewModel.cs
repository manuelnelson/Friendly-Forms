using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class PublicAssistanceViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Assistance { get; set; }
        [Required]
        public int OtherAssistance { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new PublicAssistance()
                {
                    Assistance = Assistance,
                    UserId = UserId,
                    OtherAssistance = OtherAssistance
                };
        }
    }
}
