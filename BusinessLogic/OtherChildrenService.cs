﻿using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class OtherChildrenService : Service<OtherChildrenRepository, OtherChildren, OtherChildrenViewModel>, IOtherChildrenService
    {
        public OtherChildrenService(OtherChildrenRepository formRepository) : base(formRepository)
        {
        }

        public OtherChildrenViewModel GetByUserId(int userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId.Equals(userId) && m.IsOtherParent.Equals(isOtherParent)).FirstOrDefault();
                return (entity == null ? new OtherChildrenViewModel() : entity.ConvertToModel()) as OtherChildrenViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }

        }
    }
}
