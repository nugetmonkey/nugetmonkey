using org.apache.camel;
using org.apache.camel.builder;
using org.apache.camel.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CamelTest
{
    public class Rb : RouteBuilder
    {
        public override void configure()
        {
            /**
             * Camel file is not ready for IKVM for now.
             * Just for demonstrating puposes
             */
            from("file://test1").to("file://test2");

            from("stream:in?promptMessage=Enter something: ").transform(simple("${body.toUpperCase()}")).to("stream:out");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CamelContext context = new DefaultCamelContext();
            context.addRoutes(new Rb());
            /**
             * Camel is not ready for IKVM for now
             * Just for demonstrating puposes
             */
            context.start();
            System.Threading.Thread.CurrentThread.Join();
        }
    }
}
