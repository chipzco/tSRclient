using System;
using Microsoft.AspNet.SignalR.Client;
using Serilog;

namespace tSRclient
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration().WriteTo.LiterateConsole().CreateLogger();

            Log.Information("Ah, there you are!");

            string baseAddress = "http://localhost:60180/";
            Log.Information("No one listens to me!");







            var hubConnection = new HubConnection(baseAddress);
            IHubProxy eventHubProxy = hubConnection.CreateHubProxy("tSRHub");
            
            try
            {
                hubConnection.Start().Wait();
              
            }
            catch(Exception e) {
                
                Log.Warning(e.Message); Log.Information(e.StackTrace.ToString());
                Log.Warning(e.Source);

            }
            eventHubProxy.Invoke("send", "TSR CLIENT","FUCK WHY NOT REPLY");

            eventHubProxy.On<string, string>("broadcastMessage", (name, message) => Log.Information("Event received by {name} : {message}", name, message));

            Console.WriteLine($"Server is running on {baseAddress}");
            Console.WriteLine("DO you want to send a message? - press any key to do so");
            Console.ReadLine();
            sendChat(eventHubProxy);
          

        }

        static void sendChat(IHubProxy eventHubProxy)
        {
            Console.WriteLine("Write a message");
            string msg = Console.ReadLine();
            eventHubProxy.Invoke("send", "TSR CLIENT",msg);
            Console.WriteLine("DO you want to send another  message? - press any key to do so");
            Console.ReadLine();
            sendChat(eventHubProxy);
        }

        static void calculator()
        {
            Console.WriteLine("Write a number for x");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Write a number for y");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("// Choose a option //");
            Console.WriteLine("1 - addition");
            Console.WriteLine("2 - subtration ");
            Console.WriteLine("3 - multiplication");
            Console.WriteLine("4 - division");
            int z = Convert.ToInt32(Console.ReadLine());
            switch (z)
            {
                case 1:
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine("// Result //");
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine(x + y);
                    break;
                case 2:
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine("// Result //");
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine(x - y);
                    break;
                case 3:
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine("// Result //");
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine(x * y);
                    break;
                case 4:
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine("// Result //");
                    Console.WriteLine("///////////////////////");
                    Console.WriteLine(x / y);
                    break;
            }
            Console.ReadKey(true);
        }
    }
}
