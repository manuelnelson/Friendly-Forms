﻿using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
        //will name it to asset/ eventually, but doing this to appease my previous stupidity
        [DataContract]
        [Route("/asset/")]
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