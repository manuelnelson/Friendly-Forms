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
    [Route("/SpecialCircumstances/")]
    public class ReqSpecialCircumstances
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int Circumstances { get; set; }
        [DataMember]
        public string Deviation { get; set; }
        [DataMember]
        public string Health { get; set; }
        [DataMember]
        public string Insurance { get; set; }
        [DataMember]
        public string TaxCredit { get; set; }
        [DataMember]
        public string TravelExpenses { get; set; }
        [DataMember]
        public string Visitation { get; set; }
        [DataMember]
        public string Alimony { get; set; }
        [DataMember]
        public string Mortgage { get; set; }
        [DataMember]
        public string Permanency { get; set; }
        [DataMember]
        public string NonSpecific { get; set; }
        [DataMember]
        public string ParentingTime { get; set; }
    }

    [DataContract]
    public class RespSpecialCircumstances : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class SpecialCircumstancesRestService : Service
    {
        public ISpecialCircumstancesService SpecialCircumstancesService { get; set; }

        public object Post(ReqSpecialCircumstances request)
        {
            var SpecialCircumstancesViewModel = request.TranslateTo<SpecialCircumstancesViewModel>();
            SpecialCircumstancesService.AddOrUpdate(SpecialCircumstancesViewModel);
            return new RespSpecialCircumstances();
        }
    }
}