﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic;
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
    [Route("/Output/Financial/ScheduleA/{UserId}")]
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
        public string CustodialParentName { get; set; }
        [DataMember]
        public string NonCustodialParentName { get; set; }
    }
    [DataContract]
    public class ScheduleBDtoResp
    {
        [DataMember]
        public ScheduleB ScheduleB { get; set; }
        [DataMember]
        public ScheduleB OtherScheduleB { get; set; }
        [DataMember]
        public string CustodialParentName { get; set; }
        [DataMember]
        public string NonCustodialParentName { get; set; }
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
        public List<OtherChild> OtherChildren { get; set; }
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

    [DataContract]
    public class ScheduleDDtoResp
    {
        [DataMember]
        public ScheduleD ScheduleD { get; set; }
        [DataMember]
        public ScheduleD NonCustodialScheduleD { get; set; }
        [DataMember]
        public ScheduleD NonParentScheduleD { get; set; }
        [DataMember]
        public ScheduleD TotalScheduleD { get; set; }
        [DataMember]
        public List<ChildCare> ChildCare { get; set; }
        [DataMember]
        public string CustodialParentName { get; set; }
        [DataMember]
        public string NonCustodialParentName { get; set; }
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
        public double ProRataExpenses { get; set; }
    }
    
    public class OutputsService : Service
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

        public object Get(ScheduleADto request)
        {
            var income = IncomeService.GetByUserId(request.UserId).TranslateTo<IncomeDto>();
            var incomeOther = IncomeService.GetByUserId(request.UserId, isOtherParent: true).TranslateTo<IncomeDto>();
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
            //Setup output form            
            var participants = ParticipantService.GetByUserId(request.UserId) as ParticipantViewModel;
            var outputViewModel = new PpOutputFormHelper
            {
                CustodyInformation = ParticipantService.GetCustodyInformation(participants)
            };
            var schedule = new ScheduleADtoResp
                {
                    Income = income,
                    OtherIncome = incomeOther,
                    CombinedIncome = incomeCombined,
                    IncomeTotal = income.CalculateTotalIncome(),
                    OtherIncomeTotal = incomeOther.CalculateTotalIncome(),
                    CombinedIncomeTotal = incomeCombined.CalculateTotalIncome(),
                    CustodialParentName = outputViewModel.CustodyInformation.CustodyParentName,
                    NonCustodialParentName = outputViewModel.CustodyInformation.NonCustodyParentName
                };
            return schedule;
        }

        public object Get(ScheduleBDto request)
        {
            var income = IncomeService.GetByUserId(request.UserId).TranslateTo<IncomeDto>();
            var preexistSupport = PreexistingSupportFormService.GetByUserId(request.UserId);
            var otherChildForm = OtherChildrenService.GetByUserId(request.UserId);
            var schedule = GetScheduleB(income, preexistSupport, otherChildForm);

            var incomeOther = IncomeService.GetByUserId(request.UserId, isOtherParent: true).TranslateTo<IncomeDto>();
            var preexistSupportOther = PreexistingSupportFormService.GetByUserId(request.UserId, isOtherParent:true);
            var otherChildFormOther = OtherChildrenService.GetByUserId(request.UserId, isOtherParent:true);
            var scheduleOther = GetScheduleB(incomeOther, preexistSupportOther, otherChildFormOther);
            //Setup output form            
            var participants = ParticipantService.GetByUserId(request.UserId) as ParticipantViewModel;
            var outputViewModel = new PpOutputFormHelper
            {
                CustodyInformation = ParticipantService.GetCustodyInformation(participants)
            };
            return new ScheduleBDtoResp
                {
                    ScheduleB = schedule,
                    OtherScheduleB = scheduleOther,
                    CustodialParentName = outputViewModel.CustodyInformation.CustodyParentName,
                    NonCustodialParentName = outputViewModel.CustodyInformation.NonCustodyParentName
                };
        }

        public object Get(ScheduleDDto request)
        {
            //Setup output form            
            var participants = ParticipantService.GetByUserId(request.UserId) as ParticipantViewModel;
            var health = HealthService.GetByUserId(request.UserId) as HealthViewModel;
            var childCares = ChildCareService.GetAllByUserId(request.UserId);
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


            var outputViewModel = new PpOutputFormHelper
            {
                CustodyInformation = ParticipantService.GetCustodyInformation(participants)
            };
            return new ScheduleDDtoResp
            {
                CustodialParentName = outputViewModel.CustodyInformation.CustodyParentName,
                NonCustodialParentName = outputViewModel.CustodyInformation.NonCustodyParentName
            };
        }

        private ScheduleB GetScheduleB(IncomeDto income, PreexistingSupportFormViewModel preexistingSupport, OtherChildrenViewModel otherChildren)
        {
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
                var preexistSupportChildren = PreexistingSupportChildService.GetChildrenBySupportId(preexistingSupport.Id).ToList();
                schedule.PreexistingSupportChild = preexistSupportChildren.ToList();
                schedule.PreexistingSupport = preexistSupportChildren.Select(x => x.PreexistingSupport).ToList();
                schedule.TotalSupport = schedule.PreexistingSupport.Sum(c => c.Monthly);
            }
            schedule.AdjustedSupport = schedule.Total5Minus1 - schedule.TotalSupport;
            if (otherChildren != null)
            {
                schedule.OtherChildren = OtherChildService.GetChildrenByOtherChildrenId(otherChildren.Id).ToList();
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