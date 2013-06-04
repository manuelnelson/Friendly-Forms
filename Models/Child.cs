using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Children")]
    public class Child : IEntity, IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime ? DateOfBirth { get; set; }

        public int ChildFormId { get; set; }
        [Ignore]
        public virtual ChildForm ChildForm { get; set; }


        public IViewModel ConvertToModel()
        {
            return new ChildViewModel
                {
                DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "Not Provided",
                Name = Name,
                UserId = UserId,
                Id = Id,
                ChildFormId = ChildFormId
            };

        }

        public void Update(IFormEntity entity)
        {
            var update = (Child) entity;
            DateOfBirth = update.DateOfBirth;
            Name = update.Name;
            UserId = update.UserId;
            ChildFormId = update.ChildFormId;
        }
    }    
}
