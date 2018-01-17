using EventStore.ClientAPI;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SecondWebAPI
{
    public class AccountHub : Hub
    {
        public void GetActualState(string connectionId)
        {
            IEventStoreConnection connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync();

            var results = Task.Run(() => connection.ReadStreamEventsForwardAsync(connectionId, StreamPosition.Start, 999, false));

            Task.WaitAll();

            var resultsData = results.Result;
            var bankState = new BankAccount();

            foreach (var evnt in resultsData.Events)
            {
                var esJsonData = Encoding.UTF8.GetString(evnt.Event.Data);

                if (evnt.Event.EventType == "AccountCreatedEvent")
                {
                    var objState = JsonConvert.DeserializeObject<AccountCreateEvent>(esJsonData);
                    Console.WriteLine(objState.Id + " " + objState.Name);
                    bankState.Apply(objState);
                }
                else if (evnt.Event.EventType == "FundsDespoitedEvent")
                {
                    var objState = JsonConvert.DeserializeObject<FundsDespoitedEvent>(esJsonData);
                    Console.WriteLine(objState.Id + " " + objState.Amount);
                    bankState.Apply(objState);
                }
                else if (evnt.Event.EventType == "FundsWithdrawedEvent")
                {
                    var objState = JsonConvert.DeserializeObject<FundsWithdrawedEvent>(esJsonData);
                    Console.WriteLine(objState.Id + " " + objState.Amount);
                    bankState.Apply(objState);
                }
            }

            Clients.Client(connectionId).broadcastMessage(bankState.CurrentBalance);
        }

        public void SaveTransaction(IEvent evnt,string connectionId)
        {
            IEventStoreConnection connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync();

            var jsonString = JsonConvert.SerializeObject(evnt, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
            var jsonPayload = Encoding.UTF8.GetBytes(jsonString);
            var eventStoreDataType = new EventData(Guid.NewGuid(), evnt.GetType().Name, true, jsonPayload, null);
            connection.AppendToStreamAsync(connectionId, ExpectedVersion.Any, eventStoreDataType);
        }

      
    }
}