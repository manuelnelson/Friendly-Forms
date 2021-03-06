﻿using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class HealthViewModel : IViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ProvideHealth { get; set; }
        public int Prorate { get; set; }
        public bool FathersHealth { get; set; }
        public bool MothersHealth { get; set; }
        public bool NonCustodialHealth { get; set; }
        public int? FathersHealthAmount { get; set; }
        public int? MothersHealthAmount { get; set; }
        public int? NonCustodialHealthAmount { get; set; }
        public int? FathersHealthPercentage { get; set; }
        public int? MothersHealthPercentage { get; set; }
        public int? NonCustodialHealthPercentage { get; set; }
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Health>();
        }
    }
}
