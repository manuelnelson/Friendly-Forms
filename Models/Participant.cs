using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Participants")]
    public class Participant : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public string PlaintiffsName { get; set; }
        public int PlaintiffRelationship { get; set; }
        public int PlaintiffCustodialParent { get; set; }
        public string DefendantsName { get; set; }
        public int DefendantRelationship { get; set; }
        public int DefendantCustodialParent { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
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

    }
}
