﻿using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    public class Information : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int InformationAccess { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (Information) entity;
            UserId = update.UserId;
            InformationAccess = update.InformationAccess;
        }
    }
}
