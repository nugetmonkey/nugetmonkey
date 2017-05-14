using org.apache.camel;
using org.apache.camel.builder;
using org.apache.camel.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Test1
{
    public class Rb:RouteBuilder
    {
        public override void configure()
        {
            from("file://test1").to("file://test2");
        } 
    }
    class Program
    {
        static void Main(string[] args)
        {
            CamelContext context = new DefaultCamelContextNameStrategy("sd");
            context.addRoutes(new Rb() );
            context.start();
            System.Threading.Thread.CurrentThread.Join();
        }
    }
}
