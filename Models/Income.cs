using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Incomes")]
    public class Income : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public bool IsOtherParent { get; set; }
        public int HaveSalary { get; set; }
        public string OtherIncome { get; set; }
        public int? W2Income { get; set; }
        public int? NonW2Income { get; set; }
        public int? SelfIncome { get; set; }
        public int? SelfIncomeNoDeductions { get; set; }
        public int? Commisions { get; set; }
        public int? Bonuses { get; set; }
        public int? Overtime { get; set; }
        public int? Severance { get; set; }
        public int? Retirement { get; set; }
        public int? Interest { get; set; }
        public int? Dividends { get; set; }
        public int? Trust { get; set; }
        public int? Annuities { get; set; }
        public int? Capital { get; set; }
        public int? SocialSecurity { get; set; }
        public int? Compensation { get; set; }
        public int? Unemployment { get; set; }
        public int? CivilCase { get; set; }
        public int? Gifts { get; set; }
        public int? Prizes { get; set; }
        public int? Alimony { get; set; }
        public int? Assets { get; set; }
        public int? Fringe { get; set; }
        public int? Other { get; set; }
        public string OtherDetails { get; set; }
        
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<IncomeViewModel>();
        }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
            //var updatedEntity = (Income) entity;
            //this
            //IsOtherParent = updatedEntity.IsOtherParent;
            //UserId = updatedEntity.UserId;
            //Employed = updatedEntity.Employed;
            //Salary = updatedEntity.Salary;
            //SelfEmployed = updatedEntity.SelfEmployed;
            //SelfIncome = updatedEntity.SelfIncome;
            //SelfTax = updatedEntity.SelfTax;
            //SelfTaxAmount = updatedEntity.SelfTaxAmount;
            //OtherSources = updatedEntity.OtherSources;
            //Commisions = updatedEntity.Commisions;
            //Bonuses = updatedEntity.Bonuses;
            //Overtime = updatedEntity.Overtime;
            //Severance = updatedEntity.Severance;
            //Retirement = updatedEntity.Retirement;
            //Interest = updatedEntity.Interest;
            //Dividends = updatedEntity.Dividends;
            //Trust = updatedEntity.Trust;
            //Annuities = updatedEntity.Annuities;
            //Capital = updatedEntity.Capital;
            //SocialSecurity = updatedEntity.SocialSecurity;
            //Compensation = updatedEntity.Compensation;
            //Unemployment = updatedEntity.Unemployment;
            //CivilCase = updatedEntity.CivilCase;
            //Gifts = updatedEntity.Gifts;
            //Prizes = updatedEntity.Prizes;
            //Alimony = updatedEntity.Alimony;
            //Assets = updatedEntity.Assets;
            //Fringe = updatedEntity.Fringe;
            //Other = updatedEntity.Other;
            //OtherDetails = updatedEntity.OtherDetails;            
        }
    }
}
