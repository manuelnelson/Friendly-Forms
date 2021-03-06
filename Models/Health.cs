﻿using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class Health : IEntity, IFormEntity
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

        public virtual User User { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<HealthViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
