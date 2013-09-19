using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Children")]
    public class Child : IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public int ChildFormId { get; set; }
        [Ignore]
        public virtual ChildForm ChildForm { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (Child)entity;
            DateOfBirth = update.DateOfBirth;
            Name = update.Name;
            UserId = update.UserId;
            ChildFormId = update.ChildFormId;
        }
    }
}
