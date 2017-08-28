using org.eclipse.jetty.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JettyWithNugetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(8680);
            try
            {
                server.start();
                server.dumpStdErr();
                server.join();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
