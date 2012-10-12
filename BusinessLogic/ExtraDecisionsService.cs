﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ExtraDecisionsService : IExtraDecisionsService
    {
        private readonly IExtraDecisionRepository _extraDecisionRepository;

        public ExtraDecisionsService(IExtraDecisionRepository extraDecisionRepository)
        {
            _extraDecisionRepository = extraDecisionRepository;
        }
        public ExtraDecisions AddOrUpdate(ExtraDecisionsViewModel model)
        {
            try
            {
                var entity = model.ConvertToEntity();
                //Check if court already exists and we need to update record
                var existCourt = _extraDecisionRepository.Get(model.Id);
                if (existCourt != null)
                {
                    existCourt.DecisionMaker = entity.DecisionMaker;
                    existCourt.Description = entity.Description;
                    existCourt.ChildId = entity.ChildId;
                    _extraDecisionRepository.Update(existCourt);
                    return existCourt;
                }
                _extraDecisionRepository.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to add decisions information", ex);
            }
        }

        public ExtraDecisionsViewModel GetByChildId(int childId)
        {
            try
            {
                var enumerable = _extraDecisionRepository.GetFiltered(e => e.ChildId.Equals(childId));
                var listExtras = enumerable == null ? new List<ExtraDecisions>() : enumerable.ToList();
                return new ExtraDecisionsViewModel
                    {
                        ExtraDecisions = listExtras
                    };
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve decision information", ex);
            }            
        }
    }
}
