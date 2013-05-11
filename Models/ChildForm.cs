using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;


namespace Models
{
    public class ChildForm : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
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
