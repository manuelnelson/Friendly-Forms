﻿using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class Holiday : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public bool FridayHoliday { get; set; }
        public bool MondayHoliday { get; set; }
        public int Thanksgiving { get; set; }
        public string ThanksgivingOther { get; set; }
        public string ThanksgivingTime { get; set; }
        public int Christmas { get; set; }
        public string ChristmasTime { get; set; }
        public string ChristmasOther { get; set; }
        public int SpringBreak { get; set; }
        public string SpringOther { get; set; }
        public int SummerBeginDays { get; set; }
        public string SummerBeginTime { get; set; }
        public int SummerEndDays { get; set; }
        public string SummerEndTime { get; set; }
        public string SummerDetails { get; set; }
        public int FallBreak { get; set; }
        public string FallOther { get; set; }
        public int ChristmasFather { get; set; }
        public int ChristmasMother { get; set; }
        public int SpringBreakFather { get; set; }
        public int SpringBreakMother { get; set; }
        public int FallBreakFather { get; set; }
        public int FallBreakMother { get; set; }
        public int ThanksgivingFather { get; set; }
        public int ThanksgivingMother { get; set; }
        public int MlkFather { get; set; }
        public int MlkMother { get; set; }
        public int PresidentsFather { get; set; }
        public int PresidentsMother { get; set; }
        public int MothersFather { get; set; }
        public int MothersMother { get; set; }
        public int MemorialFather { get; set; }
        public int MemorialMother { get; set; }
        public int FathersFather { get; set; }
        public int FathersMother { get; set; }
        public int IndependenceFather { get; set; }
        public int IndependenceMother { get; set; }
        public int LaborFather { get; set; }
        public int LaborMother { get; set; }
        public int HalloweenFather { get; set; }
        public int HalloweenMother { get; set; }
        public int ChildrensFather { get; set; }
        public int ChildrensMother { get; set; }
        public int MothersBdayFather { get; set; }
        public int MothersBdayMother { get; set; }
        public int FathersBdayFather { get; set; }
        public int FathersBdayMother { get; set; }
        public int ReligiousFather { get; set; }
        public int ReligiousMother { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        public virtual Child Child { get; set; }


        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<HolidayViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Holiday)entity;
            ChildrensFather = updatingEntity.ChildrensFather;
            ChildrensMother = updatingEntity.ChildrensMother;
            Christmas = updatingEntity.Christmas;
            ChristmasOther = updatingEntity.ChristmasOther;
            ChristmasTime = updatingEntity.ChristmasTime;
            FallBreak = updatingEntity.FallBreak;
            FallOther = updatingEntity.FallOther;
            FathersBdayFather = updatingEntity.FathersBdayFather;
            FathersBdayMother = updatingEntity.FathersBdayMother;
            FathersFather = updatingEntity.FathersFather;
            FathersMother = updatingEntity.FathersMother;
            FridayHoliday = updatingEntity.FridayHoliday;
            ChristmasFather = updatingEntity.ChristmasFather;
            ChristmasMother = updatingEntity.ChristmasMother;
            SpringBreakFather = updatingEntity.SpringBreakFather;
            SpringBreakMother = updatingEntity.SpringBreakMother;
            FallBreakFather = updatingEntity.FallBreakFather;
            FallBreakMother = updatingEntity.FallBreakMother;
            ThanksgivingFather = updatingEntity.ThanksgivingFather;
            ThanksgivingMother = updatingEntity.ThanksgivingMother;
            ThanksgivingTime = updatingEntity.ThanksgivingTime;            
            HalloweenFather = updatingEntity.HalloweenFather;
            HalloweenMother = updatingEntity.HalloweenMother;
            IndependenceFather = updatingEntity.IndependenceFather;
            IndependenceMother = updatingEntity.IndependenceMother;
            LaborFather = updatingEntity.LaborFather;
            LaborMother = updatingEntity.LaborMother;
            MemorialFather = updatingEntity.MemorialFather;
            MemorialMother = updatingEntity.MemorialMother;
            MlkFather = updatingEntity.MlkFather;
            MlkMother = updatingEntity.MlkMother;
            MondayHoliday = updatingEntity.MondayHoliday;
            MothersBdayFather = updatingEntity.MothersBdayFather;
            MothersBdayMother = updatingEntity.MothersBdayMother;
            MothersFather = updatingEntity.MothersFather;
            MothersMother = updatingEntity.MothersMother;
            PresidentsFather = updatingEntity.PresidentsFather;
            PresidentsMother = updatingEntity.PresidentsMother;
            ReligiousFather = updatingEntity.ReligiousFather;
            ReligiousMother = updatingEntity.ReligiousMother;
            SpringBreak = updatingEntity.SpringBreak;
            SpringOther = updatingEntity.SpringOther;
            SummerBeginDays = updatingEntity.SummerBeginDays;
            SummerBeginTime = updatingEntity.SummerBeginTime;
            SummerEndDays = updatingEntity.SummerEndDays;
            SummerEndTime = updatingEntity.SummerEndTime;
            SummerDetails = updatingEntity.SummerDetails;
            Thanksgiving = updatingEntity.Thanksgiving;
            ThanksgivingOther = updatingEntity.ThanksgivingOther;
        }
    }
}
