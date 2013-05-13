using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class PreexistingSupportForm : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual int Support { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<PreexistingSupportFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (PreexistingSupport)entity;
            IsOtherParent = update.IsOtherParent;
            UserId = update.UserId;
            Support = update.Support;
        }
    }
}