using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Responsibility : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BeginningVisitation { get; set; }
        public int EndVisitation { get; set; }
        public int TransportationCosts { get; set; }
        public double FatherPercentage { get; set; }
        public double MotherPercentage { get; set; }
        public string OtherDetails { get; set; }

        public IViewModel ConvertToModel()
        {
            return new ResponsibilityViewModel()
            {
                BeginningVisitation = BeginningVisitation,
                EndVisitation = EndVisitation,
                OtherDetails = OtherDetails,
                FatherPercentage = FatherPercentage,
                MotherPercentage = MotherPercentage,
                TransportationCosts = TransportationCosts,
                UserId = UserId
            };
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
