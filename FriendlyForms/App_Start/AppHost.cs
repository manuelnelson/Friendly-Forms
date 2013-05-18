using System.Configuration;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext;
using DataLayerContext.Repositories;
using FriendlyForms.Helpers;
using FriendlyForms.Models;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
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

	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Split Solution Api", typeof(RestService.CourtRestService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = false;

			//Change the default ServiceStack configuration
			//SetConfig(new EndpointHostConfig {
			//    DebugMode = true, //Show StackTraces in responses in development
			//});

			//Enable Authentication
			ConfigureAuth(container);

			//Register all your dependencies			
            LogManager.LogFactory = new ElmahLogFactory(new NullLogFactory());

            //Make the default lifetime of objects limited to request
            container.DefaultReuse = ReuseScope.Request;

            SetupRepositories(container);

		    SetupServices(container);

			//Register In-Memory Cache provider. 
			//For Distributed Cache Providers Use: PooledRedisClientManager, BasicRedisClientManager or see: https://github.com/ServiceStack/ServiceStack/wiki/Caching
            #if DEBUG
            container.Register<ICacheClient>(new MemoryCacheClient());
#else
            container.Register<ICacheClient>(new AzureCacheClient());
            //use azure cache client
#endif
            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>()));

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

	    private void SetupRepositories(Container container)
	    {
            var connectionString = ConfigurationManager.ConnectionStrings["SplitContext"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance)
                {
                    ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                });

            container.Register<IUnitOfWork>(c => new SplitContext());
            container.Register<ICourtRepository>(c => new CourtRepository(c.Resolve<IUnitOfWork>()));
            //container.Register<IUserRepository>(c => new UserOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
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
            container.Register<IDeviationsFormRepository>(c => new DeviationsFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IExtraExpenseFormRepository>(c => new ExtraExpenseFormRepository(c.Resolve<IUnitOfWork>()));
            container.Register<IExtraExpenseRepository>(c => new ExtraExpenseRepository(c.Resolve<IUnitOfWork>()));
        }

        private void SetupServices(Container container)
        {
            container.Register<ICourtService>(c => new CourtService(c.Resolve<ICourtRepository>() as CourtRepository));
            container.Register<IParticipantService>(c => new ParticipantService(c.Resolve<IParticipantRepository>() as ParticipantRepository));
            container.Register<IChildService>(c => new ChildService(c.Resolve<IChildRepository>() as ChildRepository));
            container.Register<IPrivacyService>(c => new PrivacyService(c.Resolve<IPrivacyRepository>() as PrivacyRepository));
            container.Register<IInformationService>(c => new InformationService(c.Resolve<IInformationRepository>() as InformationRepository));
            container.Register<IDecisionsService>(c => new DecisionsService(c.Resolve<IDecisionRepository>() as DecisionRepository));
            container.Register<IExtraDecisionsService>(c => new ExtraDecisionsService(c.Resolve<IExtraDecisionRepository>()));
            container.Register<IEmailService>(c => new EmailService());
            container.Register<IUserService>(c => new UserService(c.Resolve<IUserRepository>(), c.Resolve<IEmailService>()));
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
            container.Register<IDeviationsService>(c => new DeviationsService(c.Resolve<IDeviationsRepository>() as DeviationsRepository));
            container.Register<IOtherChildService>(c => new OtherChildService(c.Resolve<IOtherChildRepository>() as OtherChildRepository));
            container.Register<IVehicleFormService>(c => new VehicleFormService(c.Resolve<IVehicleFormRepository>() as VehicleFormRepository));
            container.Register<IChildFormService>(c => new ChildFormService(c.Resolve<IChildFormRepository>() as ChildFormRepository));
            container.Register<IAddendumService>(c => new AddendumService(c.Resolve<IAddendumRepository>() as AddendumRepository));
            container.Register<IClientService>(c => new ClientService(c.Resolve<IClientRepository>() as ClientRepository));
            container.Register<IHealthService>(c => new HealthService(c.Resolve<IHealthRepository>() as HealthRepository));
            container.Register<IChildCareService>(c => new ChildCareService(c.Resolve<IChildCareRepository>() as ChildCareRepository));
            container.Register<IChildCareFormService>(c => new ChildCareFormService(c.Resolve<IChildCareFormRepository>() as ChildCareFormRepository));
            container.Register<IPreexistingSupportFormService>(c => new PreexistingSupportFormService(c.Resolve<IPreexistingSupportFormRepository>() as PreexistingSupportFormRepository));
            container.Register<IDeviationsFormService>(c => new DeviationsFormService(c.Resolve<IDeviationsFormRepository>() as DeviationsFormRepository));
            container.Register<IExtraExpenseFormService>(c => new ExtraExpenseFormService(c.Resolve<IExtraExpenseFormRepository>() as ExtraExpenseFormRepository));
            container.Register<IExtraExpenseService>(c => new ExtraExpenseService(c.Resolve<IExtraExpenseRepository>() as ExtraExpenseRepository));
        }

	    // Uncomment to enable ServiceStack Authentication and CustomUserSession
		private void ConfigureAuth(Funq.Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new CustomUserSession(container.Resolve<IUserService>()), 
				new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
				}) {HtmlRedirect = "/"}); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature()); 

			//Requires ConnectionString configured in Web.Config
            var connectionString = ConfigurationManager.ConnectionStrings["SplitContext"].ConnectionString;
			container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance));

			container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

			var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
			authRepo.CreateMissingTables();
		}
		

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}