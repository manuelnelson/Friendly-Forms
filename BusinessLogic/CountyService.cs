using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class CountyService : ICountyService
    {
        private readonly ICountyRepository _countyRepository;

        public CountyService(ICountyRepository countyRepository)
        {
            _countyRepository = countyRepository;
        }
        public IEnumerable<County> GetAll()
        {
            try
            {
                return _countyRepository.GetAll();
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve county information", ex);
            }
        }
    }
}
