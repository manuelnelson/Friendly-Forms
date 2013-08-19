using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Responsibilities")]
    public class Responsibility : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int BeginningVisitation { get; set; }
        public int EndVisitation { get; set; }
        public int TransportationCosts { get; set; }
        public double FatherPercentage { get; set; }
        public double MotherPercentage { get; set; }
        public string OtherDetails { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ResponsibilityViewModel>();
        }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (Responsibility)entity;
            BeginningVisitation = update.BeginningVisitation;
            EndVisitation = update.EndVisitation;
            OtherDetails = update.OtherDetails;
            FatherPercentage = update.FatherPercentage;
            MotherPercentage = update.MotherPercentage;
            TransportationCosts = update.TransportationCosts;
            UserId = update.UserId;
        }

    }
}
