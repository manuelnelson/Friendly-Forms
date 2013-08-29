using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("OtherChilds")]
    public class OtherChild : IEntity, IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        [NotMapped]
        [Ignore]
        public virtual string DateOfBirthString
        {
            get { return DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "Not Provided"; }
        }
        public virtual int OtherChildrenId { get; set; }
        [Ignore]
        public virtual OtherChildren OtherChildren { get; set; }
        
        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (OtherChild) entity;
            DateOfBirth = update.DateOfBirth;
            Name = update.Name;
            UserId = update.UserId;
            OtherChildrenId = update.OtherChildrenId;
        }

    }
}
