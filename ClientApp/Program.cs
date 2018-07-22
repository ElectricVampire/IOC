using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using IOC_Container;
using Implementation;
using HighLevelModule;


namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MyIOCContainer container = new MyIOCContainer();
            
            // Register dependency
            container.Register<Email, INotifier>();
            container.Register<SMS, INotifier>();

            // Resolve dependency
            INotifier smsNotifier = container.Resolve<SMS>();
            INotifier emailNotifier = container.Resolve<Email>();

            // Create object of high level module          
            Notify notify = new Notify(smsNotifier);

            // Send
            notify.SendNotification();

            Console.Read();

        }
    }
}
