using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class ChildForm : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IViewModel ConvertToModel()
        {
            return new ChildFormViewModel()
            {
                Id = Id,
                ChildrenInvolved = ChildrenInvolved,
                UserId = UserId
            };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (ChildForm)entity;
            UserId = updatingEntity.UserId;
            ChildrenInvolved = updatingEntity.ChildrenInvolved;
        }

        public int ChildrenInvolved { get; set; }

    }
}
