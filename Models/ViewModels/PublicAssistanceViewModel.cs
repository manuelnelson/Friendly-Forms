using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class PublicAssistanceViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Assistance { get; set; }
        [Required]
        public int OtherAssistance { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<PublicAssistance>();
        }
    }
}
