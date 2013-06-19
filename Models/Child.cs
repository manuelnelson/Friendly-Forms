using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.DataAnnotations;


namespace Models
{
    public class Child : IEntity, IFormEntity
    {
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime ? DateOfBirth { get; set; }
        [Ignore]
        [NotMapped]
        public virtual string DateOfBirthString { get { return DateOfBirth.HasValue ? DateOfBirth.Value.ToShortDateString() : "No Date Provided"; } }
        public int ChildFormId { get; set; }
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
