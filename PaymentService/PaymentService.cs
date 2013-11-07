using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Contracts;
using DataInterface;
using DataLayerContext.OrmLiteRepositories;
using FriendlyForms.Recurring;
using FriendlyForms.RestService;
using Models;
using ServiceStack.Common.Web;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface.Auth;
using Container = Funq.Container;
using ServiceBase = System.ServiceProcess.ServiceBase;

namespace PaymentService
{
    partial class PaymentService : ServiceBase
    {
        private Container FunqContainer { get; set; }
        private IUserService UserService { get; set; }
        private IDbConnectionFactory ConnectionFactory { get; set; }
        private IUserRepository UserRepository { get; set; }
        private IEmailService EmailService { get; set; }
        private IPaymentService PaymentsService { get; set; }
        private ILawFirmRepository LawFirmRepository { get; set; }
        private const string UserName = "spli8523";
        private const string Password = "091BL6rX";
        private const string Vendor = "7866";
        private const double ExcessUserRate = 80;

        public PaymentService()
        {
            InitializeComponent();
            var fileName = DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString() + ".txt";
            var path = @"C:\Mine\Logs\" + fileName;
            //using (StreamWriter sw = File.CreateText(path))
            //{
            //    sw.WriteLine("Hello world, the service has started");
            //}  
            //using (var file = File.CreateText(path))
            //{
            //    try
            //    {
            //        InitializeComponent();
            //        FunqContainer = new Container();
            //        InitializeContainer();
            //    }
            //    catch (Exception ex)
            //    {
            //        file.WriteLine(@"Source: {0}", ex.Source);
            //        file.WriteLine(@"Message: {0}", ex.Message);
            //        file.WriteLine(@"Stack Trace: {0}", ex.StackTrace);
            //        if (ex.InnerException != null)
            //        {
            //            file.WriteLine(@"Inner Ex Source: {0}", ex.InnerException.Source);
            //            file.WriteLine(@"Inner Ex Message: {0}", ex.InnerException.Message);
            //            file.WriteLine(@"Inner Ex Stack Trace: {0}", ex.InnerException.StackTrace);
            //        }
            //    }
            //}
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            ////FunqContainer = new Container();
            InitializeContainer();
            //var userService = FunqContainer.TryResolve<IUserService>();
            //var lawFirmRepository = FunqContainer.TryResolve<ILawFirmRepository>();
            var emailMessageBody = "The following is a log of the accounts charged: <br><br>";
            var accounts = UserService.GetTodaysActiveAccounts();
            foreach (var account in accounts)
            {
                var amount = CalculateAmountOwed(account);
                if (amount > 0)
                {
                    emailMessageBody += ChargeAmount(amount, account);
                }
            }            
            EmailService.SendEmail(new List<string> { "elnels@gmail.com" }, "Recurring Payment Summary", emailMessageBody);
        }

        private void InitializeContainer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SplitContext"].ConnectionString;
            ConnectionFactory = new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance);
            UserRepository = new UserOrmLiteRepository(ConnectionFactory);
            EmailService = new EmailService();
            UserService = new UserService(UserRepository, EmailService);
            PaymentsService = new BusinessLogic.PaymentService();
            LawFirmRepository = new LawFirmOrmLiteRepository(ConnectionFactory);
            //FunqContainer.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance));
            //FunqContainer.Register<IUserRepository>(c => new UserOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            //FunqContainer.Register<IEmailService>(c => new EmailService());
            //FunqContainer.Register<IUserService>(c => new UserService(c.Resolve<IUserRepository>(), c.Resolve<IEmailService>()));
            //FunqContainer.Register<IPaymentService>(c => new BusinessLogic.PaymentService());
        }

        private string ChargeAmount(double amount, User user)
        {
            var recurringSoapClient = new RecurringSoapClient("RecurringSoap");
            var response = recurringSoapClient.ProcessCreditCard(UserName, Password, Vendor, user.CcInfoKey, amount.ToString(), "", "");
            var lawFirm = LawFirmRepository.Get(user.LawFirmId.Value);
            if (response.code == ResultCode.OK)
            {
                return lawFirm.Name + " was successfully charged the amount of $" + amount;
            }
            return lawFirm.Name + " was NOT charged the amount of $" + amount + " for the following reason: " + response.error + "; response code = " + response.code + "<br/>";
        }

        private double CalculateAmountOwed(User account)
        {
            if (account.AmountId == null)
                return 0;
            var monthlyAmount = PaymentsService.GetAmount(account.AmountId.Value);
            var maxMonthlyUsers = PaymentsService.GetMaxMonthlyUsers(account.AmountId.Value);
            var accountMonthlyUsers = UserService.GetNumberOfUsersAddedThisMonth(account);
            if (accountMonthlyUsers > maxMonthlyUsers)
            {
                var excessUserCharge = (accountMonthlyUsers - maxMonthlyUsers) * ExcessUserRate;
                monthlyAmount += excessUserCharge;
            }
            return monthlyAmount;
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
