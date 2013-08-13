using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    public class ExtraDecisions : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public int DecisionMaker { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }

        IViewModel IFormEntity.ConvertToModel()
        {
            throw new System.NotImplementedException();
        }

        public void Update(IFormEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public long ChildId { get; set; }
        [Ignore]
        public virtual Child Child { get; set; }

    }
}
