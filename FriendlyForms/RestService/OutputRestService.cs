using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using BusinessLogic.Helpers;
using BusinessLogic.Properties;
using FriendlyForms.Helpers;
using FriendlyForms.Models;
using Models;
using Models.ViewModels;
using Pechkin;
using Pechkin.Synchronized;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Output/ScheduleA")]
    [Route("/Output/ScheduleA/{UserId}")]
    public class ScheduleADto : IReturn<ScheduleADtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/ScheduleB/{UserId}")]
    [Route("/Output/ScheduleB")]
    public class ScheduleBDto : IReturn<ScheduleBDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/ScheduleD/{UserId}")]
    [Route("/Output/ScheduleD")]
    public class ScheduleDDto : IReturn<ScheduleDDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/ScheduleE/{UserId}")]
    [Route("/Output/ScheduleE")]
    public class ScheduleEDto : IReturn<ScheduleEDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/ChildSupport/{UserId}")]
    [Route("/Output/ChildSupport")]
    public class CswDto : IReturn<CswDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/CSA/{UserId}")]
    [Route("/Output/CSA")]
    public class CsaDto : IReturn<CsaDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/Parenting")]
    public class ParentingPlanDto : IReturn<ParentingPlanDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/DomesticMediation")]
    public class DomesticMediationDto : IReturn<DomesticMediationDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
    }

    [DataContract]
    [Route("/Output/FormComplete")]
    public class FormCompleteDto : IReturn<FormCompleteDtoResp>
    {
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string FormName { get; set; }
    }

    [DataContract]
    [Route("/Output/Pdf")]
    public class PdfDto : IReturn<PdfDtoResp>
    {
        [DataMember]
        public string Html { get; set; }
    }


    #region CheckOutput
    [DataContract]
    public class FormCompleteDtoResp
    {
        [DataMember]
        public List<IncompleteForm> IncompleteForms { get; set; } 
    }
    #endregion

    #region Parenting
    [DataContract]
    public class ParentingPlanDtoResp
    {
        [DataMember]
        public Court CourtViewModel { get; set; }
        [DataMember]
        public Participant ParticipantViewModel { get; set; }
        [DataMember]
        public List<Child> Children { get; set; }
        [DataMember]
        public Privacy PrivacyViewModel { get; set; }
        [DataMember]
        public Information InformationViewModel { get; set; }
        [DataMember]
        public List<ChildDecisions> ChildDecisions { get; set; }
        [DataMember]
        public string Disagreement { get; set; }
        [DataMember]
        public Responsibility ResponsibilityViewModel { get; set; }
        [DataMember]
        public Communication CommunicationViewModel { get; set; }
        [DataMember]
        public Schedule ScheduleViewModel { get; set; }
        [DataMember]
        public List<ChildHoliday> ChildHolidays { get; set; }
        [DataMember]
        public Addendum AddendumViewModel { get; set; }
        [DataMember]
        public FormsCompleted FormsCompleted { get; set; }
        //Only used for output form
        [DataMember]
        public PpOutputFormHelper PpOutputFormHelper { get; set; }
        [DataMember]
        public int FormUserId { get; set; }
        [DataMember]
        public string County { get; set; }
    }

    [DataContract]
    public class ChildDecisions
    {
        [DataMember]
        public Child Child { get; set; }
        [DataMember]
        public Decisions Decisions { get; set; }
        [DataMember]
        public List<ExtraDecisions> ExtraDecisions { get; set; }
    }
    [DataContract]
    public class ChildHoliday
    {
        [DataMember]
        public Child Child { get; set; }
        [DataMember]
        public Holiday Holiday { get; set; }
        [DataMember]
        public List<ExtraHoliday> ExtraHolidays { get; set; }
    }
    #endregion 

    #region Domestic Mediation
    [DataContract]
    public class DomesticMediationDtoResp
    {
        [DataMember]
        public House HouseViewModel { get; set; }
        [DataMember]
        public Property PropertyViewModel { get; set; }
        [DataMember]
        public List<Vehicle> Vehicles { get; set; }
        [DataMember]
        public VehicleForm VehicleForm { get; set; }
        [DataMember]
        public Debt DebtViewModel { get; set; }
        [DataMember]
        public Assets AssetViewModel { get; set; }
        [DataMember]
        public HealthInsurance HealthInsuranceViewModel { get; set; }
        [DataMember]
        public SpousalSupport SpousalViewModel { get; set; }
        [DataMember]
        public Tax TaxViewModel { get; set; }
        [DataMember]
        public ChildSupport ChildSupportViewModel { get; set; }
        [DataMember]
        public Participant ParticipantsViewModel { get; set; }
        [DataMember]
        public Court CourtViewModel { get; set; }
        [DataMember]
        public bool HasChildren { get; set; }
        [DataMember]
        public int FormUserId { get; set; }
    }
    #endregion

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
        public double IncomeTotal { get; set; }
        [DataMember]
        public double OtherIncomeTotal { get; set; }
        [DataMember]
        public double CombinedIncomeTotal { get; set; }
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
        public ParentNames ParentNames { get; set; }
        //[DataMember]
        //public string Father { get; set; }
        //[DataMember]
        //public string Mother { get; set; }
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
        public double TotalFatherMonthly { get; set; }
        [DataMember]
        public double TotalMotherMonthly { get; set; }
        [DataMember]
        public double TotalNonParentMonthly { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
    [DataContract]
    public class ScheduleD
    {
        [DataMember]
        public double WorkRelated { get; set; }
        [DataMember]
        public int HealthInsurance { get; set; }
        [DataMember]
        public double AdditionalExpenses { get; set; }
        [DataMember]
        public double ProRataParents { get; set; }
        [DataMember]
        public double ProRataAdditional { get; set; }
        [DataMember]
        public double TotalSchool { get; set; }
        [DataMember]
        public int TotalSummer { get; set; }
        [DataMember]
        public int TotalOther { get; set; }
        [DataMember]
        public int TotalBreaks { get; set; }
        [DataMember]
        public int TotalYearly { get; set; }
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
        public int ExcessiveParet { get; set; }

        [DataMember]
        public AllowableDeviation AllowableDeviation { get; set; }
        [DataMember]
        public Extraordinaries Extraordinaries { get; set; }

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
        public double AllowableFather { get; set; }
        [DataMember]
        public double AllowableMother { get; set; }
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
        public double ProRataFather { get; set; }
        [DataMember]
        public double ProRataMother { get; set; }
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
    [DataContract]
    public class Extraordinaries
    {
        [DataMember]
        public List<Extraordinary> Tuition { get; set; }
        [DataMember]
        public Extraordinary TuitionTotal { get; set; }
        [DataMember]
        public Extraordinary TuitionTotalMonthly { get; set; }
        [DataMember]
        public List<Extraordinary> Education { get; set; }
        [DataMember]
        public Extraordinary EducationTotal { get; set; }
        [DataMember]
        public Extraordinary EducationTotalMonthly { get; set; }
        [DataMember]
        public Extraordinary AllEducationTotal { get; set; }
        [DataMember]
        public Extraordinary AllEducationTotalMonthly { get; set; }
        [DataMember]
        public List<Extraordinary> YearlyEducation { get; set; }
        [DataMember]
        public Extraordinary YearlyEducationTotal { get; set; }
        [DataMember]
        public List<Extraordinary> MonthlyEducation { get; set; }
        [DataMember]
        public Extraordinary MonthlyEducationTotal { get; set; }
        [DataMember]
        public List<Extraordinary> Medical { get; set; }
        [DataMember]
        public Extraordinary MedicalTotal { get; set; }
        [DataMember]
        public Extraordinary MedicalTotalMonthly { get; set; }
        [DataMember]
        public List<Extraordinary> YearlyMedical { get; set; }
        [DataMember]
        public Extraordinary YearlyMedicalTotal { get; set; }
        [DataMember]
        public List<Extraordinary> MonthlyMedical { get; set; }
        [DataMember]
        public Extraordinary MonthlyMedicalTotal { get; set; }
        [DataMember]
        public List<Extraordinary> Rearing { get; set; }
        [DataMember]
        public Extraordinary RearingTotal { get; set; }
        [DataMember]
        public Extraordinary RearingTotalMonthly { get; set; }
        [DataMember]
        public List<Extraordinary> YearlyRearing { get; set; }
        [DataMember]
        public Extraordinary YearlyRearingTotal { get; set; }
        [DataMember]
        public List<Extraordinary> MonthlyRearing { get; set; }
        [DataMember]
        public Extraordinary MonthlyRearingTotal { get; set; }
    }
    [DataContract]
    public class Extraordinary
    {
        [DataMember]
        public int Father { get; set; }
        [DataMember]
        public int Mother { get; set; }
        [DataMember]
        public int NonParent { get; set; }
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public string Name { get; set; }
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

    #region CSA
    public class CsaDtoResp
    {
        public List<Child> Children { get; set; }
        public GrossIncome GrossIncome { get; set; }
        public ChildSupportAmount ChildSupportAmount { get; set; }
        public ChildSupport ChildSupport { get; set; }
        public Deviation Deviation { get; set; }
        public Health HealthInsurance { get; set; }
        public SocialSecurity SocialSecurity { get; set; }
        public Court Court { get; set; }
        public ParentNames ParentNames { get; set; }
        public string InsuranceProvider { get; set; }
        public string County { get; set; }
        public bool ChildSupportDifferent { get; set; }
    }

    public class Deviation
    {
        public bool HasDeviation { get; set; }
        public double? Amount { get; set; }
    }
    public class ChildSupportAmount
    {
        public string MonthlyAmountWritten { get; set; }
        public double MonthlyAmount { get; set; }
    }

    public class GrossIncome    
    {
        public double FatherAmount { get; set; }
        public double MotherAmount { get; set; }
    }

    #endregion

    #region PDF
    public class PdfDtoResp
    {
    }
    #endregion

    [Authenticate]
    public class OutputsService : ServiceBase
    {
        #region IOC Services
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
        public IPrivacyService PrivacyService { get; set; }
        public IInformationService InformationService { get; set; }
        public IDecisionsService DecisionsService { get; set; }
        public IExtraDecisionsService ExtraDecisionsService { get; set; }
        public IHolidayService HolidayService { get; set; }
        public IExtraHolidayService ExtraHolidayService { get; set; }
        public IResponsibilityService ResponsibilityService { get; set; }
        public ICommunicationService CommunicationService { get; set; }
        public IScheduleService ScheduleService { get; set; }
        public IHouseService HouseService { get; set; }
        public IPropertyService PropertyService { get; set; }
        public IDebtService DebtService { get; set; }
        public IAssetService AssetService { get; set; }
        public IHealthInsuranceService HealthInsuranceService { get; set; }
        public ISpousalService SpousalService { get; set; }
        public ITaxService TaxService { get; set; }
        public IChildSupportService ChildSupportService { get; set; }
        public IVehicleFormService VehicleFormService { get; set; }
        public IVehicleService VehicleService { get; set; }
        


        #endregion

        public object Get(ParentingPlanDto request)
        {
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            var court = CourtService.GetByUserId(userId) as Court;
            var participants = ParticipantService.GetByUserId(userId) as Participant;
            var children = ChildService.GetByUserId(userId);
            var privacy = PrivacyService.GetByUserId(userId);
            var information = InformationService.GetByUserId(userId);
            var responsibility = ResponsibilityService.GetByUserId(userId);
            var communication = CommunicationService.GetByUserId(userId) as Communication;
            var schedule = ScheduleService.GetByUserId(userId);
            
            var disagreement = "";
            var decisions = DecisionsService.GetChildrenListByUserId(userId);
            foreach (var decision in decisions.Where(decision => !string.IsNullOrEmpty(decision.BothResolve)))
            {
                disagreement = decision.BothResolve;
            }
            var decisionList = children.Select(child => new ChildDecisions
                {
                    Child = child, 
                    Decisions = decisions.FirstOrDefault(x=>x.ChildId == child.Id), 
                    ExtraDecisions = ExtraDecisionsService.GetByChildId(child.Id)
                }).ToList();

            var holidayList = children.Select(child => new ChildHoliday
            {
                Child = child,
                Holiday = HolidayService.GetByChildId(child.Id),
                ExtraHolidays = ExtraHolidayService.GetByChildId(child.Id)
            }).ToList();

            var formsViewModel = new FormsCompleted();

            //Setup output form            
            var outputViewModel = new PpOutputFormHelper
            {
                CustodyInformation = ParticipantService.GetCustodyInformation(participants)
            };

            //Communication
            var communicationTypes = new List<string>();
            if (communication.Telephone)
            {
                communicationTypes.Add("telephone");
            }
            if (communication.Email)
            {
                communicationTypes.Add("email");
            }
            if (communication.Other)
            {
                communicationTypes.Add(communication.OtherMethod);
            }
            outputViewModel.CommunicationTypePhrase = string.Join(", ", communicationTypes);
            
            var childViewModel = new ParentingPlanDtoResp
            {
                CourtViewModel = court,
                ParticipantViewModel = participants,
                Children = children,
                PrivacyViewModel = privacy as Privacy,
                InformationViewModel = information as Information,
                ChildDecisions = decisionList,
                Disagreement = disagreement,
                ResponsibilityViewModel = responsibility as Responsibility,
                CommunicationViewModel = communication,
                ScheduleViewModel = schedule as Schedule,
                ChildHolidays = holidayList,
                PpOutputFormHelper = outputViewModel,
                FormsCompleted = formsViewModel,
                County = CountyService.Get(court.CountyId).CountyName.ToUpper()

            };
            return childViewModel;
        }

        public object Get(DomesticMediationDto request)
        {
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            var house = HouseService.GetByUserId(userId) as House;
            var property = PropertyService.GetByUserId(userId) as Property;
            var debt = DebtService.GetByUserId(userId) as Debt;
            var assets = AssetService.GetByUserId(userId) as Assets;
            var health = HealthInsuranceService.GetByUserId(userId) as HealthInsurance;
            var spousal = SpousalService.GetByUserId(userId) as SpousalSupport;
            var taxes = TaxService.GetByUserId(userId) as Tax;
            var vehicles = VehicleService.GetByUserId(userId).ToList();
            var vehicleForm = VehicleFormService.GetByUserId(userId) as VehicleForm;
            var participants = ParticipantService.GetByUserId(userId) as Participant;
            var court = CourtService.GetByUserId(userId) as Court;
            return new DomesticMediationDtoResp
                {
                HouseViewModel = house,
                PropertyViewModel = property,
                VehicleForm = vehicleForm,
                Vehicles = vehicles,
                DebtViewModel = debt,
                AssetViewModel = assets,
                HealthInsuranceViewModel = health,
                SpousalViewModel = spousal,
                TaxViewModel = taxes,
                ParticipantsViewModel = participants,
                CourtViewModel = court,
            };

        }

        public object Get(FormCompleteDto request)
        {
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            Func<List<IncompleteForm>> action;
            var actions = new Dictionary<string, Func<List<IncompleteForm>>>
                {
                    {Resources.ParentingPlan, () => OutputService.GetParentingIncompleteForms(userId)},
                    {Resources.DomesticMediation, () => OutputService.GetDomesticIncompleteForms(userId)},
                    {Resources.FinancialForm, () => OutputService.GetFinancialIncompleteForms(userId)},
                    {Resources.StarterFormName, () => OutputService.GetStarterIncompleteForms(userId)},
                };
            if (actions.TryGetValue(request.FormName, out action))
            {
                return new FormCompleteDtoResp
                    {
                        IncompleteForms = action()
                    };
            }
            return new FormCompleteDtoResp();
        }

        public object Get(ScheduleADto request)
        {
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            //Setup output form            
            var parentNames = GetParentNames(userId);
            var schedule = GetScheduleA(userId);
            schedule.Father = parentNames.Father;
            schedule.Mother = parentNames.Mother;
            return schedule;
        }

        public object Get(ScheduleBDto request)
        {
            //Setup output form            
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(userId);
            var schedule = OutputService.GetScheduleB(userId, parentNames.Father);
            var scheduleOther = OutputService.GetScheduleB(userId, parentNames.Mother, true);

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
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(userId);
            var scheduleD = GetScheduleD(userId);
            scheduleD.ParentNames = parentNames;
            //scheduleD.Mother = parentNames.Mother;
            return scheduleD;
        }

        public object Get(ScheduleEDto request)
        {
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            return GetScheduleE(userId);
        }


        public object Get(CswDto request)
        {
            //Setup output form            
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            var parentNames = GetParentNames(userId);
            var court = CourtService.GetByUserId(userId).TranslateTo<Court>();
            var county = CountyService.Get(court.CountyId).CountyName;
            var cswDto = GetAllCsw(userId);
            cswDto.Father = parentNames.Father;
            cswDto.Mother = parentNames.Mother;
            cswDto.County = county;
            return cswDto;
        }
        
        public object Get(CsaDto request)
        {
            //Setup output form            
            var userId = request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId);
            var csaDto = GetCsa(userId);
            return csaDto;
        }

        [AddHeader(ContentType = "application/pdf")]
        [AddHeader(ContentDisposition = "attachment; filename=form.pdf")]     
        public Stream Post(PdfDto request)
        {
            var globalConfig = new GlobalConfig();
            globalConfig.SetPaperSize(PaperKind.Letter);
            var margins = new Margins(100, 100, 100, 100);
            globalConfig.SetMargins(margins);
            var synchronizedPechkin = new SynchronizedPechkin(globalConfig);

            var contentPath = "~/Content/".MapAbsolutePath();
            //TODO fix this
            contentPath = contentPath.Replace(@"\bin","");
            //Server.MapPath("~/Content/");
            var css = File.ReadAllText(Path.Combine(contentPath, "pdf.css"));
            var fullHtml = string.Format(@"<!DOCTYPE html> <html> <head><style type=""text/css"">{0}</style></head><body><div id=""main-content"">{1}</div></body></html>", css, request.Html);
            var config = new ObjectConfig();
            config.SetAllowLocalContent(true);
            config.SetPrintBackground(true);

            var userId = Convert.ToInt32(UserSession.CustomId);
            var participants = ParticipantService.GetByUserId(userId) as Participant;

            //if (Request.Url != null)
            //{
            //    var headerUrl = Request.Url.AbsoluteUri.Replace(Request.Url.LocalPath, "") + "/Headers/Header/" + userId;
            //    config.Header.SetHtmlContent(headerUrl);
            //}
            //config.Header.SetFont(new Font("Times New Roman", 9F, FontStyle.Underline));
            //config.Header.SetContentSpacing(40.0);
            //config.Header.SetLeftText("        " + participants.PlaintiffsName + "  v.  " + participants.DefendantsName + "        \r\n");
            //config.Header.SetRightText("CAF #" + court.CaseNumber);
            //config.Header.SetLineSeparator(true);            
            config.Footer.SetFont(new Font("Times New Roman", 8F, FontStyle.Regular));
            config.Footer.SetTexts(participants.PlaintiffsName + " Initials_______", @"[page] of [topage]", participants.DefendantsName + " Initials________");
            var pdfBuf = synchronizedPechkin.Convert(config, fullHtml);
            base.Response.AddHeader("Content-Length", pdfBuf.Length.ToString());
            return new MemoryStream(pdfBuf);
        }

        #region Private Helper Functions
        private CsaDtoResp GetCsa(long userId)
        {
            var court = CourtService.GetByUserId(userId).TranslateTo<Court>();
            var county = CountyService.Get(court.CountyId).CountyName;
            var parentNames = GetParentNames(userId);
            var csw = GetAllCsw(userId);
            var grossIncome = new GrossIncome
                {
                    MotherAmount = csw.MotherCsw.GrossIncome,
                    FatherAmount = csw.FatherCsw.GrossIncome
                };
            var childSupportAmount = new ChildSupportAmount
                {
                    MonthlyAmount = parentNames.NonCustodyIsFather ? csw.FatherCsw.FinalAmount : csw.MotherCsw.FinalAmount
                };
            childSupportAmount.MonthlyAmountWritten = Numbers.IntegerToWritten(Convert.ToInt32(childSupportAmount.MonthlyAmount));
            var deviations = DeviationsService.GetByUserId(userId) as Deviations;
            var deviation = new Deviation
                {
                    HasDeviation = deviations.Deviation == 1,
                    Amount = csw.FatherCsw.DeviationsAmount
                };
            var health = HealthService.GetByUserId(userId) as Health;
            var insuranceProvider = health.ProvideHealth == (int) DecisionMaker.Father
                                        ? parentNames.Father
                                        : parentNames.Mother;
            var csaDto = new CsaDtoResp
            {
                Children = ChildService.GetByUserId(userId),
                GrossIncome = grossIncome,
                ChildSupportAmount = childSupportAmount,
                ChildSupport = ChildSupportService.GetByUserId(userId) as ChildSupport,
                ParentNames = parentNames,
                Deviation = deviation,
                HealthInsurance = health,
                SocialSecurity = SocialSecurityService.GetByUserId(userId),
                InsuranceProvider = insuranceProvider,
                County = county,
                Court = court
            };
            return csaDto;
        }


        private static AllowableDeviation CalculateAllowableDeviation(Deviations deviations, LowIncomeDeviation lowIncome, HighIncomeDeviation highIncomeFather, HighIncomeDeviation highIncomeMother, double highIncomeAdjusted, ExtraExpenses totalExpenses)
        {
            return new AllowableDeviation
            {
                PresumptiveAmount = deviations.Unjust,
                BestInterest = deviations.BestInterest,
                ImpairAbility = deviations.Impair,
                //AllowableFather = highIncomeAdjusted + highIncomeFather.TotalDeviations - lowIncome.ActualAmount + totalExpenses.DeviationFather -
                //    totalExpenses.ExtraSpent,
                AllowableFather =  highIncomeFather.TotalDeviations - lowIncome.ActualAmount + totalExpenses.DeviationFather -
                    totalExpenses.ExtraSpent,
                AllowableMother =  highIncomeMother.TotalDeviations - lowIncome.ActualAmount + totalExpenses.DeviationMother -
                    totalExpenses.ExtraSpent,
            };
        }

        private static ExtraExpenses CalculateTotalExpenses(IEnumerable<ExtraExpense> extraExpenses, ScheduleB scheduleBFather, ScheduleB scheduleBMother, Extraordinaries extraordinaries)
        {
            var totalExpenses = new ExtraExpenses();
            var monthlyExpenses = extraExpenses.Select(extraExpense => extraExpense.ToMonthly()).ToList();
            totalExpenses.ExtraSpent = monthlyExpenses.Sum(x => x.ExtraSpent);

            totalExpenses.EducationFather = extraordinaries.AllEducationTotalMonthly.Father;
            totalExpenses.EducationMother = extraordinaries.AllEducationTotalMonthly.Mother;
            totalExpenses.EducationNonParent = extraordinaries.AllEducationTotalMonthly.NonParent;
            totalExpenses.EducationTotal = extraordinaries.AllEducationTotalMonthly.Total;
            totalExpenses.MedicalFather = extraordinaries.MedicalTotalMonthly.Father;
            totalExpenses.MedicalMother = extraordinaries.MedicalTotalMonthly.Mother;
            totalExpenses.MedicalNonParent = extraordinaries.MedicalTotalMonthly.NonParent;
            totalExpenses.MedicalTotal = extraordinaries.MedicalTotalMonthly.Total;
            totalExpenses.SpecialFather = extraordinaries.RearingTotalMonthly.Father;
            totalExpenses.SpecialMother = extraordinaries.RearingTotalMonthly.Mother;
            totalExpenses.SpecialNonParent = extraordinaries.RearingTotalMonthly.NonParent;
            totalExpenses.SpecialTotal = extraordinaries.RearingTotalMonthly.Total;
            totalExpenses.TotalFather = extraordinaries.AllEducationTotalMonthly.Father + extraordinaries.MedicalTotalMonthly.Father + extraordinaries.RearingTotalMonthly.Father;
            totalExpenses.TotalMother = extraordinaries.AllEducationTotalMonthly.Mother + extraordinaries.MedicalTotalMonthly.Mother + extraordinaries.RearingTotalMonthly.Mother;
            totalExpenses.TotalNonParent = extraordinaries.AllEducationTotalMonthly.NonParent + extraordinaries.MedicalTotalMonthly.NonParent + extraordinaries.RearingTotalMonthly.NonParent;

            var totalIncomes = GetTotalIncomes(scheduleBFather, scheduleBMother);
            totalExpenses.ProRataFather = totalIncomes.ProRataFatherPercentage;
            totalExpenses.ProRataMother = totalIncomes.ProRataMotherPercentage;
            totalExpenses.ProRataTotal = 100;
            totalExpenses.TotalTotal = totalExpenses.TotalFather + totalExpenses.TotalMother + totalExpenses.TotalNonParent;
            totalExpenses.PercentageFather = (int)Math.Round((double)totalExpenses.ProRataFather/100 * totalExpenses.TotalFather);
            totalExpenses.PercentageMother = (int)Math.Round((double)totalExpenses.ProRataMother / 100 * totalExpenses.TotalMother);
            totalExpenses.DeviationFather = totalExpenses.PercentageFather - totalExpenses.TotalFather;
            totalExpenses.DeviationMother = totalExpenses.PercentageMother - totalExpenses.TotalMother;
            return totalExpenses;
        }

        private static HighIncomeDeviation CalculateHighIncomeMotherDeviation(Deviations deviations)
        {
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
            return highIncomeMother;
        }

        private static HighIncomeDeviation CalculateHighIncomeFatherDeviation(Deviations deviations)
        {
            var highIncomeFather = new HighIncomeDeviation
            {
                Deviation = deviations.HighDeviation ?? 0,
                Alimony = deviations.AlimonyPaidFather ?? 0,
                LifeInsurance = deviations.InsuranceFather ?? 0,
                Mortgage = deviations.MortgageFather ?? 0,
                ChildTaxCredit = deviations.TaxCreditFather ?? 0,
                OtherInsurance = deviations.HealthFather ?? 0,
                NonSpecific = deviations.NonSpecificFather ?? 0,
                PermanancyPlan = deviations.PermanencyFather ?? 0,
                VisitationExpense = deviations.VisitationFather ?? 0,
            };
            highIncomeFather.TotalDeviations = highIncomeFather.Deviation + highIncomeFather.Alimony +
                                               highIncomeFather.LifeInsurance + highIncomeFather.Mortgage +
                                               highIncomeFather.ChildTaxCredit + highIncomeFather.OtherInsurance +
                                               highIncomeFather.NonSpecific + highIncomeFather.PermanancyPlan +
                                               highIncomeFather.VisitationExpense;
            return highIncomeFather;
        }

        private static Extraordinaries CalculateExtraordinaries(IEnumerable<ExtraExpense> extraExpenses, List<Child> children)
        {
            var tuition = new List<Extraordinary>();
            var tuitionTotal = new Extraordinary();
            var tuitionTotalMonthly = new Extraordinary();
            var education = new List<Extraordinary>();
            var educationTotal = new Extraordinary();
            var educationTotalMonthly = new Extraordinary();
            var allEducationTotal = new Extraordinary();
            var allEducationTotalMonthly = new Extraordinary();
            var medical = new List<Extraordinary>();
            var medicalTotal = new Extraordinary();
            var medicalTotalMonthly = new Extraordinary();
            var rearing = new List<Extraordinary>();
            var rearingTotal = new Extraordinary();
            var rearingTotalMonthly = new Extraordinary();
            var yearlyEducation = new List<Extraordinary>();
            var yearlyEducationCombinedTotal = new Extraordinary();
            var yearlyMedical = new List<Extraordinary>();
            var yearlyMedicalCombinedTotal = new Extraordinary();
            var yearlyRearing = new List<Extraordinary>();
            var yearlyRearingCombinedTotal = new Extraordinary();
            var monthlyEducation = new List<Extraordinary>();
            var monthlyEducationCombinedTotal = new Extraordinary();
            var monthlyMedical = new List<Extraordinary>();
            var monthlyMedicalCombinedTotal = new Extraordinary();
            var monthlyRearing = new List<Extraordinary>();
            var monthlyRearingCombinedTotal = new Extraordinary();

            var enumerable   = extraExpenses as IList<ExtraExpense> ?? extraExpenses.ToList();
            foreach (var extraExpense in enumerable)
            {
                var child = children.First(x => x.Id == extraExpense.ChildId);
                tuition.Add(new Extraordinary
                {
                    Father = extraExpense.TutitionFather,
                    Mother = extraExpense.TutitionMother,
                    NonParent = extraExpense.TutitionNonParent,
                    Name = child.Name,
                });
                tuitionTotal.Father += extraExpense.TutitionFather;
                tuitionTotal.Mother += extraExpense.TutitionMother;
                tuitionTotal.NonParent += extraExpense.TutitionNonParent;

                education.Add(new Extraordinary
                {
                    Father = extraExpense.EducationFather,
                    Mother = extraExpense.EducationMother,
                    NonParent = extraExpense.EducationNonParent,
                    Name = child.Name,
                });
                educationTotal.Father += extraExpense.EducationFather;
                educationTotal.Mother += extraExpense.EducationMother;
                educationTotal.NonParent += extraExpense.EducationNonParent;
                medical.Add(new Extraordinary
                {
                    Father = extraExpense.MedicalFather,
                    Mother = extraExpense.MedicalMother,
                    NonParent = extraExpense.MedicalNonParent,
                    Name = child.Name,
                });
                medicalTotal.Father += extraExpense.MedicalFather;
                medicalTotal.Mother += extraExpense.MedicalMother;
                medicalTotal.NonParent += extraExpense.MedicalNonParent;
                rearing.Add(new Extraordinary
                {
                    Father = extraExpense.SpecialFather,
                    Mother = extraExpense.SpecialMother,
                    NonParent = extraExpense.SpecialNonParent,
                    Name = child.Name,
                });
                rearingTotal.Father += extraExpense.SpecialFather;
                rearingTotal.Mother += extraExpense.SpecialMother;
                rearingTotal.NonParent += extraExpense.SpecialNonParent;
                var yearlyEducationTotal = extraExpense.EducationFather + extraExpense.EducationMother +
                                           extraExpense.EducationNonParent + extraExpense.TutitionFather +
                                           extraExpense.TutitionMother + extraExpense.TutitionNonParent;
                yearlyEducation.Add(new Extraordinary
                {
                    Total = yearlyEducationTotal,
                    Name = child.Name,
                });
                yearlyEducationCombinedTotal.Total += yearlyEducationTotal;
                var yearlyMedicalTotal = extraExpense.MedicalFather + extraExpense.MedicalMother +
                                         extraExpense.MedicalNonParent;
                yearlyMedical.Add(new Extraordinary
                {
                    Total = yearlyMedicalTotal,
                    Name = child.Name,
                });
                yearlyMedicalCombinedTotal.Total += yearlyMedicalTotal;
                var yearlyRearingTotal = extraExpense.SpecialFather + extraExpense.SpecialMother +
                                         extraExpense.SpecialNonParent;
                yearlyRearing.Add(new Extraordinary
                {
                    Father = yearlyRearingTotal,
                    Name = child.Name,
                });
                yearlyRearingCombinedTotal.Total += yearlyRearingTotal;
            }
            var monthlyExpenses = enumerable.Select(extraExpense => extraExpense.ToMonthly()).ToList();
            foreach (var extraExpense in monthlyExpenses)
            {
                var child = children.First(x => x.Id == extraExpense.ChildId);
                var monthlyEducationTotal = extraExpense.EducationFather + extraExpense.EducationMother +
                                             extraExpense.EducationNonParent + extraExpense.TutitionFather +
                                             extraExpense.TutitionMother + extraExpense.TutitionNonParent;
                monthlyEducation.Add(new Extraordinary
                {
                    Total = monthlyEducationTotal,
                    Name = child.Name,
                });
                monthlyEducationCombinedTotal.Total += monthlyEducationTotal;
                var monthlyMedicalTotal = extraExpense.MedicalFather + extraExpense.MedicalMother +
                                           extraExpense.MedicalNonParent;
                monthlyMedical.Add(new Extraordinary
                {
                    Total = monthlyMedicalTotal,
                    Name = child.Name,
                });
                monthlyMedicalCombinedTotal.Total += monthlyMedicalTotal;
                var monthlyRearingTotal = extraExpense.SpecialFather + extraExpense.SpecialMother +
                                           extraExpense.SpecialNonParent;
                monthlyRearing.Add(new Extraordinary
                {
                    Father = monthlyRearingTotal,
                    Name = child.Name,
                });
                monthlyRearingCombinedTotal.Total += monthlyRearingTotal;
                tuitionTotalMonthly.Father += extraExpense.TutitionFather;
                tuitionTotalMonthly.Mother += extraExpense.TutitionMother;
                tuitionTotalMonthly.NonParent += extraExpense.TutitionNonParent;
                tuitionTotalMonthly.Total += extraExpense.TutitionFather + extraExpense.TutitionMother + extraExpense.TutitionNonParent;
                educationTotalMonthly.Father += extraExpense.EducationFather;
                educationTotalMonthly.Mother += extraExpense.EducationMother;
                educationTotalMonthly.NonParent += extraExpense.EducationNonParent;
                educationTotalMonthly.Total += extraExpense.EducationFather + extraExpense.EducationMother + extraExpense.EducationNonParent;
                medicalTotalMonthly.Father += extraExpense.MedicalFather;
                medicalTotalMonthly.Mother += extraExpense.MedicalMother;
                medicalTotalMonthly.NonParent += extraExpense.MedicalNonParent;
                medicalTotalMonthly.Total += extraExpense.MedicalFather + extraExpense.MedicalMother + extraExpense.MedicalNonParent;
                rearingTotalMonthly.Father += extraExpense.SpecialFather;
                rearingTotalMonthly.Mother += extraExpense.SpecialMother;
                rearingTotalMonthly.NonParent += extraExpense.SpecialNonParent;
                rearingTotalMonthly.Total += extraExpense.SpecialFather + extraExpense.SpecialMother + extraExpense.SpecialNonParent;
            }
 
            allEducationTotal.Father = tuitionTotal.Father + educationTotal.Father;
            allEducationTotal.Mother = tuitionTotal.Mother + educationTotal.Mother;
            allEducationTotal.NonParent = tuitionTotal.NonParent + educationTotal.NonParent;
            allEducationTotal.Total = tuitionTotal.Total + educationTotal.Total;

            allEducationTotalMonthly.Father = tuitionTotalMonthly.Father + educationTotalMonthly.Father;
            allEducationTotalMonthly.Mother = tuitionTotalMonthly.Mother + educationTotalMonthly.Mother;
            allEducationTotalMonthly.NonParent = tuitionTotalMonthly.NonParent + educationTotalMonthly.NonParent;
            
            var extraordinaries = new Extraordinaries
            {
                Education = education,
                EducationTotal = educationTotal,
                EducationTotalMonthly = educationTotalMonthly,
                AllEducationTotal = allEducationTotal,
                AllEducationTotalMonthly = allEducationTotalMonthly,
                Tuition = tuition,
                TuitionTotal = tuitionTotal,
                TuitionTotalMonthly = tuitionTotalMonthly,
                YearlyEducation = yearlyEducation,
                YearlyEducationTotal = yearlyEducationCombinedTotal,
                MonthlyEducation = monthlyEducation,
                MonthlyEducationTotal = monthlyEducationCombinedTotal,
                Medical = medical,
                MedicalTotal = medicalTotal,
                MedicalTotalMonthly = medicalTotalMonthly,
                YearlyMedical = yearlyMedical,
                YearlyMedicalTotal = yearlyMedicalCombinedTotal,
                MonthlyMedical = monthlyMedical,
                MonthlyMedicalTotal = monthlyMedicalCombinedTotal,
                Rearing = rearing,
                RearingTotal = rearingTotal,
                RearingTotalMonthly = rearingTotalMonthly,
                YearlyRearing = yearlyRearing,
                YearlyRearingTotal = yearlyRearingCombinedTotal,
                MonthlyRearing = monthlyRearing,
                MonthlyRearingTotal = monthlyRearingCombinedTotal
            };
            return extraordinaries;
        }

        private ScheduleADtoResp GetScheduleA(long userId)
        {
            var income = IncomeService.GetByUserId(userId).ToIncomeDto().ToMonthly();
            var incomeOther = IncomeService.GetByUserId(userId, isOtherParent: true).ToIncomeDto().ToMonthly();
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
            return new ScheduleADtoResp
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
            var health = HealthService.GetByUserId(userId) as Health;
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
                var child = ChildService.Get(childCare.ChildId);
                childCareWithTotal.TotalFather = childCareWithTotal.BreaksFather + childCareWithTotal.OtherFather +
                                                   childCareWithTotal.SchoolFather + childCareWithTotal.SummerFather;
                childCareWithTotal.TotalMother = childCareWithTotal.BreaksMother + childCareWithTotal.OtherMother +
                                                   childCareWithTotal.SchoolMother + childCareWithTotal.SummerMother;
                childCareWithTotal.TotalNonParent = childCareWithTotal.BreaksNonParent + childCareWithTotal.OtherNonParent +
                                                      childCareWithTotal.SchoolNonParent + childCareWithTotal.SummerNonParent;
                childCareWithTotal.TotalFatherMonthly = (double)childCareWithTotal.TotalFather / 12;
                childCareWithTotal.TotalMotherMonthly = (double)childCareWithTotal.TotalMother / 12;
                childCareWithTotal.TotalNonParentMonthly = (double)childCareWithTotal.TotalNonParent / 12;
                childCareWithTotal.Name = child.Name;
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
            schedule.TotalMonthly = (double)schedule.TotalYearly / 12;
            otherSchedule.TotalMonthly = (double)otherSchedule.TotalYearly / 12;
            nonParentSchedule.TotalMonthly = (double)nonParentSchedule.TotalYearly / 12;

            schedule.WorkRelated = schedule.TotalMonthly;
            otherSchedule.WorkRelated = otherSchedule.TotalMonthly;
            nonParentSchedule.WorkRelated = nonParentSchedule.TotalMonthly;
            schedule.AdditionalExpenses = schedule.WorkRelated + schedule.HealthInsurance;
            otherSchedule.AdditionalExpenses = otherSchedule.WorkRelated + otherSchedule.HealthInsurance;
            nonParentSchedule.AdditionalExpenses = nonParentSchedule.WorkRelated + nonParentSchedule.HealthInsurance;

            var scheduleBFather = OutputService.GetScheduleB(userId, "namehere");
            var scheduleBMother = OutputService.GetScheduleB(userId, "name", true);
            var totalIncomes = GetTotalIncomes(scheduleBFather, scheduleBMother);
            schedule.ProRataParents = totalIncomes.ProRataFatherPercentage;
            otherSchedule.ProRataParents = totalIncomes.ProRataMotherPercentage;
            nonParentSchedule.ProRataParents = 0;
            var totalAdditional = schedule.AdditionalExpenses + otherSchedule.AdditionalExpenses +
                                  nonParentSchedule.AdditionalExpenses;
            schedule.ProRataAdditional = totalIncomes.ProRataFather * totalAdditional;
            otherSchedule.ProRataAdditional = totalIncomes.ProRataMother * totalAdditional;
            nonParentSchedule.ProRataAdditional = 0;

            totalScheduleD.WorkRelated = schedule.WorkRelated + otherSchedule.WorkRelated + nonParentSchedule.WorkRelated;
            totalScheduleD.HealthInsurance = schedule.HealthInsurance + otherSchedule.HealthInsurance + nonParentSchedule.HealthInsurance;
            totalScheduleD.AdditionalExpenses = schedule.AdditionalExpenses + otherSchedule.AdditionalExpenses + nonParentSchedule.AdditionalExpenses;
            totalScheduleD.ProRataParents = schedule.ProRataParents + otherSchedule.ProRataParents + nonParentSchedule.ProRataParents;
            totalScheduleD.ProRataAdditional = schedule.ProRataAdditional + otherSchedule.ProRataAdditional + nonParentSchedule.ProRataAdditional;
            return new ScheduleDDtoResp
                {
                FatherScheduleD = schedule,
                MotherScheduleD = otherSchedule,
                NonParentScheduleD = nonParentSchedule,
                TotalScheduleD = totalScheduleD,
                ChildCare = childCaresWithTotals,
            };
        }

        private ScheduleEDtoResp GetScheduleE(long userId)
        {
            var deviations = DeviationsService.GetByUserId(userId).TranslateTo<Deviations>();
            var children = ChildService.GetByUserId(userId);
            var participants = ParticipantService.GetByUserId(userId) as Participant;
            var extraExpenses = ExtraExpenseService.GetAllByUserId(userId);
            var custodyInformation = ParticipantService.GetCustodyInformation(participants);
            /*-----Get Information---*/
            var scheduleBFather = OutputService.GetScheduleB(userId, "");
            var scheduleBMother = OutputService.GetScheduleB(userId, "", isOtherParent: true);
            //If there are other children, use the PreexistingOrder amount, otherwise just use Adjusted Support
            var totalIncomes = GetTotalIncomes(scheduleBFather, scheduleBMother);
            var highIncomeAdjusted = totalIncomes.TotalIncomeTotal;    
            
            var scheduleD = GetScheduleD(userId);
            var presumptiveAmounts = GetPresumptiveAmounts(scheduleD.FatherScheduleD, scheduleD.MotherScheduleD, scheduleBFather,
                                                           scheduleBMother);
            var lowIncome = CalculateLowIncomeDeviation(deviations, custodyInformation, children, presumptiveAmounts);
            var highIncomeFather = CalculateHighIncomeFatherDeviation(deviations);
            var highIncomeMother = CalculateHighIncomeMotherDeviation(deviations);
            var parentNames = GetParentNames(userId);
            var extraordinaries = CalculateExtraordinaries(extraExpenses, children);
            var totalExpenses = CalculateTotalExpenses(extraExpenses, scheduleBFather, scheduleBMother, extraordinaries);
            
            AllowableDeviation allowableDeviation = CalculateAllowableDeviation(deviations, lowIncome, highIncomeFather, highIncomeMother, highIncomeAdjusted,
                                                       totalExpenses);

            return new ScheduleEDtoResp
            {
                LowIncomeDeviation = lowIncome,
                HighIncomeDeviationFather = highIncomeFather,
                HighIncomeDeviationMother = highIncomeMother,
                HighIncomeAdjusted = highIncomeAdjusted,
                TotalExpenses = totalExpenses,
                AllowableDeviation = allowableDeviation,
                Extraordinaries = extraordinaries,
                Father = parentNames.Father,
                Mother = parentNames.Mother
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
            var healthInsurance = HealthService.GetByUserId(userId) as Health;
            var totalIncomes = GetTotalIncomes(scheduleBFather, scheduleBMother);
            var cswFather = new Csw
            {
                GrossIncome = scheduleA.IncomeTotal,
                AdjustedIncome = totalIncomes.TotalIncomeFather,
                CombinedIncome = totalIncomes.ProRataFatherPercentage,
            };
            var cswMother = new Csw
            {
                GrossIncome = scheduleA.OtherIncomeTotal,
                AdjustedIncome = totalIncomes.TotalIncomeMother,
                CombinedIncome = totalIncomes.ProRataMotherPercentage,
            };
            var cswTotal = new Csw
                {
                GrossIncome = cswFather.GrossIncome + cswMother.GrossIncome,
                AdjustedIncome = totalIncomes.TotalIncomeTotal,
                CombinedIncome = 100
            };
            var children = ChildService.GetByUserId(userId);
            var scheduleE = GetScheduleE(userId);
            var totalOtherChildren = scheduleBFather.OtherChildren.Count + scheduleBMother.OtherChildren.Count;
            cswTotal.SupportObligation = (int)BcsoService.GetAmount(totalIncomes.TotalIncomeTotal, totalOtherChildren);
            cswFather = FinishCsw(cswFather, cswTotal.SupportObligation, scheduleD.FatherScheduleD, socialSecurityFather, healthInsurance);
            cswMother = FinishCsw(cswMother, cswTotal.SupportObligation, scheduleD.MotherScheduleD, socialSecurityMother, healthInsurance);
            cswFather.DeviationsAmount = scheduleE.AllowableDeviation.AllowableFather;
            cswMother.DeviationsAmount = scheduleE.AllowableDeviation.AllowableMother;
            return new CswDtoResp
                {
                FatherCsw = cswFather,
                MotherCsw = cswMother,
                TotalCsw = cswTotal,
                Children = children
            };
        }

        private LowIncomeDeviation CalculateLowIncomeDeviation(Deviations deviations, CustodyInformation custodyInformation, List<Child> children, PresumptiveAmounts presumptiveAmounts)
        {
            var lowIncome = new LowIncomeDeviation
            {
                DeviationAmount = deviations.LowDeviation ?? 0,
            };
            var csw9 = custodyInformation.NonCustodyIsFather ? presumptiveAmounts.Father : presumptiveAmounts.Mother;
            lowIncome.CompareAmount = csw9 - lowIncome.DeviationAmount;
            var minChildSupportAmount = 100 + children.Count * 50;
            lowIncome.CalculatedAmount = Math.Abs(csw9 - minChildSupportAmount);
            var smallerAmount = lowIncome.CalculatedAmount > lowIncome.CompareAmount
                                    ? lowIncome.CompareAmount
                                    : lowIncome.CalculatedAmount;

            lowIncome.ActualAmount = lowIncome.CompareAmount < minChildSupportAmount ? smallerAmount : lowIncome.DeviationAmount;
            lowIncome.Explaination = deviations.WhyLow;
            return lowIncome;
        }

        private class PresumptiveAmounts
        {
            public double Father { get; set; }
            public double Mother { get; set; }
        }
        
        private PresumptiveAmounts GetPresumptiveAmounts(ScheduleD scheduleDFather, ScheduleD scheduleDMother, ScheduleB scheduleBFather, ScheduleB scheduleBMother)
        {            
            var totalIncomes = GetTotalIncomes(scheduleBFather, scheduleBMother);           
            var totalOtherChildren = scheduleBFather.OtherChildren.Count + scheduleBMother.OtherChildren.Count;
            var supportObligation = BcsoService.GetAmount(totalIncomes.TotalIncomeTotal, totalOtherChildren);
            //calculate father
            var proRataObligationFather = totalIncomes.ProRataFather * supportObligation;
            var workRelatedExpensesFather = scheduleDFather.ProRataAdditional;
            var adjustedObligationFather = proRataObligationFather + workRelatedExpensesFather;
            var adjustedExpensesPaidFather = scheduleDFather.AdditionalExpenses;
            var presumptiveAmountFather = adjustedObligationFather - adjustedExpensesPaidFather;
            //calculate mother
            var proRataObligationMother = totalIncomes.ProRataMother * supportObligation;
            var workRelatedExpensesMother = scheduleDMother.ProRataAdditional;
            var adjustedObligationMother = proRataObligationMother + workRelatedExpensesMother;
            var adjustedExpensesPaidMother = scheduleDMother.AdditionalExpenses;
            var presumptiveAmountMother = adjustedObligationMother - adjustedExpensesPaidMother;
            return new PresumptiveAmounts
                {
                    Father = presumptiveAmountFather,
                    Mother = presumptiveAmountMother
                };
        }
        private static TotalIncomes GetTotalIncomes(ScheduleB scheduleBFather, ScheduleB scheduleBMother)
        {
            var fatherIncome = scheduleBFather.AdjustedSupport;
            var motherIncome = scheduleBMother.AdjustedSupport;
            if (scheduleBFather.OtherChildren.Count > 0)
                fatherIncome = scheduleBFather.PreexistingOrder;
            if (scheduleBMother.OtherChildren.Count > 0)
                motherIncome = scheduleBMother.PreexistingOrder;
            var totalIncome = fatherIncome + motherIncome;
            var totalIncomes = new TotalIncomes
                {
                    ProRataFatherPercentage = (float)fatherIncome / totalIncome * 100,
                    ProRataFather = fatherIncome/totalIncome,
                    ProRataMother = motherIncome/totalIncome,
                    TotalIncomeFather = fatherIncome,
                    TotalIncomeMother = motherIncome,
                    TotalIncomeTotal = totalIncome
                };
            totalIncomes.ProRataMotherPercentage = 100 - totalIncomes.ProRataFatherPercentage;
            return totalIncomes;
        }
        private static Csw FinishCsw(Csw csw, double supportObligation, ScheduleD scheduleD, SocialSecurity socialSecurity, Health healthInsurance, bool isFather = true)
        {
            csw.ProRataObligation = (int)Math.Round(csw.CombinedIncome/100 * supportObligation);
            csw.WorkRelatedExpenses = scheduleD.ProRataAdditional;
            csw.AdjustedObligation = csw.ProRataObligation + csw.WorkRelatedExpenses;
            csw.AdjustedExpensesPaid = scheduleD.AdditionalExpenses;
            csw.PresumptiveAmount = csw.AdjustedObligation - csw.AdjustedExpensesPaid;
            //csw.DeviationsAmount = 0; //TODO: comes from scheduleE
            csw.Subtotal = csw.PresumptiveAmount + csw.DeviationsAmount;
            csw.SocialSecurity = (int)(socialSecurity.Amount ?? 0.0);
            csw.FinalAmount = csw.SocialSecurity > csw.Subtotal
                                        ? csw.SocialSecurity
                                        : csw.Subtotal - csw.SocialSecurity;
            csw.UninsuredExpenses = (int)(isFather ? healthInsurance.FathersHealthPercentage ?? 0.0 : healthInsurance.MothersHealthPercentage ?? 0.0);
            return csw;
        }

        private ParentNames GetParentNames(long userId)
        {
            var participants = ParticipantService.GetByUserId(userId) as Participant;
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
                         : outputViewModel.CustodyInformation.NonCustodyParentName,
                CustodialParent = outputViewModel.CustodyInformation.CustodyParentName,
                NonCustodialParent = outputViewModel.CustodyInformation.NonCustodyParentName,
                NonCustodyIsFather = outputViewModel.CustodyInformation.NonCustodyIsFather,
                PlaintiffName = participants.PlaintiffsName,
                DefendantName = participants.DefendantsName
            };
        }
        #endregion
    }

    public class TotalIncomes
    {
        public double TotalIncomeMother { get; set; }
        public double TotalIncomeFather { get; set; }
        public double TotalIncomeTotal { get; set; }
        public double ProRataFatherPercentage { get; set; }
        public double ProRataMotherPercentage { get; set; }
        public double ProRataFather { get; set; }
        public double ProRataMother { get; set; }
    }

    public class ParentNames
    {
        public string Father { get; set; }
        public string Mother { get; set; }
        public string CustodialParent { get; set; }
        public string NonCustodialParent { get; set; }
        public bool NonCustodyIsFather { get; set; }
        public string PlaintiffName { get; set; }
        public string DefendantName { get; set; }
    }
}
