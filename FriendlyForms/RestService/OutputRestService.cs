using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using BusinessLogic.Helpers;
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
    #endregion

    #region ScheduleD

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
        public int WorkRelated { get; set; }
        [DataMember]
        public int HealthInsurance { get; set; }
        [DataMember]
        public int AdditionalExpenses { get; set; }
        [DataMember]
        public int ProRataParents { get; set; }
        [DataMember]
        public int ProRataAdditional { get; set; }
        [DataMember]
        public int TotalSchool { get; set; }
        [DataMember]
        public int TotalSummer { get; set; }
        [DataMember]
        public int TotalOther { get; set; }
        [DataMember]
        public int TotalBreaks { get; set; }
        [DataMember]
        public int TotalYearly { get; set; }
        [DataMember]
        public int TotalMonthly { get; set; }
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
        public int YearlyAmountFather { get; set; }
        [DataMember]
        public int YearlyAmountMother { get; set; }
        [DataMember]
        public int YearlyAmountNonParent { get; set; }
        [DataMember]
        public int YearlyAmountTotal { get; set; }
        [DataMember]
        public int MonthlyAverage { get; set; }
        [DataMember]
        public int Obligation { get; set; }
        [DataMember]
        public int SpecialExpenses { get; set; }
        [DataMember]
        public int ExpensesFactor { get; set; }
        [DataMember]
        public int MonthlyExpensesFather { get; set; }
        [DataMember]
        public int MonthlyExpensesMother { get; set; }
        [DataMember]
        public int MonthlyExpensesNonParent { get; set; }
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
        public int DeviationAmount { get; set; }
        [DataMember]
        public int CompareAmount { get; set; }
        [DataMember]
        public int CalculatedAmount { get; set; }
        [DataMember]
        public int ActualAmount { get; set; }
        [DataMember]
        public string Explaination { get; set; }
    }
    [DataContract]
    public class HighIncomeDeviation
    {
        [DataMember]
        public int Deviation { get; set; }
        [DataMember]
        public int OtherInsurance { get; set; }
        [DataMember]
        public int LifeInsurance { get; set; }
        [DataMember]
        public int ChildTaxCredit { get; set; }
        [DataMember]
        public int VisitationExpense { get; set; }
        [DataMember]
        public int Alimony { get; set; }
        [DataMember]
        public int Mortgage { get; set; }
        [DataMember]
        public int PermanancyPlan { get; set; }
        [DataMember]
        public int NonSpecific { get; set; }
        [DataMember]
        public int TotalDeviations { get; set; }
    }
    [DataContract]
    public class ExtraExpenses
    {
        [DataMember]
        public int TutitionFather { get; set; }
        [DataMember]
        public int TutitionMother { get; set; }
        [DataMember]
        public int TutitionNonParent { get; set; }
        [DataMember]
        public int TutitionNonTotal { get; set; }
        [DataMember]
        public int EducationFather { get; set; }
        [DataMember]
        public int EducationMother { get; set; }
        [DataMember]
        public int EducationNonParent { get; set; }
        [DataMember]
        public int EducationTotal { get; set; }
        [DataMember]
        public int MedicalFather { get; set; }
        [DataMember]
        public int MedicalMother { get; set; }
        [DataMember]
        public int MedicalNonParent { get; set; }
        [DataMember]
        public int MedicalTotal { get; set; }
        [DataMember]
        public int SpecialFather { get; set; }
        [DataMember]
        public int SpecialMother { get; set; }
        [DataMember]
        public int SpecialNonParent { get; set; }
        [DataMember]
        public int SpecialTotal { get; set; }
        [DataMember]
        public int TotalFather { get; set; }
        [DataMember]
        public int TotalMother { get; set; }
        [DataMember]
        public int TotalNonParent { get; set; }
        [DataMember]
        public int TotalTotal { get; set; }
        [DataMember]
        public int ProRataFather { get; set; }
        [DataMember]
        public int ProRataMother { get; set; }
        [DataMember]
        public int ProRataTotal { get; set; }
        [DataMember]
        public int PercentageFather { get; set; }
        [DataMember]
        public int PercentageMother { get; set; }
        [DataMember]
        public int DeviationFather { get; set; }
        [DataMember]
        public int DeviationMother { get; set; }
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
        public string County { get; set; }
        public string ValidSchedules { get; set; }
        public string InvalidSchedules { get; set; }
    }

    public class Csw
    {
        public int GrossIncome { get; set; }
        public int AdjustedIncome { get; set; }
        public int CombinedIncome { get; set; }
        public int SupportObligation { get; set; }
        public int ProRataObligation { get; set; }
        public int WorkRelatedExpenses { get; set; }
        public int AdjustedObligation { get; set; }
        public int AdjustedExpensesPaid { get; set; }
        public int PresumptiveAmount { get; set; }
        public int DeviationsAmount { get; set; }
        public int Subtotal { get; set; }
        public int SocialSecurity { get; set; }
        public int FinalAmount { get; set; }
        public int UninsuredExpenses { get; set; }
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
        public IOtherChildService OtherChildService { get; set; }
        public IParticipantService ParticipantService { get; set; }
        public IHealthService HealthService { get; set; }
        public IChildCareService ChildCareService { get; set; }
        public IExtraExpenseService ExtraExpenseService { get; set; }
        public IChildService ChildService { get; set; }
        public IBcsoService BcsoService { get; set; }
        public IOutputService OutputService { get; set; }
        public ICourtService CourtService { get; set; }
        public ICountyService CountyService { get; set; }

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
            var schedule = OutputService.GetScheduleB(request.UserId, parentNames.Father);
            var scheduleOther = OutputService.GetScheduleB(request.UserId, parentNames.Mother, true);
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
            var deviations = DeviationsService.GetByUserId(userId).TranslateTo<Deviations>();
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

            var highIncomeFather = new HighIncomeDeviation
                {
                    Deviation = deviations.HighDeviation ?? 0,
                    Alimony = deviations.AlimonyPaidFather ?? 0,
                    LifeInsurance = deviations.InsuranceFather ?? 0,
                    Mortgage = deviations.MortgageFather ?? 0,
                    ChildTaxCredit = deviations.TaxCreditFather ?? 0,
                    OtherInsurance = deviations.HealthFather ??0,
                    NonSpecific = deviations.NonSpecificFather ?? 0,
                    PermanancyPlan = deviations.PermanencyFather ?? 0,
                    VisitationExpense = deviations.VisitationFather ?? 0,
                };
            highIncomeFather.TotalDeviations = highIncomeFather.Deviation + highIncomeFather.Alimony +
                                               highIncomeFather.LifeInsurance + highIncomeFather.Mortgage +
                                               highIncomeFather.ChildTaxCredit + highIncomeFather.OtherInsurance +
                                               highIncomeFather.NonSpecific + highIncomeFather.PermanancyPlan +
                                               highIncomeFather.VisitationExpense;

            var highIncomeMother = new HighIncomeDeviation
            {
                Deviation = deviations.HighDeviation ?? 0,
                Alimony = deviations.AlimonyPaidMother ?? 0,
                LifeInsurance = deviations.InsuranceMother ?? 0,
                Mortgage = deviations.MortgageMother ?? 0,
                ChildTaxCredit = deviations.TaxCreditMother ?? 0,
                OtherInsurance = deviations.HealthMother ?? 0,
                NonSpecific = deviations.NonSpecificMother ?? 0,
                PermanancyPlan = deviations.PermanencyMother ?? 0,
                VisitationExpense = deviations.VisitationMother ?? 0,
            };
            highIncomeMother.TotalDeviations = highIncomeMother.Deviation + highIncomeMother.Alimony +
                                               highIncomeMother.LifeInsurance + highIncomeMother.Mortgage +
                                               highIncomeMother.ChildTaxCredit + highIncomeMother.OtherInsurance +
                                               highIncomeMother.NonSpecific + highIncomeMother.PermanancyPlan +
                                               highIncomeMother.VisitationExpense;

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

            var parentingTime = 5;
            var allowableDeviation = new AllowableDeviation
                {
                    PresumptiveAmount = deviations.Unjust,
                    BestInterest = deviations.BestInterest,
                    ImpairAbility = deviations.Impair,
                    //TODO: There's a difference for noncustodian vs custodian
                    AllowableFather = lowIncome.CalculatedAmount + highIncomeFather.TotalDeviations + totalExpenses.DeviationFather + parentingTime,
                    AllowableMother = lowIncome.CalculatedAmount + highIncomeMother.TotalDeviations + totalExpenses.DeviationFather + parentingTime,                    
                };

   
            return new ScheduleEDtoResp
            {
                LowIncomeDeviation = lowIncome,
                HighIncomeAdjusted = 0,
                HighIncomeDeviationFather = highIncomeFather,
                HighIncomeDeviationMother = highIncomeMother,
                TotalExpenses = totalExpenses,
                ParentingTime = parentingTime,
                AllowableDeviation = allowableDeviation,
                //ExtraExpenseses = extraExpenses,
                //AllowableExpenses = allowableExpenses,
                Father = parentNames.Father,
                Mother = parentNames.Mother
            };
        }

        public object Get(CswDto request)
        {
            //Setup output form            
            request.UserId = Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(request.UserId);
            var court = CourtService.GetByUserId(request.UserId).TranslateTo<Court>();
            var county = CountyService.Get(court.CountyId).CountyName;
            var cswDto = GetAllCsw(request.UserId);
            cswDto.Father = parentNames.Father;
            cswDto.Mother = parentNames.Mother;
            cswDto.County = county;
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
            var scheduleBFather = OutputService.GetScheduleB(userId, "namehere");
            var scheduleBMother = OutputService.GetScheduleB(userId, "name", true);
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
            cswTotal.SupportObligation = (int) BcsoService.GetAmount(cswTotal.AdjustedIncome, children.Count); 
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
            csw.WorkRelatedExpenses = (int) (csw.CombinedIncome*scheduleD.ProRataAdditional);
            csw.AdjustedObligation = csw.ProRataObligation + csw.WorkRelatedExpenses;
            csw.AdjustedExpensesPaid = (int) scheduleD.TotalMonthly;
            csw.PresumptiveAmount = csw.AdjustedObligation - csw.AdjustedExpensesPaid;
            csw.DeviationsAmount = 0; //TODO: comes from scheduleE
            csw.Subtotal = csw.PresumptiveAmount + csw.DeviationsAmount;
            csw.SocialSecurity = (int) (socialSecurity.Amount ?? 0.0);
            csw.FinalAmount = csw.SocialSecurity > csw.Subtotal
                                        ? csw.SocialSecurity
                                        : csw.Subtotal - csw.SocialSecurity;
            csw.UninsuredExpenses = (int) (isFather ? healthInsurance.FathersHealthAmount ?? 0.0 : healthInsurance.MothersHealthAmount ?? 0.0);
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
}
