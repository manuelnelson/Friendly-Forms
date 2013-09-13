using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class AttorneyClientService : Service<IAttorneyClientRepository, AttorneyClient>, IAttorneyClientService
    {
        public AttorneyClientService(IAttorneyClientRepository repository) : base(repository)
        {
        }

        public List<AttorneyClient> GetByUserId(long userId)
        {
            try
            {
                return Repository.GetByUserId(userId).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to save child information", ex);
            }
        }
    }
}
