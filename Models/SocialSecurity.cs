using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class SocialSecurity : IFormEntity
    {
        public int Id { get; set; }
        public bool IsOtherParent { get; set; }
        public int UserId { get; set; }
        public int ReceiveSocial { get; set; }
        public int Amount { get; set; } 
        public IViewModel ConvertToModel()
        {
            return new SocialSecurityViewModel()
                {
                    IsOtherParent = IsOtherParent,
                    Amount = Amount.ToString(),
                    ReceiveSocial = ReceiveSocial,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var updateEntity = (SocialSecurity) entity;
            IsOtherParent = updateEntity.IsOtherParent;
            Amount = updateEntity.Amount;
            ReceiveSocial = updateEntity.ReceiveSocial;
            UserId = updateEntity.UserId;
        }
    }
}
