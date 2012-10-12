using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class OtherChild : IFormEntity
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual int Gender { get; set; }

        public virtual int OtherChildrenId { get; set; }
        public virtual OtherChildren OtherChildren { get; set; }
        
        public IViewModel ConvertToModel()
        {
            return new OtherChildViewModel()
                {
                    DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "Not provided",
                    Gender = Gender,
                    Name = Name,
                    UserId = UserId,
                    Id = Id,
                    OtherChildrenId = OtherChildrenId
                };
        }

        public void Update(IFormEntity entity)
        {
            var update = (OtherChild) entity;
            DateOfBirth = DateOfBirth;
            Gender = Gender;
            Name = Name;
            UserId = UserId;
            OtherChildrenId = OtherChildrenId;
        }

    }
}
