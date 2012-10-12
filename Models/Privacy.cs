using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Privacy : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NeedPrivacy { get; set; }
        public string Details { get; set; }
        public IViewModel ConvertToModel()
        {
            return new PrivacyViewModel()
            {
                Details = Details,
                NeedPrivacy = NeedPrivacy,
                UserId = UserId
            };
        }

        public void Update(IFormEntity entity)
        {
            var update = (Privacy)entity;
            Details = update.Details;
            NeedPrivacy = update.NeedPrivacy;
            UserId = update.UserId;
        }

    }
}
