using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using API1;
using System.Net.Http;
using System.Web.Http;
using API1.Models;

namespace API1
{
    public class Startup
    {

        static public readonly HttpClient _client = new HttpClient()
        {
            BaseAddress = _clientUri,

        };
        //static public HttpServer _server { get; private set; }
        //static public SampleHandler SH { get; set; }
        static public Uri _serverUri = new Uri(@"http://192.168.0.127:62246/api/");
        static public Uri _clientUri = new Uri(@"http://192.168.0.129:62246/api/");
        static public Dictionary<string, string> ValuesDic = new Dictionary<string, string>{
            {"string1", "string2" },
            {"string3", "string4" }
        };
        //static public HttpMessageHandler _messageHandler { get; set;  }

        //private Uri _clientPrivateUri { get; set; }
        //private Uri _serverPrivateUri { get; set; }

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();


           
            
            //SH = new  SampleHandler(_clientUri);
            //_client = new HttpClient(SH)
            //{
              //  BaseAddress = _clientUri
            //};

            //Person person = new Person();
            //PositionInQueue pos = new PositionInQueue(person, 1, new DateTime(2018,01,26));
            //_server = new HttpServer(SH);



        }
    }
}