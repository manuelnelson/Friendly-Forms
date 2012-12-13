using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class ParticipantViewModel : IViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Plaintiff's Name")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string PlaintiffsName { get; set; }

        [Required]
        [Display(Name = "Plaintiff Relationship")]
        public int PlaintiffRelationship { get; set; }

        [Required]
        [Display(Name = "Plaintiff Custodial Parent")]
        public int PlaintiffCustodialParent { get; set; }

        [Required]
        [Display(Name = "Defendant's Name")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string DefendantsName { get; set; }

        [Required]
        [Display(Name = "Defendant's Relationship")]
        public int DefendantRelationship { get; set; }

        [Required]
        [Display(Name = "Defendant Custodial Parent")]
        public int DefendantCustodialParent { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Participant()
            {
                DefendantCustodialParent = this.DefendantCustodialParent,
                DefendantRelationship = this.DefendantRelationship,
                DefendantsName = this.DefendantsName,
                PlaintiffCustodialParent = this.PlaintiffCustodialParent,
                PlaintiffRelationship = this.PlaintiffRelationship,
                PlaintiffsName = this.PlaintiffsName,
                UserId = this.UserId
            };
        } 
    }



}