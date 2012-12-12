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
        //will name it to asset/ eventually, but doing this to appease my previous stupidity
        [DataContract]
        [Route("/assets/")]
        public class ReqAsset
        {
            [DataMember]
            public int UserId { get; set; }
            [DataMember]
            public int Retirement { get; set; }
            [DataMember]
            public string RetirementDescription { get; set; }
            [DataMember]
            public int NonRetirement { get; set; }
            [DataMember]
            public string NonRetirementDescription { get; set; }
            [DataMember]
            public int Business { get; set; }
            [DataMember]
            public string BusinessDescription { get; set; }
            [DataMember]
            public string AdditionalAssets { get; set; }
        }

        [DataContract]
        public class RespAsset : IHasResponseStatus
        {
            [DataMember]
            public ResponseStatus ResponseStatus { get; set; }
        }

        public class AssetRestService : Service
        {
            public IAssetService AssetService { get; set; }

            public object Post(ReqAsset request)
            {
                var assetViewModel = request.TranslateTo<AssetViewModel>();
                AssetService.AddOrUpdate(assetViewModel);
                return new RespAsset();
            }
        }
    
}