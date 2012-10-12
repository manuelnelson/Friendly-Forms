﻿using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class HealthInsurance : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Health { get; set; }
        public string HealthDescription { get; set; }

        public IViewModel ConvertToModel()
        {
            return new HealthViewModel()
                {
                    HealthDescription = HealthDescription,
                    Health = Health,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (HealthInsurance)entity;
            UserId = updatingEntity.UserId;
            Health = updatingEntity.Health;
            HealthDescription = updatingEntity.HealthDescription;
        }
    }
}
