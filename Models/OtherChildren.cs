using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class OtherChildren : IEntity, IFormEntity
    {
        public virtual long Id { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual long UserId { get; set; }
        public virtual int LegallyResponsible { get; set; }
        public virtual int AtHome { get; set; }
        public virtual int Support { get; set; }
        public virtual int Preexisting { get; set; }
        public virtual int InCourt { get; set; }
        public virtual string Details { get; set; }

        public IViewModel ConvertToModel()
        {
            return new OtherChildrenViewModel()
                {
                    IsOtherParent = IsOtherParent,
                    AtHome = AtHome,
                    Details = Details,
                    Id = Id,
                    InCourt = InCourt,
                    LegallyResponsible = LegallyResponsible,
                    Preexisting = Preexisting,
                    Support = Support,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var update = (OtherChildren) entity;
            IsOtherParent = update.IsOtherParent;
            AtHome = update.AtHome;
            Details = update.Details;                    
            InCourt = update.InCourt;
            LegallyResponsible = update.LegallyResponsible;
            Preexisting = update.Preexisting;
            Support = update.Support;
            UserId = update.UserId;
        }
    }
}
