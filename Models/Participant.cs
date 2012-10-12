using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Participant : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IViewModel ConvertToModel()
        {
            return new ParticipantViewModel()
            {
                DefendantRelationship = DefendantRelationship,
                DefendantCustodialParent = DefendantCustodialParent,
                DefendantsName = DefendantsName,
                PlaintiffCustodialParent = PlaintiffCustodialParent,
                PlaintiffRelationship = PlaintiffRelationship,
                PlaintiffsName = PlaintiffsName,
                UserId = UserId
            };
        }

        public void Update(IFormEntity entity)
        {
            var update = (Participant)entity;
            DefendantRelationship = update.DefendantRelationship;
            DefendantCustodialParent = update.DefendantCustodialParent;
            DefendantsName = update.DefendantsName;
            PlaintiffCustodialParent = update.PlaintiffCustodialParent;
            PlaintiffRelationship = update.PlaintiffRelationship;
            PlaintiffsName = update.PlaintiffsName;
            UserId = update.UserId;
        }

        public string PlaintiffsName { get; set; }
        public int PlaintiffRelationship { get; set; }
        public int PlaintiffCustodialParent { get; set; }
        public string DefendantsName { get; set; }
        public int DefendantRelationship { get; set; }
        public int DefendantCustodialParent { get; set; }
    }
}
