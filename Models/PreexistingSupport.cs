using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("PreexistingSupports")]
    public class PreexistingSupport : IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual int Support { get; set; }
        public virtual string CourtName { get; set; }
        public virtual string CaseNumber { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int Monthly { get; set; }

        [Ignore]
        [NotMapped]
        public virtual List<PreexistingSupportChild> Children { get; set; }


        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (PreexistingSupport) entity;
            IsOtherParent = update.IsOtherParent;
            UserId = update.UserId;
            CourtName = update.CourtName;
            CaseNumber = update.CaseNumber;
            OrderDate = update.OrderDate;
            Monthly = update.Monthly;
            Support = update.Support;
        }
    }
}
