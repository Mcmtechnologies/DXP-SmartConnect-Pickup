using Newtonsoft.Json;
using System;

namespace DXP.SmartConnectPickup.ConsoleAppTesting
{
    class Program
    {
        protected Program()
        {

        }

        static void Main(string[] args)
        {
            object obj = "Data";
            Console.WriteLine(JsonConvert.SerializeObject(obj));
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
