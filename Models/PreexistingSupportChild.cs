using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class PreexistingSupportChild : IEntity, IFormEntity
    {
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual int Gender { get; set; }

        public virtual int PreexistingSupportId { get; set; }
        public virtual PreexistingSupport PreexistingSupport { get; set; }

        public IViewModel ConvertToModel()
        {
            return new PreexistingSupportChildViewModel()
                {
                    Id = Id,
                    DateOfBirth = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "Not Provided",
                    Gender = Gender,
                    Name = Name,
                    UserId = UserId,
                    PreexistingSupportId = PreexistingSupportId
                };
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
