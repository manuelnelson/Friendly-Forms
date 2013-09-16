using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    public class Information : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int InformationAccess { get; set; }
        public string AccessOfRightsDetails { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
