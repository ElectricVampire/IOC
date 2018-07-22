using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;

namespace Implementation
{
    public class Email : INotifier
    {
        public void Send()
        {
            Console.WriteLine("Sending Email");
        }
    }
    public class SMS : INotifier
    {
        public void Send()
        {
            Console.WriteLine("Sending SMS");
        }
    }
}
