﻿using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class Deviations : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int Circumstances { get; set; }
        public string Unjust { get; set; }
        public string BestInterest { get; set; }
        public string Impair { get; set; }
        public int? HighLow { get; set; }
        public int? LowDeviation { get; set; }
        public string WhyLow { get; set; }
        public int? HighIncome { get; set; }
        public int? Health { get; set; }
        public int? Insurance { get; set; }
        public int? TaxCredit { get; set; }
        public int? TravelExpenses { get; set; }
        public int? Visitation { get; set; }
        public int? Alimony { get; set; }
        public int? Mortgage { get; set; }
        public int? Permanency { get; set; }
        public int? NonSpecific { get; set; }


        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<DeviationsViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}