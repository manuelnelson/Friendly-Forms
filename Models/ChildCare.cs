﻿using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ChildCares")]
    public class ChildCare : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public int SchoolFather { get; set; }
        public int SchoolMother { get; set; }
        public int SchoolNonParent { get; set; }
        public int SummerFather { get; set; }
        public int SummerMother { get; set; }
        public int SummerNonParent { get; set; }
        public int BreaksFather { get; set; }
        public int BreaksMother { get; set; }
        public int BreaksNonParent { get; set; }
        public int OtherFather { get; set; }
        public int OtherMother { get; set; }
        public int OtherNonParent { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        [Ignore]
        public Child Child { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
