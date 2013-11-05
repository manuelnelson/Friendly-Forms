using System.ServiceProcess;

namespace PaymentService
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicesToRun = new ServiceBase[] 
                { 
                    new PaymentService(),  
                };
            ServiceBase.Run(servicesToRun);
        }
    }
}
