using Kinetic.Core.Clients.HereApi;
using Kinetic.Core.Clients.HereApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinetic.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            HereApiClient client = new HereApiClient();
            Location location = client.GetPoints("fdsfsfd");
            Console.ReadKey();
        }
    }
}
