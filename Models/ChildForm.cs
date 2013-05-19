using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class ChildForm : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ChildrenInvolved { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ChildFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (ChildForm)entity;
            UserId = updatingEntity.UserId;
            ChildrenInvolved = updatingEntity.ChildrenInvolved;
        }


    }
}
