using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class Responsibility : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
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
