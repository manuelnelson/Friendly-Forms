using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using ServiceStack.Common.Web;

namespace BusinessLogic
{
    public class ClientService : Service<IClientRepository, Client>, IClientService
    {
        private IClientRepository ClientRepository { get; set; }

        public ClientService(IClientRepository repository) : base(repository)
        {
            ClientRepository = repository;
        }

        public List<User> GetUsersClients(int userId)
        {
            try
            {
                return ClientRepository.GetUsersClients(userId).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to retrieve items");
            }
        }

        public bool LawyerHasClient(int lawyerId, int clientId)
        {
            try
            {
                return ClientRepository.LawyerHasClient(lawyerId, clientId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new HttpError(HttpStatusCode.InternalServerError, "Unable to authorize");                
            }
        }
    }
}
