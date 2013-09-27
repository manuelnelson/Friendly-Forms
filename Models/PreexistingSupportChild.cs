using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("PreexistingSupportChilds")]
    public class PreexistingSupportChild : IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual int Gender { get; set; }

        public virtual int PreexistingSupportId { get; set; }
        [Ignore]
        public virtual PreexistingSupport PreexistingSupport { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (PreexistingSupportChild) entity;
            PreexistingSupportId = update.PreexistingSupportId;
            Gender = update.Gender;
            Name = update.Name;
            UserId = update.UserId;
            PreexistingSupportId = update.PreexistingSupportId;
        }
    }
}
