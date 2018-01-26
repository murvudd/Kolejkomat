using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;


namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            //Set connection
            var connection = new HubConnection("http://kolejkomatapp.azurewebsites.net/signalr/hubs");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("mainHub");
            //Start connection

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();


            myHub.Invoke<string>("logIn", new string[]{ "login ", "mail" }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error calling send: {0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            //myHub.On<string>("addNewMessageToPage", param =>
            //{
            //    Console.WriteLine(param);
            //});

            //myHub.Invoke<string>("Hello").Wait();


            Console.Read();
            connection.Stop();
        }
    }
}
