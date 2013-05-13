using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]

    [Route("/Holiday/")]
    [Route("/Holiday/{ChildId}", Verbs = "GET")]
    public class ReqHoliday
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public long ChildId { get; set; }
        [DataMember]
        public bool FridayHoliday { get; set; }
        [DataMember]
        public bool MondayHoliday { get; set; }
        [DataMember]
        public int Thanksgiving { get; set; }
        [DataMember]
        public string ThanksgivingOther { get; set; }
        [DataMember]
        public string ThanksgivingTime { get; set; }
        [DataMember]
        public int Christmas { get; set; }
        [DataMember]
        public string ChristmasTime { get; set; }
        [DataMember]
        public string ChristmasOther { get; set; }
        [DataMember]
        public int SpringBreak { get; set; }
        [DataMember]
        public string SpringOther { get; set; }
        [DataMember]
        public int SummerBeginDays { get; set; }
        [DataMember]
        public string SummerBeginTime { get; set; }
        [DataMember]
        public int SummerEndDays { get; set; }
        [DataMember]
        public string SummerEndTime { get; set; }
        [DataMember]
        public string SummerDetails { get; set; }
        [DataMember]
        public int FallBreak { get; set; }
        [DataMember]
        public string FallOther { get; set; }
        [DataMember]
        public int ChristmasFather { get; set; }
        [DataMember]
        public int ChristmasMother { get; set; }
        [DataMember]
        public int SpringBreakFather { get; set; }
        [DataMember]
        public int SpringBreakMother { get; set; }
        [DataMember]
        public int FallBreakFather { get; set; }
        [DataMember]
        public int FallBreakMother { get; set; }
        [DataMember]
        public int ThanksgivingFather { get; set; }
        [DataMember]
        public int ThanksgivingMother { get; set; }
        [DataMember]
        public int MlkFather { get; set; }
        [DataMember]
        public int MlkMother { get; set; }
        [DataMember]
        public int PresidentsFather { get; set; }
        [DataMember]
        public int PresidentsMother { get; set; }
        [DataMember]
        public int MothersFather { get; set; }
        [DataMember]
        public int MothersMother { get; set; }
        [DataMember]
        public int MemorialFather { get; set; }
        [DataMember]
        public int MemorialMother { get; set; }
        [DataMember]
        public int FathersFather { get; set; }
        [DataMember]
        public int FathersMother { get; set; }
        [DataMember]
        public int IndependenceFather { get; set; }
        [DataMember]
        public int IndependenceMother { get; set; }
        [DataMember]
        public int LaborFather { get; set; }
        [DataMember]
        public int LaborMother { get; set; }
        [DataMember]
        public int HalloweenFather { get; set; }
        [DataMember]
        public int HalloweenMother { get; set; }
        [DataMember]
        public int ChildrensFather { get; set; }
        [DataMember]
        public int ChildrensMother { get; set; }
        [DataMember]
        public int MothersBdayFather { get; set; }
        [DataMember]
        public int MothersBdayMother { get; set; }
        [DataMember]
        public int FathersBdayFather { get; set; }
        [DataMember]
        public int FathersBdayMother { get; set; }
        [DataMember]
        public int ReligiousFather { get; set; }
        [DataMember]
        public int ReligiousMother { get; set; }
    }

    [DataContract]
    public class RespHoliday : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public Holiday Holidays { get; set; }
        [DataMember]
        public List<ExtraHoliday> ExtraHolidays { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class HolidayRestService : ServiceBase
    {
        public IHolidayService HolidayService { get; set; }
        public IExtraHolidayService ExtraHolidayService{ get; set; }

        public object Get(ReqHoliday request)
        {
            var holiday = HolidayService.GetByChildId(request.ChildId);
            var extraHoliday = ExtraHolidayService.GetByChildId(request.ChildId);
            return new RespHoliday()
            {
                Holidays = holiday,
                ExtraHolidays = extraHoliday
            };
        }

        public object Post(ReqHoliday request)
        {
            var holiday = request.TranslateTo<HolidayViewModel>();
            holiday.UserId = Convert.ToInt32(UserSession.CustomId);
            HolidayService.AddOrUpdate(holiday);
            return new RespHoliday();

            //var holiday = request.TranslateTo<Holiday>();
            //HolidayService.Add(holiday);
            //return new RespHoliday
            //    {
            //        Id = holiday.Id
            //    };
        }
        public object Put(ReqHoliday request)
        {
            var holiday = request.TranslateTo<Holiday>();
            holiday.UserId = Convert.ToInt32(UserSession.CustomId);
            HolidayService.Update(holiday);
            return new RespHoliday();
        }
    }
}