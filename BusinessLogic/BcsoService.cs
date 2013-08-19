﻿using System;
using BusinessLogic.Contracts;
using BusinessLogic.Helpers;
using DataInterface;
using Elmah;

namespace BusinessLogic
{
    public class BcsoService : IBcsoService
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
                var nearest50 = income.RoundTo(50);
                //TODO: temporary fix
                if (nearest50 < 800)
                    nearest50 = 800;
                if (nearest50 > 30000)
                    nearest50 = 30000;
                return _bcsoRepository.GetAmount(nearest50, numberOfChildren);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve Basic child support obligations", ex);
            }
        }
    }
}
