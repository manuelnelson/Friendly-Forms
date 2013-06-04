﻿using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("PreexistingSupportForms")]
    public class PreexistingSupportForm : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual int Support { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<PreexistingSupportFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (PreexistingSupport)entity;
            IsOtherParent = update.IsOtherParent;
            UserId = update.UserId;
            Support = update.Support;
        }
    }
}