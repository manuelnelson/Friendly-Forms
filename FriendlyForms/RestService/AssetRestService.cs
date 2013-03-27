﻿using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
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
            public long Id { get; set; }
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
            public long Id { get; set; }
            [DataMember]
            public ResponseStatus ResponseStatus { get; set; }
        }

        public class AssetRestService : Service
        {
            public IAssetService AssetService { get; set; }
            public object Get(ReqAsset request)
            {
                if (request.Id != 0)
                {
                    return AssetService.Get(request.Id);
                }
                if (request.UserId != 0)
                {
                    return AssetService.GetByUserId(request.UserId);
                }
                return new Assets();
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