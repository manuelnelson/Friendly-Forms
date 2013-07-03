using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Models;
using Models;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Output/Financial/ScheduleA")]
    public class ScheduleADto : IReturn<ScheduleADtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/Financial/ScheduleB/{UserId}")]
    [Route("/Output/Financial/ScheduleB")]
    public class ScheduleBDto : IReturn<ScheduleBDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/Financial/ScheduleD/{UserId}")]
    [Route("/Output/Financial/ScheduleD")]
    public class ScheduleDDto : IReturn<ScheduleDDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/Financial/ScheduleE/{UserId}")]
    [Route("/Output/Financial/ScheduleE")]
    public class ScheduleEDto : IReturn<ScheduleEDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/Financial/ChildSupportWorkSheet/{UserId}")]
    [Route("/Output/Financial/ChildSupportWorkSheet")]
    public class CswDto : IReturn<CswDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }


    #region ScheduleA
    //adding this dto to get rid of the nullable fields
    public class IncomeDto
    {
        public int HaveSalary { get; set; }
        public string OtherIncome { get; set; }
        public int W2Income { get; set; }
        public int NonW2Income { get; set; }
        public int SelfIncome { get; set; }
        public int SelfIncomeNoDeductions { get; set; }
        public int Commisions { get; set; }
        public int Bonuses { get; set; }
        public int Overtime { get; set; }
        public int Severance { get; set; }
        public int Retirement { get; set; }
        public int Interest { get; set; }
        public int Dividends { get; set; }
        public int Trust { get; set; }
        public int Annuities { get; set; }
        public int Capital { get; set; }
        public int SocialSecurity { get; set; }
        public int Compensation { get; set; }
        public int Unemployment { get; set; }
        public int CivilCase { get; set; }
        public int Gifts { get; set; }
        public int Prizes { get; set; }
        public int Alimony { get; set; }
        public int Assets { get; set; }
        public int Fringe { get; set; }
        public int Other { get; set; }
        public string OtherDetails { get; set; }
    }
    [DataContract]
    public class ScheduleADtoResp
    {
        [DataMember]
        public IncomeDto Income { get; set; }
        [DataMember]
        public IncomeDto OtherIncome { get; set; }
        [DataMember]
        public IncomeDto CombinedIncome { get; set; }
        [DataMember]
        public int IncomeTotal { get; set; }
        [DataMember]
        public int OtherIncomeTotal { get; set; }
        [DataMember]
        public int CombinedIncomeTotal { get; set; }
        [DataMember]
        public string Father { get; set; }
        [DataMember]
        public string Mother { get; set; }
    }

    #endregion

    #region ScheduleB
    [DataContract]
    public class ScheduleBDtoResp
    {
        [DataMember]
        public ScheduleB ScheduleB { get; set; }
        [DataMember]
        public ScheduleB OtherScheduleB { get; set; }
        [DataMember]
        public string Father { get; set; }
        [DataMember]
        public string Mother { get; set; }
    }
    [DataContract]
    public class ScheduleB
    {
        [DataMember]
        public int GrossIncome { get; set; }
        [DataMember]
        public int SelfEmploymentIncome { get; set; }
        [DataMember]
        public double FicaIncome { get; set; }
        [DataMember]
        public double MedicareTax { get; set; }
        [DataMember]
        public double Total34 { get; set; }
        [DataMember]
        public double Total5Minus1 { get; set; }
        [DataMember]
        public List<PreexistingSupportChild> PreexistingSupportChild { get; set; }
        [DataMember]
        public List<PreexistingSupport> PreexistingSupport { get; set; }
        [DataMember]
        public int TotalSupport { get; set; }
        [DataMember]
        public double AdjustedSupport { get; set; }

        [DataMember]
        public List<OtherChildDto> OtherChildren { get; set; }
        [DataMember]
        public string OtherChildrenDescription { get; set; }
        [DataMember]
        public double Subtotal { get; set; }
        [DataMember]
        public int GeorgiaObligations { get; set; }
        [DataMember]
        public double TheoreticalSupport { get; set; }
        [DataMember]
        public double PreexistingOrder { get; set; }
    }
    #endregion

    #region ScheduleD
    public class OtherChildDto
    {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ClaimedBy { get; set; }
    }

    [DataContract]
    public class ScheduleDDtoResp
    {
        [DataMember]
        public ScheduleD FatherScheduleD { get; set; }
        [DataMember]
        public ScheduleD MotherScheduleD { get; set; }
        [DataMember]
        public ScheduleD NonParentScheduleD { get; set; }
        [DataMember]
        public ScheduleD TotalScheduleD { get; set; }
        [DataMember]
        public List<ChildCareWithTotals> ChildCare { get; set; }
        [DataMember]
        public string Father { get; set; }
        [DataMember]
        public string Mother { get; set; }
    }

    [DataContract]
    public class ChildCareWithTotals
    {
        [DataMember]
        public int SchoolFather { get; set; }
        [DataMember]
        public int SchoolMother { get; set; }
        [DataMember]
        public int SchoolNonParent { get; set; }
        [DataMember]
        public int SummerFather { get; set; }
        [DataMember]
        public int SummerMother { get; set; }
        [DataMember]
        public int SummerNonParent { get; set; }
        [DataMember]
        public int BreaksFather { get; set; }
        [DataMember]
        public int BreaksMother { get; set; }
        [DataMember]
        public int BreaksNonParent { get; set; }
        [DataMember]
        public int OtherFather { get; set; }
        [DataMember]
        public int OtherMother { get; set; }
        [DataMember]
        public int OtherNonParent { get; set; }
        [DataMember]
        public int TotalFather { get; set; }
        [DataMember]
        public int TotalMother { get; set; }
        [DataMember]
        public int TotalNonParent { get; set; }
        [DataMember]
        public int TotalFatherMonthly { get; set; }
        [DataMember]
        public int TotalMotherMonthly { get; set; }
        [DataMember]
        public int TotalNonParentMonthly { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
    [DataContract]
    public class ScheduleD
    {
        [DataMember]
        public double WorkRelated { get; set; }
        [DataMember]
        public double HealthInsurance { get; set; }
        [DataMember]
        public double AdditionalExpenses { get; set; }
        [DataMember]
        public double ProRataParents { get; set; }
        [DataMember]
        public double ProRataAdditional { get; set; }
        [DataMember]
        public double TotalSchool { get; set; }
        [DataMember]
        public double TotalSummer { get; set; }
        [DataMember]
        public double TotalOther { get; set; }
        [DataMember]
        public double TotalBreaks { get; set; }
        [DataMember]
        public double TotalYearly { get; set; }
        [DataMember]
        public double TotalMonthly { get; set; }
    }

    #endregion

    #region ScheduleE
    [DataContract]
    public class ScheduleEDtoResp
    {
        [DataMember]
        public LowIncomeDeviation LowIncomeDeviation { get; set; }
        [DataMember]
        public double HighIncomeAdjusted { get; set; }
        [DataMember]
        public HighIncomeDeviation HighIncomeDeviationFather { get; set; }
        [DataMember]
        public HighIncomeDeviation HighIncomeDeviationMother { get; set; }
        [DataMember]
        public ExtraExpenses TotalExpenses { get; set; }

        [DataMember]
        public double ParentingTime { get; set; }
        [DataMember]
        public AllowableDeviation AllowableDeviation { get; set; }
        [DataMember]
        public List<ExtraExpenses> ExtraExpenseses { get; set; }

        [DataMember]
        public AllowableExpenses AllowableExpenses { get; set; }

        [DataMember]
        public string Father { get; set; }
        [DataMember]
        public string Mother { get; set; }
    }

    [DataContract]
    public class AllowableExpenses
    {
        [DataMember]
        public double YearlyAmountFather { get; set; }
        [DataMember]
        public double YearlyAmountMother { get; set; }
        [DataMember]
        public double YearlyAmountNonParent { get; set; }
        [DataMember]
        public double YearlyAmountTotal { get; set; }
        [DataMember]
        public double MonthlyAverage { get; set; }
        [DataMember]
        public double Obligation { get; set; }
        [DataMember]
        public double SpecialExpenses { get; set; }
        [DataMember]
        public double ExpensesFactor { get; set; }
        [DataMember]
        public double MonthlyExpensesFather { get; set; }
        [DataMember]
        public double MonthlyExpensesMother { get; set; }
        [DataMember]
        public double MonthlyExpensesNonParent { get; set; }
    }

    [DataContract]
    public class AllowableDeviation
    {
        [DataMember]
        public int AllowableFather { get; set; }
        [DataMember]
        public int AllowableMother { get; set; }
        [DataMember]
        public string PresumptiveAmount { get; set; }
        [DataMember]
        public string BestInterest { get; set; }
        [DataMember]
        public string ImpairAbility { get; set; }
    }

    [DataContract]
    public class LowIncomeDeviation
    {
        [DataMember]
        public double DeviationAmount { get; set; }
        [DataMember]
        public double CompareAmount { get; set; }
        [DataMember]
        public double CalculatedAmount { get; set; }
        [DataMember]
        public double ActualAmount { get; set; }
        [DataMember]
        public string Explaination { get; set; }
    }
    [DataContract]
    public class HighIncomeDeviation
    {
        [DataMember]
        public double Deviation { get; set; }
        [DataMember]
        public double OtherInsurance { get; set; }
        [DataMember]
        public double LifeInsurance { get; set; }
        [DataMember]
        public double ChildTaxCredit { get; set; }
        [DataMember]
        public double VisitationExpense { get; set; }
        [DataMember]
        public double Alimony { get; set; }
        [DataMember]
        public double Mortgage { get; set; }
        [DataMember]
        public double PermanancyPlan { get; set; }
        [DataMember]
        public double NonSpecific { get; set; }
        [DataMember]
        public double TotalDeviations { get; set; }
    }
    [DataContract]
    public class ExtraExpenses
    {
        [DataMember]
        public double TutitionFather { get; set; }
        [DataMember]
        public double TutitionMother { get; set; }
        [DataMember]
        public double TutitionNonParent { get; set; }
        [DataMember]
        public double TutitionNonTotal { get; set; }
        [DataMember]
        public double EducationFather { get; set; }
        [DataMember]
        public double EducationMother { get; set; }
        [DataMember]
        public double EducationNonParent { get; set; }
        [DataMember]
        public double EducationTotal { get; set; }
        [DataMember]
        public double MedicalFather { get; set; }
        [DataMember]
        public double MedicalMother { get; set; }
        [DataMember]
        public double MedicalNonParent { get; set; }
        [DataMember]
        public double MedicalTotal { get; set; }
        [DataMember]
        public double SpecialFather { get; set; }
        [DataMember]
        public double SpecialMother { get; set; }
        [DataMember]
        public double SpecialNonParent { get; set; }
        [DataMember]
        public double SpecialTotal { get; set; }
        [DataMember]
        public double TotalFather { get; set; }
        [DataMember]
        public double TotalMother { get; set; }
        [DataMember]
        public double TotalNonParent { get; set; }
        [DataMember]
        public double TotalTotal { get; set; }
        [DataMember]
        public double ProRataFather { get; set; }
        [DataMember]
        public double ProRataMother { get; set; }
        [DataMember]
        public double ProRataTotal { get; set; }
        [DataMember]
        public double PercentageFather { get; set; }
        [DataMember]
        public double PercentageMother { get; set; }
        [DataMember]
        public double DeviationFather { get; set; }
        [DataMember]
        public double DeviationMother { get; set; }
        [DataMember]
        public string SpecialDescriptionFather { get; set; }
        [DataMember]
        public string SpecialDescriptionMother { get; set; }
        [DataMember]
        public string SpecialDescriptionNonParent { get; set; }
        [DataMember]
        public int ExtraSpent { get; set; }
    }
    #endregion

    #region CSW
    public class CswDtoResp
    {
        public List<Child> Children { get; set; }
        public Csw FatherCsw { get; set; }
        public Csw MotherCsw { get; set; }
        public Csw TotalCsw { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string ValidSchedules { get; set; }
        public string InvalidSchedules { get; set; }
    }

    public class Csw
    {
        public double GrossIncome { get; set; }
        public double AdjustedIncome { get; set; }
        public double CombinedIncome { get; set; }
        public double SupportObligation { get; set; }
        public double ProRataObligation { get; set; }
        public double WorkRelatedExpenses { get; set; }
        public double AdjustedObligation { get; set; }
        public double AdjustedExpensesPaid { get; set; }
        public double PresumptiveAmount { get; set; }
        public double DeviationsAmount { get; set; }
        public double Subtotal { get; set; }
        public double SocialSecurity { get; set; }
        public double FinalAmount { get; set; }
        public double UninsuredExpenses { get; set; }
    }

    #endregion
    [Authenticate]
    public class OutputsService : ServiceBase
    {
        public IIncomeService IncomeService { get; set; }
        public ISocialSecurityService SocialSecurityService { get; set; }
        public IPreexistingSupportService PreexistingSupportService { get; set; }
        public IPreexistingSupportFormService PreexistingSupportFormService { get; set; }
        public IPreexistingSupportChildService PreexistingSupportChildService { get; set; }
        public IOtherChildrenService OtherChildrenService { get; set; }
        public IDeviationsService DeviationsService { get; set; }
        public IDeviationsFormService DeviationsFormService { get; set; }
        public IOtherChildService OtherChildService { get; set; }
        public IParticipantService ParticipantService { get; set; }
        public IHealthService HealthService { get; set; }
        public IChildCareService ChildCareService { get; set; }
        public IExtraExpenseService ExtraExpenseService { get; set; }
        public IChildService ChildService { get; set; }
        public IBcsoService BcsoService { get; set; }

        public object Get(ScheduleADto request)
        {
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            //Setup output form            
            var parentNames = GetParentNames(request.UserId);
            var schedule = GetScheduleA(request.UserId);
            schedule.Father = parentNames.Father;
            schedule.Mother = parentNames.Mother;
            return schedule;
        }

        public object Get(ScheduleBDto request)
        {
            //Setup output form            
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(request.UserId);
            var schedule = GetScheduleB(request.UserId, parentNames.Father);
            var scheduleOther = GetScheduleB(request.UserId, parentNames.Mother, true);
            return new ScheduleBDtoResp
                {
                    ScheduleB = schedule,
                    OtherScheduleB = scheduleOther,
                    Father = parentNames.Father,
                    Mother = parentNames.Mother
                };
        }

        public object Get(ScheduleDDto request)
        {
            //Setup output form            
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(request.UserId);
            var scheduleD = GetScheduleD(request.UserId);
            scheduleD.Father = parentNames.Father;
            scheduleD.Mother = parentNames.Mother;
            return scheduleD;
        }

        public object Get(ScheduleEDto request)
        {
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            var userId = request.UserId;
            var deviations = DeviationsFormService.GetByUserId(userId).TranslateTo<DeviationsForm>();
            var cswAll = GetAllCsw(userId);
            var children = ChildService.GetByUserId(userId);
            var participants = ParticipantService.GetByUserId(userId) as ParticipantViewModel;
            var extraExpenses = ExtraExpenseService.GetByUserId(userId).TranslateTo<ExtraExpense>();
            var custodyInformation = ParticipantService.GetCustodyInformation(participants);
            var lowIncome = new LowIncomeDeviation
                {
                    DeviationAmount = deviations.LowDeviation ?? 0,
                };
            lowIncome.CompareAmount = custodyInformation.NonCustodyIsFather
                                          ? (cswAll.FatherCsw.PresumptiveAmount - lowIncome.DeviationAmount)
                                          : (cswAll.MotherCsw.PresumptiveAmount - lowIncome.DeviationAmount);
            var minChildSupportAmount = 100 + children.Children.Count * 50;
            lowIncome.CalculatedAmount = Math.Abs(minChildSupportAmount - lowIncome.CompareAmount);
            //TODO: still confused on this one. Ask for clarification
            lowIncome.ActualAmount = minChildSupportAmount > lowIncome.CalculatedAmount ? 0 : 10;
            lowIncome.Explaination = deviations.WhyLow;

            var highIncome = new HighIncomeDeviation
                {
                    Deviation = deviations.HighDeviation ?? 0,
                };
            var parentNames = GetParentNames(userId);
            var totalExpenses = new ExtraExpenses
                {
                    EducationFather = extraExpenses.EducationFather,
                    EducationMother = extraExpenses.EducationMother,
                    EducationNonParent = extraExpenses.EducationNonParent,
                    EducationTotal = extraExpenses.EducationFather + extraExpenses.EducationMother + extraExpenses.EducationNonParent,
                    MedicalFather = extraExpenses.MedicalFather,
                    MedicalMother = extraExpenses.MedicalMother,
                    MedicalNonParent = extraExpenses.MedicalNonParent,
                    MedicalTotal = extraExpenses.MedicalFather + extraExpenses.MedicalMother + extraExpenses.MedicalNonParent,
                    SpecialFather = extraExpenses.SpecialFather,
                    SpecialMother = extraExpenses.SpecialMother,
                    SpecialNonParent = extraExpenses.SpecialNonParent,
                    SpecialTotal = extraExpenses.SpecialFather + extraExpenses.SpecialMother + extraExpenses.SpecialNonParent,
                    TotalFather = extraExpenses.EducationFather + extraExpenses.MedicalFather + extraExpenses.SpecialFather,
                    TotalMother = extraExpenses.EducationMother + extraExpenses.MedicalMother + extraExpenses.SpecialMother,
                    TotalNonParent = extraExpenses.EducationNonParent + extraExpenses.MedicalNonParent + extraExpenses.SpecialNonParent,
                    ProRataFather = cswAll.FatherCsw.CombinedIncome,
                    ProRataMother = cswAll.MotherCsw.CombinedIncome,
                    ProRataTotal = 100,                    
                };
            totalExpenses.TotalTotal = totalExpenses.TotalFather + totalExpenses.TotalMother + totalExpenses.TotalNonParent;
            totalExpenses.PercentageFather = totalExpenses.TotalFather*totalExpenses.ProRataFather;
            totalExpenses.PercentageMother = totalExpenses.TotalMother*totalExpenses.ProRataMother;
            totalExpenses.DeviationFather = totalExpenses.PercentageFather - totalExpenses.TotalFather;
            totalExpenses.DeviationMother = totalExpenses.PercentageMother - totalExpenses.TotalMother;


            var allowableDeviation = new AllowableDeviation();
            //var extraExpenses = new List<ExtraExpenses>();
            var allowableExpenses = new AllowableExpenses();
            return new ScheduleEDtoResp
            {
                LowIncomeDeviation = lowIncome,
                HighIncomeAdjusted = 0,
                HighIncomeDeviationFather = highIncome,
                HighIncomeDeviationMother = highIncome,
                TotalExpenses = totalExpenses,
                ParentingTime = 5,
                AllowableDeviation = allowableDeviation,
                //ExtraExpenseses = extraExpenses,
                AllowableExpenses = allowableExpenses,
                Father = parentNames.Father,
                Mother = parentNames.Mother
            };
        }

        public object Get(CswDto request)
        {
            //Setup output form            
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(request.UserId);
            var cswDto = GetAllCsw(request.UserId);
            cswDto.Father = parentNames.Father;
            cswDto.Mother = parentNames.Mother;
            return cswDto;
        }

        private ScheduleADtoResp GetScheduleA(long userId)
        {
            var income = IncomeService.GetByUserId(userId).TranslateTo<IncomeDto>();
            var incomeOther = IncomeService.GetByUserId(userId, isOtherParent: true).TranslateTo<IncomeDto>();
            var incomeCombined = new IncomeDto
            {
                Alimony = income.Alimony + incomeOther.Alimony,
                Annuities = income.Annuities + incomeOther.Annuities,
                Assets = income.Assets + incomeOther.Assets,
                Bonuses = income.Bonuses + incomeOther.Bonuses,
                Capital = income.Capital + incomeOther.Capital,
                CivilCase = income.CivilCase + incomeOther.CivilCase,
                Commisions = income.Commisions + incomeOther.Commisions,
                Compensation = income.Compensation + incomeOther.Compensation,
                Dividends = income.Dividends + incomeOther.Dividends,
                Fringe = income.Fringe + incomeOther.Fringe,
                Gifts = income.Gifts + incomeOther.Gifts,
                HaveSalary = income.HaveSalary + incomeOther.HaveSalary,
                Interest = income.Interest + incomeOther.Interest,
                NonW2Income = income.NonW2Income + incomeOther.NonW2Income,
                Other = income.Other + incomeOther.Other,
                OtherIncome = income.OtherIncome + incomeOther.OtherIncome,
                Overtime = income.Overtime + incomeOther.Overtime,
                Prizes = income.Prizes + incomeOther.Prizes,
                Retirement = income.Retirement + incomeOther.Retirement,
                SelfIncome = income.SelfIncome + incomeOther.SelfIncome,
                SelfIncomeNoDeductions = income.SelfIncomeNoDeductions + incomeOther.SelfIncomeNoDeductions,
                Severance = income.Severance + incomeOther.Severance,
                SocialSecurity = income.SocialSecurity + incomeOther.SocialSecurity,
                Trust = income.Trust + incomeOther.Trust,
                Unemployment = income.Unemployment + incomeOther.Unemployment,
                W2Income = income.W2Income + incomeOther.W2Income,
            };
            return new ScheduleADtoResp()
            {
                Income = income,
                OtherIncome = incomeOther,
                CombinedIncome = incomeCombined,
                IncomeTotal = income.CalculateTotalIncome(),
                OtherIncomeTotal = incomeOther.CalculateTotalIncome(),
                CombinedIncomeTotal = incomeCombined.CalculateTotalIncome()
            };
        }
        private ScheduleB GetScheduleB(long userId, string parentName, bool isOtherParent = false)
        {
            var income = IncomeService.GetByUserId(userId, isOtherParent).TranslateTo<IncomeDto>();
            var preexistingSupport = PreexistingSupportFormService.GetByUserId(userId, isOtherParent);
            var otherChildren = OtherChildrenService.GetByUserId(userId, isOtherParent);

            var schedule = new ScheduleB
            {
                GrossIncome = income.CalculateTotalIncome(),
                SelfEmploymentIncome = income.SelfIncome
            };
            schedule.FicaIncome = schedule.SelfEmploymentIncome * .62;
            schedule.MedicareTax = schedule.SelfEmploymentIncome * .0145;
            schedule.Total34 = schedule.FicaIncome + schedule.MedicareTax;
            schedule.Total5Minus1 = schedule.GrossIncome - schedule.Total34;
            if (preexistingSupport != null)
            {
                var preexistingSupportChildren = PreexistingSupportChildService.GetChildrenBySupportId(preexistingSupport.Id).ToList();
                schedule.PreexistingSupportChild = preexistingSupportChildren.ToList();
                schedule.PreexistingSupport = preexistingSupportChildren.Select(x => x.PreexistingSupport).ToList();
                schedule.TotalSupport = schedule.PreexistingSupport.Sum(c => c.Monthly);
            }
            schedule.AdjustedSupport = schedule.Total5Minus1 - schedule.TotalSupport;
            if (otherChildren != null)
            {
                schedule.OtherChildren = OtherChildService.GetChildrenByOtherChildrenId(otherChildren.Id).Select(x => x.TranslateTo<OtherChildDto>()).ToList();
                foreach (var otherChildDto in schedule.OtherChildren)
                {
                    otherChildDto.ClaimedBy = parentName;
                }
                schedule.OtherChildrenDescription = otherChildren.Details;
            }
            schedule.Subtotal = Math.Abs(schedule.Total5Minus1 - 0.0) > 0.01 ? schedule.Total5Minus1 : schedule.GrossIncome;
            //Todo: get this number
            schedule.GeorgiaObligations = 0;
            schedule.TheoreticalSupport = schedule.GeorgiaObligations * .75;
            schedule.PreexistingOrder = Math.Abs(schedule.AdjustedSupport - 0) > 0.01
                                            ? schedule.AdjustedSupport - schedule.TheoreticalSupport
                                            : schedule.Subtotal - schedule.TheoreticalSupport;
            return schedule;
        }
        private ScheduleDDtoResp GetScheduleD(long userId)
        {
            var health = HealthService.GetByUserId(userId) as HealthViewModel;
            var childCares = ChildCareService.GetAllByUserId(userId);
            var childCaresWithTotals = childCares.Select(childCare => childCare.TranslateTo<ChildCareWithTotals>()).ToList();

            var schedule = new ScheduleD
            {
                HealthInsurance = health.FathersHealthAmount ?? 0
            };
            var otherSchedule = new ScheduleD
            {
                HealthInsurance = health.MothersHealthAmount ?? 0
            };
            var nonParentSchedule = new ScheduleD
            {
                HealthInsurance = health.NonCustodialHealthAmount ?? 0
            };
            var totalScheduleD = new ScheduleD
            {
                HealthInsurance = health.NonCustodialHealthAmount ?? 0
            };

            for (var i = 0; i < childCares.Count; i++)
            {
                var childCareWithTotal = childCaresWithTotals[i];
                var childCare = childCares[i];
                childCareWithTotal.TotalFather = childCareWithTotal.BreaksFather + childCareWithTotal.OtherFather +
                                                   childCareWithTotal.SchoolFather + childCareWithTotal.SummerFather;
                childCareWithTotal.TotalMother = childCareWithTotal.BreaksMother + childCareWithTotal.OtherMother +
                                                   childCareWithTotal.SchoolMother + childCareWithTotal.SummerMother;
                childCareWithTotal.TotalNonParent = childCareWithTotal.BreaksNonParent + childCareWithTotal.OtherNonParent +
                                                      childCareWithTotal.SchoolNonParent + childCareWithTotal.SummerNonParent;
                childCareWithTotal.TotalFatherMonthly = childCareWithTotal.TotalFather / 12;
                childCareWithTotal.TotalMotherMonthly = childCareWithTotal.TotalMother / 12;
                childCareWithTotal.TotalNonParentMonthly = childCareWithTotal.TotalNonParent / 12;
                childCareWithTotal.Name = childCare.Child.Name;
                schedule.TotalSummer += childCare.SummerFather;
                otherSchedule.TotalSummer += childCare.SummerMother;
                nonParentSchedule.TotalSummer += childCare.SummerNonParent;
                schedule.TotalSchool += childCare.SchoolFather;
                otherSchedule.TotalSchool += childCare.SchoolMother;
                nonParentSchedule.TotalSchool += childCare.SchoolNonParent;
                schedule.TotalBreaks += childCare.BreaksFather;
                otherSchedule.TotalBreaks += childCare.BreaksMother;
                nonParentSchedule.TotalBreaks += childCare.BreaksNonParent;
                schedule.TotalOther += childCare.OtherFather;
                otherSchedule.TotalOther += childCare.OtherMother;
                nonParentSchedule.TotalOther += childCare.OtherNonParent;
                schedule.TotalYearly += childCareWithTotal.TotalFather;
                otherSchedule.TotalYearly += childCareWithTotal.TotalMother;
                nonParentSchedule.TotalYearly += childCareWithTotal.TotalNonParent;
            }
            schedule.TotalMonthly = schedule.TotalYearly / 12;
            otherSchedule.TotalMonthly = otherSchedule.TotalYearly / 12;
            nonParentSchedule.TotalMonthly = nonParentSchedule.TotalYearly / 12;

            schedule.WorkRelated = schedule.TotalYearly;
            otherSchedule.WorkRelated = otherSchedule.TotalYearly;
            nonParentSchedule.WorkRelated = nonParentSchedule.TotalYearly;
            schedule.AdditionalExpenses = schedule.WorkRelated + schedule.HealthInsurance;
            otherSchedule.AdditionalExpenses = otherSchedule.WorkRelated + otherSchedule.HealthInsurance;
            nonParentSchedule.AdditionalExpenses = nonParentSchedule.WorkRelated + nonParentSchedule.HealthInsurance;
            schedule.ProRataParents = 0;
            otherSchedule.ProRataParents = 0;
            nonParentSchedule.ProRataParents = 0;
            schedule.ProRataAdditional = 0;
            otherSchedule.ProRataAdditional = 0;
            nonParentSchedule.ProRataAdditional = 0;
            return new ScheduleDDtoResp()
                {
                    FatherScheduleD = schedule,
                    MotherScheduleD = otherSchedule,
                    NonParentScheduleD = nonParentSchedule,
                    TotalScheduleD = totalScheduleD,
                    ChildCare = childCaresWithTotals,
                };
        }
        private CswDtoResp GetAllCsw(long userId)
        {
            var scheduleA = GetScheduleA(userId);
            var scheduleBFather = GetScheduleB(userId, "namehere");
            var scheduleBMother = GetScheduleB(userId, "name", true);
            var scheduleD = GetScheduleD(userId);
            var socialSecurityFather = SocialSecurityService.GetByUserId(userId);
            var socialSecurityMother = SocialSecurityService.GetByUserId(userId, true);
            var healthInsurance = HealthService.GetByUserId(userId) as HealthViewModel;
            var totalIncome = scheduleBFather.AdjustedSupport + scheduleBMother.AdjustedSupport;
            var cswFather = new Csw
                {
                    GrossIncome = scheduleA.IncomeTotal,
                    AdjustedIncome = scheduleBFather.AdjustedSupport,
                    //Apparently this could be 14 as well? whats the logic here?
                    CombinedIncome = scheduleBFather.AdjustedSupport / totalIncome,
                };
            var cswMother = new Csw
            {
                GrossIncome = scheduleA.IncomeTotal,
                AdjustedIncome = scheduleBMother.AdjustedSupport,
                //Apparently this could be 14 as well? whats the logic here?
                CombinedIncome = scheduleBMother.AdjustedSupport / totalIncome,
            };
            var cswTotal = new Csw()
                {
                    GrossIncome = cswFather.GrossIncome + cswMother.GrossIncome,
                    AdjustedIncome = cswFather.AdjustedIncome + cswMother.AdjustedIncome,
                    CombinedIncome = 100
                };
            var children = ChildService.GetByUserId(userId).Children;
            cswTotal.SupportObligation = BcsoService.GetAmount(cswTotal.AdjustedIncome, children.Count); 
            cswFather = FinishCsw(cswFather, cswTotal, scheduleD.FatherScheduleD, socialSecurityFather, healthInsurance);
            cswMother = FinishCsw(cswMother, cswTotal, scheduleD.MotherScheduleD, socialSecurityMother, healthInsurance);

            return new CswDtoResp()
                {
                    FatherCsw = cswFather,
                    MotherCsw = cswMother,
                    TotalCsw = cswTotal,
                    Children = children
                };
        }
        private static Csw FinishCsw(Csw csw, Csw cswTotal, ScheduleD scheduleD, SocialSecurityViewModel socialSecurity, HealthViewModel healthInsurance, bool isFather=true)
        {
            csw.ProRataObligation = csw.CombinedIncome*cswTotal.SupportObligation;
            csw.WorkRelatedExpenses = csw.CombinedIncome*scheduleD.ProRataAdditional;
            csw.AdjustedObligation = csw.ProRataObligation + csw.WorkRelatedExpenses;
            csw.AdjustedExpensesPaid = scheduleD.TotalMonthly;
            csw.PresumptiveAmount = csw.AdjustedObligation - csw.AdjustedExpensesPaid;
            csw.DeviationsAmount = 0; //TODO: comes from scheduleE
            csw.Subtotal = csw.PresumptiveAmount + csw.DeviationsAmount;
            csw.SocialSecurity = socialSecurity.Amount ?? 0.0;
            csw.FinalAmount = csw.SocialSecurity > csw.Subtotal
                                        ? csw.SocialSecurity
                                        : csw.Subtotal - csw.SocialSecurity;
            csw.UninsuredExpenses = isFather ? healthInsurance.FathersHealthAmount ?? 0.0 : healthInsurance.MothersHealthAmount ?? 0.0;
            return csw;
        }
        private ParentNames GetParentNames(long userId)
        {
            var participants = ParticipantService.GetByUserId(userId) as ParticipantViewModel;
            var outputViewModel = new PpOutputFormHelper
            {
                CustodyInformation = ParticipantService.GetCustodyInformation(participants)
            };
            return new ParentNames
                {
                    Father = outputViewModel.CustodyInformation.NonCustodyIsFather
                                 ? outputViewModel.CustodyInformation.NonCustodyParentName
                                 : outputViewModel.CustodyInformation.CustodyParentName,
                    Mother = outputViewModel.CustodyInformation.NonCustodyIsFather
                             ? outputViewModel.CustodyInformation.CustodyParentName
                             : outputViewModel.CustodyInformation.NonCustodyParentName
                };        
        }
    }

    internal class ParentNames
    {
        public string Father { get; set; }
        public string Mother { get; set; }
    }

    public static class OutputHelper
    {
        public static int CalculateTotalIncome(this IncomeDto income)
        {
            return income.W2Income + income.Commisions + income.SelfIncomeNoDeductions + income.Bonuses + income.Overtime +
                   income.Severance + income.Retirement + income.Interest + income.Dividends + income.Trust + income.Annuities +
                   income.Capital + income.SocialSecurity + income.Compensation + income.Unemployment + income.CivilCase + income.Gifts + income.Prizes +
                   income.Alimony + income.Assets + income.Fringe + income.Other;
        }
    }

}
