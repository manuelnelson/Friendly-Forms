using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ChildForms")]
    public class ChildForm : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
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
