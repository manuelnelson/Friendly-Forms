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
        public string FatherCosts { get; set; }
        public string MotherCosts { get; set; }

        public IViewModel ConvertToModel()
        {
            return new ResponsibilityViewModel()
            {
                BeginningVisitation = BeginningVisitation,
                EndVisitation = EndVisitation,
                FatherCosts = FatherCosts,
                FatherPercentage = FatherPercentage,
                MotherCosts = MotherCosts,
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
            FatherCosts = update.FatherCosts;
            FatherPercentage = update.FatherPercentage;
            MotherCosts = update.MotherCosts;
            MotherPercentage = update.MotherPercentage;
            TransportationCosts = update.TransportationCosts;
            UserId = update.UserId;
        }

    }
}
