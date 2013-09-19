using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
        //will name it to asset/ eventually, but doing this to appease my previous stupidity
        [DataContract]
        [Route("/assets/")]
        public class ReqAsset : IHasUser
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long UserId { get; set; }
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
            public int AdditionalAssets { get; set; }
            [DataMember]
            public string AdditionalAssetsDescription { get; set; }
        }

        [DataContract]
        public class RespAsset : IHasResponseStatus
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public ResponseStatus ResponseStatus { get; set; }
        }
        [CanViewClientInfo]
        public class AssetRestService : ServiceBase
        {
            public IAssetService AssetService { get; set; }
            public object Get(ReqAsset request)
            {
                if (request.Id != 0)
                {
                    return AssetService.Get(request.Id);
                }
                return AssetService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
            }
            public object Post(ReqAsset request)
            {
                var assets = request.TranslateTo<Assets>();
                AssetService.Add(assets);
                return new RespAsset()
                    {
                        Id = assets.Id
                    };
            }
            public object Put(ReqAsset request)
            {
                var assets = request.TranslateTo<Assets>();
                AssetService.Update(assets);
                return new RespAsset();
            }
        }
    
}