using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace ConsoleClient
{
    class Program
    {
        static void SignIn(string email, string password, string firstName, string lastName, IHubProxy hub)
        {
            hub.Invoke<string>("signIn", new string[] { email, password, firstName, lastName }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Błąd w rejestracji: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });
        }
        static void LogIn(string email, string password, IHubProxy hub)
        {
            hub.Invoke<string>("logIn", new string[] { email, password }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Błąd w logowaniu: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });
        }
        static void ToQueue(Guid id, string issue, IHubProxy hub)
        {
            Console.WriteLine("dodawanie do kolejki ruszyło");
            hub.Invoke<string>("addUserToQueue", new string[] { id.ToString(), issue }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Błąd w dodawaniu do kolejki: {0}", task.Exception.GetBaseException());
                    //throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Powinno się zakończyć  " + task.Result);
                }
            });
        }
        static void TimeEstimation(Guid id, IHubProxy hub)
        {
            hub.Invoke<string>("estimateTime", id.ToString()).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Błąd w timeEstimation: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });
        }
        static void SendQueue(IHubProxy hub)
        {
            hub.Invoke("SendQueue").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    //Console.WriteLine(task.Result);
                    Console.WriteLine("Sending Queue ...");
                }
            });
            //hub.On<string>("hello", param =>
            //{
            //    Console.WriteLine("hello time estimation:        {0}      count", param);
            //});
        }
        static void DeleteUser(Guid id, IHubProxy hub)
        {
            hub.Invoke<string>("DeleteUserFromQueue", id.ToString()).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Bład w usuwaniu z kolejki: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });
        }
        static void Hello(string message, IHubProxy hub)
        {
            hub.Invoke<string>("Hello", message).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Bład w invokowaniu f. Hello: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });
        }
        static void CreateALotOf(IHubProxy hub)
        {
            Console.WriteLine("czy stworzyć dużą liczbe użytkowników na raz? y/n");
            if (Console.ReadLine() == ("y"))
            {
                Console.WriteLine("Tworzenie...");
                for (int i = 2; i < 20; i++)
                {
                    SignIn(i + "em@il.com", "admin1", "Imię   " + i, "Nazwisko  " + i, hub);
                }

            }
            else
            {
                Console.WriteLine("Nietworzę");
            }
        }
        static void LogInAsNextOne(IHubProxy hub)
        {
            Console.WriteLine("czy logować się po kolei y/n");
            if (Console.ReadLine() == ("y"))
            {
                Console.WriteLine("Tworzenie...");
                for (int i = 2; i < 20; i++)
                {

                    Console.WriteLine("Loguję jako  " + i);
                    LogIn(i + "em@il.com", "admin1", hub);
                    Console.ReadLine();
                }

            }
            else
            {
                Console.WriteLine("nieloguję");
            }
        }
        static void AddToQueueAlot(Person osoba,IHubProxy hub)
        {
            Console.WriteLine("czy dodawać do kolejki y/n");
            if (Console.ReadLine() == ("y"))
            {
                Console.WriteLine("Tworzenie...");
                for (int i = 2; i < 20; i++)
                {
                    LogIn(i + "em@il.com", "admin1", hub);
                    Console.ReadLine();
                    ToQueue(osoba.Id,"Issue "+i, hub);
                }

            }
            else
            {
                Console.WriteLine("nie dodaję");
            }
        }
        static void DeleteAlot(Person osoba, IHubProxy hub)
        {
            Console.WriteLine("czy usuwać do kolejki y/n");
            if (Console.ReadLine() == ("y"))
            {
                Console.WriteLine("Tworzenie...");
                for (int i = 2; i < 20; i++)
                {
                    LogIn(i + "em@il.com", "admin1", hub);
                    Console.ReadLine();
                    DeleteUser(osoba.Id, hub);
                }

            }
            else
            {
                Console.WriteLine("nie usuwam");
            }
        }

        static void Main(string[] args)

        {
            Person osoba = new Person();
            //Set connection
            var connection = new HubConnection("http://kolejkomatapp4.azurewebsites.net/signalr/hubs");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("mainHub");
            myHub.On<string>("hello", s1 =>
            {
                Console.WriteLine("hello: {0}", s1);
            });
            myHub.On<string>("accountCreated", param =>
            {
                if (param == "True") Console.WriteLine("account created        {0}      type: {1}", param, param.GetType());
                else Console.WriteLine("niezalogowano");
            });
            myHub.On<string, bool>("loggedIn", (s1, s2) =>
            {
                //osoba.Id = Guid.Empty;
                osoba.Id = Guid.Parse(s1);
                //Console.WriteLine("id :{0}      typ{1}", s1, s1.GetType());
                Console.WriteLine("logged in, id :{0}", osoba.Id);
            });
            myHub.On<string>("timeEstimation", param =>
            {
                Console.WriteLine("time estimation:        {0}      type: {1}", param, param.GetType());
            });
            myHub.On<string, string>("hello", (param, param2) =>
            {
                Console.WriteLine("hello param, param:        {0}      param2: {1}", param, param2);
            });
            myHub.On<string>("userAdded", param =>
            {
                Console.WriteLine("dodano do kolejki:        {0}", param);
                //if (param == "True")
                //{
                //    myHub.Invoke<string>("estimateTime", osoba.Id.ToString()).ContinueWith(task =>
                //    {
                //        if (task.IsFaulted)
                //        {
                //            Console.WriteLine("Błąd w timeEstimation: {0}", task.Exception.GetBaseException());
                //            throw (task.Exception.GetBaseException());
                //        }
                //        else
                //        {
                //            Console.WriteLine(task.Result);
                //        }
                //    });
                //}
            });
            myHub.On<string>("itsURTime", param =>
            {
                if (osoba.Id.ToString() == param) Console.WriteLine("To Twoja Kolej!!:        {0}", param);
            });
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
            DeleteAlot(osoba, myHub);
            AddToQueueAlot(osoba, myHub);
            LogInAsNextOne(myHub);

            Console.ReadLine();
            connection.Stop();
            
        }
        
        public static void Metoda(string s1, string s2)
        {
            Console.WriteLine("action = metoda       {0}        {1}", s1, s2);
        }
        //static void OnHello()
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
