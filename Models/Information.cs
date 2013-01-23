using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Information : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int InformationAccess { get; set; }
        public IViewModel ConvertToModel()
        {
            return new InformationViewModel
            {
                UserId = UserId,
                InformationAccess = InformationAccess
            };
        }

        public void Update(IFormEntity entity)
        {
            var update = (Information) entity;
            UserId = update.UserId;
            InformationAccess = update.InformationAccess;
        }
    }
}
