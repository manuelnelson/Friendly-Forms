using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Contract;
using Models.ViewModels;
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
        
        public IViewModel ConvertToModel()
        {
            return new OtherChildViewModel
                {
                    DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "Not provided",
                    Name = Name,
                    UserId = UserId,
                    Id = Id,
                    OtherChildrenId = OtherChildrenId
                };
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
