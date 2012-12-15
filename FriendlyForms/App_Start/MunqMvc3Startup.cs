using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext;
using DataLayerContext.Repositories;
using FriendlyForms.Authentication;
using Munq.LifetimeManagers;
using Munq.MVC3;

[assembly: WebActivator.PreApplicationStartMethod(
	typeof(FriendlyForms.App_Start.MunqMvc3Startup), "PreStart")]

namespace FriendlyForms.App_Start {
	public static class MunqMvc3Startup {
		public static void PreStart() {
			DependencyResolver.SetResolver(new MunqDependencyResolver());

            var container = MunqDependencyResolver.Container;
            //Munq.Configuration.ConfigurationLoader.FindAndRegisterDependencies(container); // Optional
            container.DefaultLifetimeManager = new RequestLifetime(); //Set default lifetime to Request

			// TODO: Register Dependencies in Global.asax Application_Start
			// var ioc = MunqDependencyResolver.Container;
			// Munq.Configuration.ConfigurationLoader.FindAndRegisterDependencies(ioc); // Optional
			// ioc.Register<IMyRepository, MyRepository>();
			// ...
            

            //Repositories
            container.Register<IUnitOfWork>(c => new SplitContext());
            container.Register<ICourtRepository>(c => new CourtRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IUserRepository>(c => new UserRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IParticipantRepository>(c => new ParticipantRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IChildRepository>(c => new ChildRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IPrivacyRepository>(c => new PrivacyRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IInformationRepository>(c => new InformationRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IDecisionRepository>(c => new DecisionRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IExtraDecisionRepository>(c => new ExtraDecisionRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IResponsibilityRepository>(c => new ResponsibilityRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ICommunicationRepository>(c => new CommunicationRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IScheduleRepository>(c => new ScheduleRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ICountyRepository>(c => new CountyRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IHouseRepository>(c => new HouseRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IRealEstateRepository>(c => new RealEstateRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IVehicleRepository>(c => new VehicleRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IDebtRepository>(c => new DebtRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IAssetRepository>(c => new AssetRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IHealthInsuranceRepository>(c => new HealthInsuranceRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ISpousalRepository>(c => new SpousalRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ITaxRepository>(c => new TaxRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IChildSupportRepository>(c => new ChildSupportRepository(c.Resolve<IUnitOfWork>()));
		    container.Register<IHolidayRepository>(c => new HolidayRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IExtraHolidayRepository>(c => new ExtraHolidayRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IIncomeRepository>(c => new IncomeRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ISocialSecurityRepository>(c => new SocialSecurityRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IPreexistingSupportChildRepository>(c => new PreexistingSupportChildRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IPreexistingSupportRepository>(c => new PreexistingSupportRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IOtherChildrenRepository>(c => new OtherChildrenRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ISpecialCircumstancesRepository>(c => new SpecialCircumstancesRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IOtherChildRepository>(c => new OtherChildRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IVehicleFormRepository>(c => new VehicleFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IChildFormRepository>(c => new ChildFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IAddendumRepository>(c => new AddendumRepository(c.Resolve<IUnitOfWork>()));

            //Services
            container.Register<ICourtService>(c => new CourtService(c.Resolve<ICourtRepository>() as CourtRepository));
            container.Register<IParticipantService>(c => new ParticipantService(c.Resolve<IParticipantRepository>() as ParticipantRepository));
            container.Register<IChildService>(c => new ChildService(c.Resolve<IChildRepository>() as ChildRepository));
            container.Register<IPrivacyService>(c => new PrivacyService(c.Resolve<IPrivacyRepository>() as PrivacyRepository));
            container.Register<IInformationService>(c => new InformationService(c.Resolve<IInformationRepository>() as InformationRepository));
            container.Register<IDecisionsService>(c => new DecisionsService(c.Resolve<IDecisionRepository>() as DecisionRepository));
            container.Register<IExtraDecisionsService>(c => new ExtraDecisionsService(c.Resolve<IExtraDecisionRepository>()));
            container.Register<IMailService>(c => new MailService());
            container.Register<IUserService>(c => new UserService(c.Resolve<IUserRepository>(),c.Resolve<IMailService>()));
            container.Register<IResponsibilityService>(c => new ResponsibilityService(c.Resolve<IResponsibilityRepository>() as ResponsibilityRepository));
            container.Register<ICommunicationService>(c => new CommunicationService(c.Resolve<ICommunicationRepository>() as CommunicationRepository));
            container.Register<IScheduleService>(c => new ScheduleService(c.Resolve<IScheduleRepository>() as ScheduleRepository));
            container.Register<ICountyService>(c => new CountyService(c.Resolve<ICountyRepository>()));
            container.Register<IHouseService>(c => new HouseService(c.Resolve<IHouseRepository>() as HouseRepository));
            container.Register<IPropertyService>(c => new PropertyService(c.Resolve<IRealEstateRepository>() as RealEstateRepository));
            container.Register<IVehicleService>(c => new VehicleService(c.Resolve<IVehicleRepository>() as VehicleRepository));
            container.Register<IDebtService>(c => new DebtService(c.Resolve<IDebtRepository>() as DebtRepository));
            container.Register<IAssetService>(c => new AssetService(c.Resolve<IAssetRepository>() as AssetRepository));
            container.Register<IHealthInsuranceService>(c => new HealthInsuranceService(c.Resolve<IHealthInsuranceRepository>() as HealthInsuranceRepository));
            container.Register<ISpousalService>(c => new SpousalService(c.Resolve<ISpousalRepository>() as SpousalRepository));
            container.Register<ITaxService>(c => new TaxService(c.Resolve<ITaxRepository>() as TaxRepository));
            container.Register<IChildSupportService>(c => new ChildSupportService(c.Resolve<IChildSupportRepository>() as ChildSupportRepository));
            container.Register<IHolidayService>(c => new HolidayService(c.Resolve<IHolidayRepository>() as HolidayRepository));
            container.Register<IExtraHolidayService>(c => new ExtraHolidayService(c.Resolve<IExtraHolidayRepository>() as ExtraHolidayRepository));
            container.Register<IIncomeService>(c => new IncomeService(c.Resolve<IIncomeRepository>() as IncomeRepository));
            container.Register<ISocialSecurityService>(c => new SocialSecurityService(c.Resolve<ISocialSecurityRepository>() as SocialSecurityRepository));
            container.Register<IPreexistingSupportChildService>(c => new PreexistingSupportChildService(c.Resolve<IPreexistingSupportChildRepository>() as PreexistingSupportChildRepository));
            container.Register<IPreexistingSupportService>(c => new PreexistingSupportService(c.Resolve<IPreexistingSupportRepository>() as PreexistingSupportRepository));
            container.Register<IOtherChildrenService>(c => new OtherChildrenService(c.Resolve<IOtherChildrenRepository>() as OtherChildrenRepository));
            container.Register<ISpecialCircumstancesService>(c => new SpecialCircumstancesService(c.Resolve<ISpecialCircumstancesRepository>() as SpecialCircumstancesRepository));
            container.Register<IOtherChildService>(c => new OtherChildService(c.Resolve<IOtherChildRepository>() as OtherChildRepository));
            container.Register<IVehicleFormService>(c => new VehicleFormService(c.Resolve<IVehicleFormRepository>() as VehicleFormRepository));
            container.Register<IChildFormService>(c => new ChildFormService(c.Resolve<IChildFormRepository>() as ChildFormRepository));
            container.Register<IAddendumService>(c => new AddendumService(c.Resolve<IAddendumRepository>() as AddendumRepository));

            //Authentication
            container.Register<IFormsAuthentication>(c => new DefaultFormsAuthentication());

		}
	}
}
 

