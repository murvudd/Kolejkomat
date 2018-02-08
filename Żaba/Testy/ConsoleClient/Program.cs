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
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
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
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
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
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
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
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
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
                    Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
                    throw (task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });
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
                Console.WriteLine("param: {0}      type:{1}", s1, s1.GetType());
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
                Console.WriteLine("id :{0}      typ{1}", s1, s1.GetType());
                Console.WriteLine("id :{0}", osoba.Id);


            });
            myHub.On<string>("timeEstimation", param =>
            {
                Console.WriteLine("time estimation:        {0}      type: {1}", param, param.GetType());
            });
            myHub.On<string, string>("hello", (param, param2) =>
            {
                Console.WriteLine("param, param:        {0}      count: {1}", param, param2);
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

            Thread.Sleep(5000);
            LogIn("han@solo.com", "hansolo", myHub);
            Console.ReadLine();
            Console.WriteLine("id powino byc: {0}", osoba.Id);

            ToQueue(osoba.Id, "dodano z clienta, bad feeling", myHub);
            //Thread.Sleep(5000);
            //DeleteUser(osoba.Id, myHub);



            Console.WriteLine("id powino byc: {0}", osoba.Id);
            {
                //SignIn("nowy.ziomek@gmail.com", "haslo1", "nowy", "ziomek", myHub);
                //myHub.On<string>("accountCreated", param =>
                //{
                //    if(param =="True") Console.WriteLine("account created        " + param);
                //});

                //LogIn("Blendzior@2137.pl", "admin1", osoba, myHub);
                //ToQueue(osoba.Id, "KUUUUUUUUUUUUUUUUUUUHWA", osoba, myHub);

                //LogIn("nowymail@gmail.com", "jakieshaslo", osoba, myHub);
                //ToQueue(osoba.Id, "jakis issue", osoba, myHub);

                //LogIn("przyklad@przyklad.pl ", "superhaslo", osoba, myHub);
                //ToQueue(osoba.Id, "przykładny issue", osoba, myHub);


                //ToQueue(osoba.Id, "i have a bad feeling about this", osoba, myHub);
                //TimeEstimation(osoba.Id, osoba, myHub);
                //SendQueue(myHub);
                //ToQueue(osoba.Id, "i have a bad feeling about this", osoba, myHub);
                //ToQueue(Guid.NewGuid(), "nieistniejąca osoba", osoba, myHub);
                //Thread.Sleep(5000);
                //DeleteUser(osoba.Id, osoba, myHub);
                //TimeEstimation(osoba.Id, osoba, myHub);

                //myHub.Invoke<string>("Hello").Wait();
            }
            Console.ReadLine();
            connection.Stop();

            //connection.Start().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error opening the connection:{0}",
            //                      task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine("Connected");
            //    }

            //}).Wait();

            //LogIn("Blendzior@2137.pl", "admin1", myHub);
            ////Thread.Sleep(5000);
            ////Console.WriteLine("id powino byc: {0}", osoba.Id);
            //ToQueue(osoba.Id, "kuuuuhwa", myHub);


            //Console.ReadLine();
            //connection.Stop();
        }

        

        //static void SignIn(string email, string password, string firstName, string lastName, Person osoba, IHubProxy hub)
        //{
        //    hub.Invoke<string>("signIn", new string[] { email, password, firstName, lastName }).ContinueWith(task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
        //            throw (task.Exception.GetBaseException());
        //        }
        //        else
        //        {
        //            Console.WriteLine(task.Result);
        //        }
        //    });

        //    //hub.On<string>("accountCreated", param =>
        //    //{
        //    //    if (param == "True") Console.WriteLine("account created        {0}      type: {1}", param, param.GetType());
        //    //});
        //}



        

        //static void LogIn(string email, string password, Person prsn, IHubProxy hub)
        //{
        //try
        //{

        //hub.Invoke<string>("logIn", new string[] { email, password }).ContinueWith(task =>
        // {
        //     if (task.IsFaulted)
        //     {
        //         Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
        //         throw (task.Exception.GetBaseException());
        //     }
        //     else
        //     {
        //         Console.WriteLine(task.Result);
        //     }
        // });
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}

        //hub.On<string, bool>("loggedIn", (s1, s2) =>
        //{

        //    //osoba.Id = Guid.Empty;
        //    prsn.Id = Guid.Parse(s1);
        //    Console.WriteLine("id :{0}      typ{1}", s1, s1.GetType());
        //    Console.WriteLine("id :{0}", prsn.Id);


        //});
        //}



        

        //static void ToQueue(Guid id, string issue, Person osoba, IHubProxy hub)
        //{
        //    //try
        //    //{

        //    Console.WriteLine("dodawanie do kolejki ruszyło");
        //    hub.Invoke<string>("addUserToQueue", new string[] { id.ToString(), issue }).ContinueWith(task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
        //            throw (task.Exception.GetBaseException());
        //        }
        //        else
        //        {
        //            Console.WriteLine("Powinno się zakończyć  " + task.Result);
        //        }
        //    });

        //    //hub.On<string,string>("hello", (s1, s2) =>
        //    //{
        //    //    Console.WriteLine("s1 {0}      s2 {1}", s1, s2);
        //    //});

        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    Console.WriteLine(e);
        //    //    throw;
        //    //}
        //    //hub.On<string>("hello", s1 =>
        //    //{
        //    //    Console.WriteLine("person, {0}      type:{1}", s1, s1.GetType());
        //    //});
        //    //System.Action<string, string> action;
        //    //action = Metoda;
        //    //hub.On<string, string>("hello", action);
        //    //hub.On<Guid>("userAdded", param =>
        //    //{
        //    //    osoba.Id = Guid.Parse(param.ToString());
        //    //    Console.WriteLine("dodano do kolejki, ID:        {0}      type: {1}", param, param.GetType());
        //    //});
        //}



        

        //static void TimeEstimation(Guid id, Person osoba, IHubProxy hub)
        //{
        //    //try
        //    //{

        //    hub.Invoke<string>("estimateTime", id.ToString()).ContinueWith(task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
        //            throw (task.Exception.GetBaseException());
        //        }
        //        else
        //        {
        //            Console.WriteLine(task.Result);
        //        }
        //    });

        //    //hub.On<string>("timeEstimation", param =>
        //    //{
        //    //    Console.WriteLine("time estimation:        {0}      type: {1}", param, param.GetType());
        //    //});

        //    //hub.On<string, string>("hello", (param, param2) =>
        //    //{
        //    //    Console.WriteLine("param, param:        {0}      count: {1}", param, param2);
        //    //});
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    Console.WriteLine(e);
        //    //    throw;
        //    //}
        //}



        

        //static void DeleteUser(Guid id, Person osoba, IHubProxy hub)
        //{
        //    try
        //    {
        //        hub.Invoke<string>("DeleteUserFromQueue", id.ToString()).ContinueWith(task =>
        //        {
        //            if (task.IsFaulted)
        //            {
        //                Console.WriteLine("There was an error: {0}", task.Exception.GetBaseException());
        //                throw (task.Exception.GetBaseException());
        //            }
        //            else
        //            {
        //                Console.WriteLine(task.Result);
        //            }
        //        });

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //    //hub.On<string>("hello", param =>
        //    //{

        //    //    Console.WriteLine("param:  {0}      type: {1}", param, param.GetType());
        //    //});
        //}



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
