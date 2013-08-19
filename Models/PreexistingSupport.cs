using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("PreexistingSupports")]
    public class PreexistingSupport : IEntity, IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual int Support { get; set; }
        public virtual string CourtName { get; set; }
        public virtual int CaseNumber { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int Monthly { get; set; }

        public IViewModel ConvertToModel()
        {
            return new PreexistingSupportViewModel
                {
                    IsOtherParent = IsOtherParent,
                    Support = Support,
                    CaseNumber = CaseNumber.ToString(),
                    CourtName = CourtName,
                    Monthly = Monthly.ToString(),
                    OrderDate = OrderDate.ToString("MM/dd/yyyy"),
                    Id = Id,
                    UserId = UserId
                };
        }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (PreexistingSupport) entity;
            IsOtherParent = update.IsOtherParent;
            UserId = update.UserId;
            CourtName = update.CourtName;
            CaseNumber = update.CaseNumber;
            OrderDate = update.OrderDate;
            Monthly = update.Monthly;
            Support = update.Support;
        }
    }
}
