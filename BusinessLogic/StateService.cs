using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class StateService : Service<IStateRepository, State>, IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
            : base(stateRepository)
        {
            _stateRepository = stateRepository;
        }
        public IEnumerable<State> GetAll()
        {
            try
            {
                return _stateRepository.GetAll();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve state information", ex);
            }
        }
    }
}
