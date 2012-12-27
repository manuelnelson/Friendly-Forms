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
        public int? Deviation { get; set; }
        [DataMember]
        public int? Health { get; set; }
        [DataMember]
        public int? Insurance { get; set; }
        [DataMember]
        public int? TaxCredit { get; set; }
        [DataMember]
        public int? TravelExpenses { get; set; }
        [DataMember]
        public int? Visitation { get; set; }
        [DataMember]
        public int? Alimony { get; set; }
        [DataMember]
        public int? Mortgage { get; set; }
        [DataMember]
        public int? Permanency { get; set; }
        [DataMember]
        public int? NonSpecific { get; set; }
        [DataMember]
        public int? ParentingTime { get; set; }
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