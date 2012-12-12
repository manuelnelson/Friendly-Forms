using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Tax/")]
    public class ReqTax
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int Taxes { get; set; }
        [DataMember]
        public string TaxDescription { get; set; }
    }

    [DataContract]
    public class RespTax : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class TaxRestService : Service
    {
        public ITaxService TaxService { get; set; }

        public object Post(ReqTax request)
        {
            var taxViewModel = request.TranslateTo<TaxViewModel>();
            TaxService.AddOrUpdate(taxViewModel);
            return new RespTax();
        }
    }
}