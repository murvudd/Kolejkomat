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

            Person osoba = new Person();
            //Set connection
            var connection = new HubConnection("http://kolejkomatapp4.azurewebsites.net/signalr/hubs");
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
            //SignIn("nowy.ziomek@gmail.com", "haslo1", "nowy", "ziomek", myHub);
            //myHub.On<string>("accountCreated", param =>
            //{
            //    if(param =="True") Console.WriteLine("account created        " + param);
            //});

            //LogIn("Blendzior@2137.pl", "admin1", osoba, myHub);
            //ToQueue(osoba.Id, "KUUUUUUUUUUUUUUUUUUUHWA", osoba, myHub);

            //LogIn("nowymail@gmail.com", "jakieshaslo", osoba, myHub);
            //ToQueue(osoba.Id, "jakis issue", osoba, myHub);

            LogIn("przyklad@przyklad.pl ", "superhaslo", osoba, myHub);
            //ToQueue(osoba.Id, "przykładny issue", osoba, myHub);
            TimeEstimation(osoba.Id, osoba, myHub);
            //myHub.Invoke<string>("Hello").Wait();
            Console.ReadLine();
            connection.Stop();
        }


        static void SignIn(string email, string password, string firstName, string lastName, Person osoba, IHubProxy hub)
        {
            hub.Invoke<string>("signIn", new string[] { email, password, firstName, lastName }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            hub.On<string>("accountCreated", param =>
            {
                if (param == "True") Console.WriteLine("account created        {0}      type: {1}", param, param.GetType());
            });
        }

        static void LogIn(string email, string password, Person osoba, IHubProxy hub)
        {
            hub.Invoke<string>("logIn", new string[] { email, password}).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            hub.On<Guid>("loggedIn", param =>
            {
                osoba.Id = param;
                Console.WriteLine("zalogowano, ID:        {0}", param);
            });
        }

        static void ToQueue(Guid id, string issue, Person osoba, IHubProxy hub)
        {
            hub.Invoke<string>("addUserToQueue", new string[] { id.ToString(), issue}).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            hub.On<Guid>("userAdded", param =>
            {
                osoba.Id = Guid.Parse(param.ToString());
                Console.WriteLine("zalogowano, ID:        {0}      type: {1}", param, param.GetType());
            });
        }

        static void TimeEstimation(Guid id, Person osoba, IHubProxy hub)
        {
            hub.Invoke<string>("estimateTime",  id.ToString()  ).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });

            hub.On<string>("timeEstimation", param =>
            {
                
                Console.WriteLine("zalogowano, ID:        {0}      type: {1}", param, param.GetType());
            });
        }
    }

public class Person
    {
        public Guid Id { get; set; }

        public Person()
        {
            Id = Guid.Empty;
        }
    }
}
