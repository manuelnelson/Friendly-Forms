using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Child : IFormEntity
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime ? DateOfBirth { get; set; }

        public IViewModel ConvertToModel()
        {
            return new ChildrenViewModel()
            {
                DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "Not Provided",
                Name = Name,
                UserId = UserId,
                Id = Id
            };

        }

        public void Update(IFormEntity entity)
        {
            var update = (Child) entity;
            DateOfBirth = update.DateOfBirth;
            Name = update.Name;
            UserId = update.UserId;

        }
    }    
}
