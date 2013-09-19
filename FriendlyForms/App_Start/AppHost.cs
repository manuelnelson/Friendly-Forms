using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext;
using DataLayerContext.OrmLiteRepositories;
using DataLayerContext.Repositories;
using FriendlyForms.Helpers;
using FriendlyForms.Models;
using Funq;
using ServiceStack.Configuration;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(FriendlyForms.App_Start.AppHost), "Start")]

//IMPORTANT: Add the line below to MvcApplication.RegisterRoutes(RouteCollection) in the Global.asax:
//routes.IgnoreRoute("api/{*pathInfo}"); 

/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace FriendlyForms.App_Start
{
	//A customizeable typed UserSession that can be extended with your own properties
	//To access ServiceStack's Session, Cache, etc from MVC Controllers inherit from ControllerBase<CustomUserSession>
    //public class CustomUserSession : AuthUserSession
    //{
    //    public string CustomProperty { get; set; }
    //}
    public class AppConfig
    {
        public AppConfig(IResourceManager appSettings)
        {
            AdminUserNames = appSettings.Get("AdminUserNames", new List<string>());
            Env = appSettings.Get("Env", Env.Local);
        }
        public List<string> AdminUserNames { get; set; }
        public Env Env { get; set; }
    }

    public enum Env
    {
        Local,
        Dev,
        Test,
        Prod,
    }

	public class AppHost : AppHostBase
	{
        public static AppConfig AppConfig;
        public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Split Solution Api", typeof(RestService.CourtRestService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			JsConfig.EmitCamelCaseNames = false;
            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            var appSettings = new AppSettings();
            AppConfig = new AppConfig(appSettings);
            container.Register(AppConfig);

			//Change the default ServiceStack configuration
			//SetConfig(new EndpointHostConfig {
			//    DebugMode = true, //Show StackTraces in responses in development
			//});

			//Enable Authentication
			ConfigureAuth(container);



			//Register all your dependencies
			//container.Register(new TodoRepository());
            LogManager.LogFactory = new ElmahLogFactory(new NullLogFactory());

            SetupOrmLiteRepositories(container);
            //SetupEFRepositories(container);
            SetupServices(container);
            //SetupEFServices(container);
            
            //Register In-Memory Cache provider. 
            //For Distributed Cache Providers Use: PooledRedisClientManager, BasicRedisClientManager or see: https://github.com/ServiceStack/ServiceStack/wiki/Caching
#if DEBUG
            container.Register<ICacheClient>(new MemoryCacheClient());
            //            container.Register<ICacheClient>(new AzureCacheClient());
#else
            container.Register<ICacheClient>(new AzureCacheClient());
            //container.Register<ICacheClient>(new MemoryCacheClient());
            //use azure cache client
#endif
            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>()));

		    CreateMissingTables(container, run:false);

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

        private void CreateMissingTables(Container container, bool run)
        {
            //We don't need to run this all the time
            if (!run) return;

            var vehicleRepo = (VehicleOrmLiteRepository)container.Resolve<IVehicleRepository>();
            vehicleRepo.CreateMissingTables();
            var deviationRepo = (DeviationsOrmLiteRepository)container.Resolve<IDeviationsRepository>();
            deviationRepo.CreateMissingTables();
        }

        private void SetupEFRepositories(Container container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SplitContext"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance)
                {
                    ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                });

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
            container.Register<IDeviationsRepository>(c => new DeviationsRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IOtherChildRepository>(c => new OtherChildRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IVehicleFormRepository>(c => new VehicleFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IChildFormRepository>(c => new ChildFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IAddendumRepository>(c => new AddendumRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IClientRepository>(c => new ClientRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IHealthRepository>(c => new HealthRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IChildCareRepository>(c => new ChildCareRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IChildCareFormRepository>(c => new ChildCareFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IPreexistingSupportFormRepository>(c => new PreexistingSupportFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IExtraExpenseFormRepository>(c => new ExtraExpenseFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IExtraExpenseRepository>(c => new ExtraExpenseRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IBcsoRepository>(c => new BcsoRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ILawFirmRepository>(c => new LawFirmRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IAttorneyPageRepository>(c => new AttorneyPageRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IAttorneyPageUserRepository>(c => new AttorneyPageUserRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IAttorneyClientRepository>(c => new AttorneyClientRepository(c.Resolve<IUnitOfWork>()));
        }

        private void SetupOrmLiteRepositories(Container container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SplitContext"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance)
                {
                    ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                });

            container.Register<ICourtRepository>(c => new CourtOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IUserRepository>(c => new UserOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IParticipantRepository>(c => new ParticipantOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IChildRepository>(c => new ChildOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IPrivacyRepository>(c => new PrivacyOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IInformationRepository>(c => new InformationOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IDecisionRepository>(c => new DecisionOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IExtraDecisionRepository>(c => new ExtraDecisionOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IResponsibilityRepository>(c => new ResponsibilityOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ICommunicationRepository>(c => new CommunicationOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IScheduleRepository>(c => new ScheduleOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ICountyRepository>(c => new CountyOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IHouseRepository>(c => new HouseOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IRealEstateRepository>(c => new RealEstateOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IVehicleRepository>(c => new VehicleOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IDebtRepository>(c => new DebtOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IAssetRepository>(c => new AssetOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IHealthInsuranceRepository>(c => new HealthInsuranceOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ISpousalRepository>(c => new SpousalOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ITaxRepository>(c => new TaxOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IChildSupportRepository>(c => new ChildSupportOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IHolidayRepository>(c => new HolidayOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IExtraHolidayRepository>(c => new ExtraHolidayOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IIncomeRepository>(c => new IncomeOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ISocialSecurityRepository>(c => new SocialSecurityOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IPreexistingSupportChildRepository>(c => new PreexistingSupportChildOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IPreexistingSupportRepository>(c => new PreexistingSupportOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IOtherChildrenRepository>(c => new OtherChildrenOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IDeviationsRepository>(c => new DeviationsOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IOtherChildRepository>(c => new OtherChildOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IVehicleFormRepository>(c => new VehicleFormOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IChildFormRepository>(c => new ChildFormOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IAddendumRepository>(c => new AddendumOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IClientRepository>(c => new ClientOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IHealthRepository>(c => new HealthOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IChildCareRepository>(c => new ChildCareOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IChildCareFormRepository>(c => new ChildCareFormOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IPreexistingSupportFormRepository>(c => new PreexistingSupportFormOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IExtraExpenseFormRepository>(c => new ExtraExpenseFormOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IExtraExpenseRepository>(c => new ExtraExpenseOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IBcsoRepository>(c => new BcsoOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ILawFirmRepository>(c => new LawFirmOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IAttorneyPageRepository>(c => new AttorneyPageOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IAttorneyPageUserRepository>(c => new AttorneyPageUserOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IAttorneyClientRepository>(c => new AttorneyClientOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
        }

        private void SetupServices(Container container)
        {
            container.Register<ICourtService>(c => new CourtService(c.Resolve<ICourtRepository>()));
            container.Register<IParticipantService>(c => new ParticipantService(c.Resolve<IParticipantRepository>()));
            container.Register<IChildService>(c => new ChildService(c.Resolve<IChildRepository>()));
            container.Register<IPrivacyService>(c => new PrivacyService(c.Resolve<IPrivacyRepository>()));
            container.Register<IInformationService>(c => new InformationService(c.Resolve<IInformationRepository>()));
            container.Register<IDecisionsService>(c => new DecisionsService(c.Resolve<IDecisionRepository>()));
            container.Register<IExtraDecisionsService>(c => new ExtraDecisionsService(c.Resolve<IExtraDecisionRepository>()));
            container.Register<IEmailService>(c => new EmailService());
            container.Register<IUserService>(c => new UserService(c.Resolve<IUserRepository>(), c.Resolve<IEmailService>()));
            container.Register<IResponsibilityService>(c => new ResponsibilityService(c.Resolve<IResponsibilityRepository>()));
            container.Register<ICommunicationService>(c => new CommunicationService(c.Resolve<ICommunicationRepository>()));
            container.Register<IScheduleService>(c => new ScheduleService(c.Resolve<IScheduleRepository>()));
            container.Register<ICountyService>(c => new CountyService(c.Resolve<ICountyRepository>()));
            container.Register<IHouseService>(c => new HouseService(c.Resolve<IHouseRepository>()));
            container.Register<IPropertyService>(c => new PropertyService(c.Resolve<IRealEstateRepository>()));
            container.Register<IVehicleService>(c => new VehicleService(c.Resolve<IVehicleRepository>()));
            container.Register<IDebtService>(c => new DebtService(c.Resolve<IDebtRepository>()));
            container.Register<IAssetService>(c => new AssetService(c.Resolve<IAssetRepository>()));
            container.Register<IHealthInsuranceService>(c => new HealthInsuranceService(c.Resolve<IHealthInsuranceRepository>()));
            container.Register<ISpousalService>(c => new SpousalService(c.Resolve<ISpousalRepository>()));
            container.Register<ITaxService>(c => new TaxService(c.Resolve<ITaxRepository>()));
            container.Register<IChildSupportService>(c => new ChildSupportService(c.Resolve<IChildSupportRepository>()));
            container.Register<IHolidayService>(c => new HolidayService(c.Resolve<IHolidayRepository>()));
            container.Register<IExtraHolidayService>(c => new ExtraHolidayService(c.Resolve<IExtraHolidayRepository>()));
            container.Register<IIncomeService>(c => new IncomeService(c.Resolve<IIncomeRepository>()));
            container.Register<ISocialSecurityService>(c => new SocialSecurityService(c.Resolve<ISocialSecurityRepository>()));
            container.Register<IPreexistingSupportChildService>(c => new PreexistingSupportChildService(c.Resolve<IPreexistingSupportChildRepository>()));
            container.Register<IPreexistingSupportService>(c => new PreexistingSupportService(c.Resolve<IPreexistingSupportRepository>()));
            container.Register<IOtherChildrenService>(c => new OtherChildrenService(c.Resolve<IOtherChildrenRepository>()));
            container.Register<IDeviationsService>(c => new DeviationsService(c.Resolve<IDeviationsRepository>()));
            container.Register<IOtherChildService>(c => new OtherChildService(c.Resolve<IOtherChildRepository>()));
            container.Register<IVehicleFormService>(c => new VehicleFormService(c.Resolve<IVehicleFormRepository>()));
            container.Register<IChildFormService>(c => new ChildFormService(c.Resolve<IChildFormRepository>()));
            container.Register<IAddendumService>(c => new AddendumService(c.Resolve<IAddendumRepository>()));
            container.Register<IClientService>(c => new ClientService(c.Resolve<IClientRepository>()));
            container.Register<IHealthService>(c => new HealthService(c.Resolve<IHealthRepository>()));
            container.Register<IChildCareService>(c => new ChildCareService(c.Resolve<IChildCareRepository>()));
            container.Register<IChildCareFormService>(c => new ChildCareFormService(c.Resolve<IChildCareFormRepository>()));
            container.Register<IPreexistingSupportFormService>(c => new PreexistingSupportFormService(c.Resolve<IPreexistingSupportFormRepository>()));
            container.Register<IExtraExpenseFormService>(c => new ExtraExpenseFormService(c.Resolve<IExtraExpenseFormRepository>()));
            container.Register<IExtraExpenseService>(c => new ExtraExpenseService(c.Resolve<IExtraExpenseRepository>()));
            container.Register<IMenuService>(c => new MenuService(c.Resolve<IChildService>(), c.Resolve<IChildFormService>(), c.Resolve<ICourtService>(), c.Resolve<IOutputService>()));
            container.Register<IBcsoService>(c => new BcsoService(c.Resolve<IBcsoRepository>()));
            container.Register<ILawFirmService>(c => new LawFirmService(c.Resolve<ILawFirmRepository>()));
            container.Register<IAttorneyPageService>(c => new AttorneyPageService(c.Resolve<IAttorneyPageRepository>()));
            container.Register<IAttorneyPageUserService>(c => new AttorneyPageUserService(c.Resolve<IAttorneyPageUserRepository>()));
            container.Register<IAttorneyClientService>(c => new AttorneyClientService(c.Resolve<IAttorneyClientRepository>()));

            container.Register<IOutputService>(c => new OutputService(c.Resolve<IIncomeService>(), c.Resolve<IPreexistingSupportFormService>(), c.Resolve<IOtherChildService>(), c.Resolve<IPreexistingSupportChildService>(), c.Resolve<IOtherChildrenService>(),
                c.Resolve<ICourtService>(), c.Resolve<IParticipantService>(), c.Resolve<IChildService>(), c.Resolve<IPrivacyService>(), c.Resolve<IInformationService>(), c.Resolve<IDecisionsService>(), c.Resolve<IExtraDecisionsService>(), c.Resolve<IHolidayService>(),
                c.Resolve<IExtraHolidayService>(), c.Resolve<IResponsibilityService>(), c.Resolve<ICommunicationService>(), c.Resolve<IScheduleService>(),
                c.Resolve<IHouseService>(), c.Resolve<IPropertyService>(), c.Resolve<IVehicleService>(), c.Resolve<IDebtService>(), c.Resolve<IAssetService>(), c.Resolve<IHealthInsuranceService>(), c.Resolve<ITaxService>(), c.Resolve<ISpousalService>(),
                c.Resolve<IChildSupportService>(), c.Resolve<IVehicleFormService>(), c.Resolve<IChildCareFormService>(), c.Resolve<IExtraExpenseFormService>(), c.Resolve<IHealthService>(), c.Resolve<ISocialSecurityService>(), c.Resolve<IDeviationsService>(), c.Resolve<IChildFormService>(), c.Resolve<IAddendumService>(), c.Resolve<IPreexistingSupportService>()));
        }
        /* Uncomment to enable ServiceStack Authentication and CustomUserSession*/
		private void ConfigureAuth(Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new CustomUserSession(container.Resolve<IUserService>()),
                new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
				}) { HtmlRedirect = null });

            //Default route: /register
            Plugins.Add(new RegistrationFeature());
            //Validation
            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(RestService.UserAuthRestService).Assembly);

            //Requires ConnectionString configured in Web.Config
            var connectionString = ConfigurationManager.ConnectionStrings["SplitContext"].ConnectionString;
            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance));
            container.Register<IUserAuthRepository>(c => new CustomOrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            var authRepo = (CustomOrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            authRepo.CreateMissingTables();

		}
		

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}