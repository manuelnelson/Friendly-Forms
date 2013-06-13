using System;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;

namespace BusinessLogic
{
    public class BcsoService: IBcsoService
    {
        private readonly IBcsoRepository _bcsoRepository;

        public BcsoService(IBcsoRepository bscoRepository)
        {
            _bcsoRepository = bscoRepository;
        }

        public double GetAmount(double income, int numberOfChildren)
        {
            try
            {
                return _bcsoRepository.GetAmount(income, numberOfChildren);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve Basic child support obligations", ex);
            }
        }
    }
}
