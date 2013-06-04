﻿using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ChildSupports")]
    public class ChildSupport : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public string PaidBy { get; set; }
        public string PaidTo { get; set; }
        public int MonthlyAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int TemporaryAgreement { get; set; }
        public int Payment { get; set; }
        public int PaymentDay { get; set; }
        public IViewModel ConvertToModel()
        {
            return new ChildSupportViewModel
                {
                    EffectiveDate = EffectiveDate.ToString("MM/dd/yyyy"),
                    MonthlyAmount = MonthlyAmount.ToString(),
                    PaidBy = PaidBy,
                    PaidTo = PaidTo,
                    Payment = Payment,
                    PaymentDay = PaymentDay,
                    TemporaryAgreement = TemporaryAgreement,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (ChildSupport)entity;
            UserId = updatingEntity.UserId;
            PaidBy = updatingEntity.PaidBy;
            PaidTo = updatingEntity.PaidTo;
            MonthlyAmount = updatingEntity.MonthlyAmount;
            EffectiveDate = updatingEntity.EffectiveDate;
            TemporaryAgreement = updatingEntity.TemporaryAgreement;
            Payment = updatingEntity.Payment;
            PaymentDay = updatingEntity.PaymentDay;
        }
    }
}
