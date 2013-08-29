using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class ChildService : FormService<IChildRepository, Child>, IChildService
    {
        private IChildRepository ChildRepository { get; set; }
        public ChildService(IChildRepository formRepository) : base(formRepository)
        {
            ChildRepository = formRepository;
        }

        public new List<Child> GetByUserId(long userId)
        {
            try
            {
                return ChildRepository.GetByUserId(userId);
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save child information", ex);                
            }
        }
    }
}
