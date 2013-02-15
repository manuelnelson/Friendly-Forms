using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class Information : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int InformationAccess { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<InformationViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (Information) entity;
            UserId = update.UserId;
            InformationAccess = update.InformationAccess;
        }
    }
}
