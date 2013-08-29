﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ChildSupports")]
    public class ChildSupport : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int SupportBegin { get; set; }
        public DateTime? BeginDate { get; set; }
        [NotMapped]
        [Ignore]
        public virtual string BeginDateString
        {
            get { return BeginDate.HasValue ? BeginDate.Value.ToString("MM/dd/yyyy") : "Not Provided"; }
        }
        public int SupportEnd { get; set; }
        public DateTime? EndDate { get; set; }
        [NotMapped]
        [Ignore]
        public virtual string EndDateString
        {
            get { return EndDate.HasValue ? EndDate.Value.ToString("MM/dd/yyyy") : "Not Provided"; }
        }
        public int BothParties { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
        }
    }
}
