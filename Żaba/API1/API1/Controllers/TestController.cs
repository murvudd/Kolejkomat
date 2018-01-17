using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API1;
using System.Threading;
using System.Threading.Tasks;

namespace API1.Controllers
{
    public class TestController : ApiController
    {



        // GET api/<controller>
        [HttpGet]
        async public Task<HttpRequestMessage> Get()
        {
            var content = new FormUrlEncodedContent(Startup.ValuesDic);
            var Response = await Startup._client.PostAsync(Startup._clientUri, content);
            var ResponseResult = await Response.Content.ReadAsHttpRequestMessageAsync();
            return ResponseResult;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            //HttpClient _client = new HttpClient();
            //_client.BaseAddress = ;
            return $"value {id}";
        }

        // POST api/<controller>
        //public void Post([FromBody]string value)
        [HttpPost]
        async public Task<HttpRequestMessage> Post()
        {
            var content = new FormUrlEncodedContent(Startup.ValuesDic);
            var Response = await Startup._client.PostAsync(Startup._clientUri, content);
            var ResponseResult = await Response.Content.ReadAsHttpRequestMessageAsync();
            return ResponseResult;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}