using System.ServiceProcess;

namespace QtES.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var service = new Service1();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                
            };
            service.Start(args);
# if Debug 
            Serv
# endif
# if Release
            ServiceBase.Run(ServicesToRun);
# endif
        }
    }
}
