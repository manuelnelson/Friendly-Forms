﻿using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class PublicAssistance : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int Assistance { get; set; }
        public int OtherAssistance { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<PublicAssistanceViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (PublicAssistance) entity;
            Assistance = updatingEntity.Assistance;
            UserId = updatingEntity.UserId;
            OtherAssistance = updatingEntity.OtherAssistance;
        }
    }
}
