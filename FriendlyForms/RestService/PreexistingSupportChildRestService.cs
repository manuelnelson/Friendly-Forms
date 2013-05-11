﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/PreexistingSupportChild/")]
    [Route("/PreexistingSupportChild/{Id}/")]
    public class ReqPreexistingSupportChild
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DateOfBirth { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public int PreexistingSupportId { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupportChild : IHasResponseStatus
    {
        [DataMember]
        public ReqPreexistingSupportChild Child { get; set; }
        [DataMember]
        public List<ReqPreexistingSupportChild> Children { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class PreexistingSupportChildRestService : ServiceBase
    {
        public IPreexistingSupportChildService PreexistingSupportChildService { get; set; }
        public object Get(ReqPreexistingSupportChild request)
        {
            var childrenEntities = PreexistingSupportChildService.GetChildrenBySupportId(request.Id);
            var children = new List<ReqPreexistingSupportChild>();
            foreach (var preexistingSupportChild in childrenEntities)
            {
                var child = preexistingSupportChild.TranslateTo<ReqPreexistingSupportChild>();
                if (preexistingSupportChild.DateOfBirth != null)
                    child.DateOfBirth = preexistingSupportChild.DateOfBirth.Value.ToShortDateString();
                children.Add(child);
            }
            return new RespPreexistingSupportChild
                {
                    Children = children
                };
        }
        public object Post(ReqPreexistingSupportChild request)
        {
            var preexistingSupportChildViewModel = request.TranslateTo<PreexistingSupportChildViewModel>();
            preexistingSupportChildViewModel.UserId = Convert.ToInt32(UserSession.CustomId);
            var preexistingSupportEntity = PreexistingSupportChildService.AddOrUpdate(preexistingSupportChildViewModel);
            var preexistingSupport = preexistingSupportEntity.TranslateTo<ReqPreexistingSupportChild>();
            if (preexistingSupportEntity.DateOfBirth != null)
                preexistingSupport.DateOfBirth = preexistingSupportEntity.DateOfBirth.Value.ToShortDateString();
            return new RespPreexistingSupportChild()
                {
                    Child = preexistingSupport
                };
        }
        public object Put(ReqPreexistingSupportChild request)
        {
            var preexistingSupportChild = request.TranslateTo<PreexistingSupportChild>();
            preexistingSupportChild.UserId = Convert.ToInt32(UserSession.CustomId);
            PreexistingSupportChildService.Update(preexistingSupportChild);
            return new RespPreexistingSupportChild();
        }
    }
}