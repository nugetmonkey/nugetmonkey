using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var headers = new HttpClient().DefaultRequestHeaders;
             var  headers1 = new HttpResponseMessage().Headers;
              headers = new HttpRequestMessage().Headers;
        }
    }
}
