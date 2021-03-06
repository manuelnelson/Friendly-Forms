﻿using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class Addendum : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public int HasAddendum { get; set; }
        public string AddendumDetails { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<AddendumViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
