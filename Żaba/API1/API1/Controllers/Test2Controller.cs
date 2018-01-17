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
    public class Test2Controller : ApiController
    {
        //private static readonly HttpClient _client = new HttpClient() {
        //    //BaseAddress = Startup._serverUri
        //};


        // GET: api/Test2
        [HttpGet]
        async public Task<HttpRequestMessage> Get()
        {
            var content = new FormUrlEncodedContent(Startup.ValuesDic);
            var Response = await Startup._client.PostAsync(Startup._serverUri, content);
            var ResponseResult = await Response.Content.ReadAsHttpRequestMessageAsync();
            return ResponseResult;
        }


        // GET: api/Test2/5
        [HttpGet()]
        [Route("api/[controller]/{id:int}") ]
        async public Task<HttpRequestMessage> Get(int id)
        {
            string str = "test/" + id;
            var content = new FormUrlEncodedContent(Startup.ValuesDic);
            var Response = await Startup._client.PostAsync(new Uri(Startup._serverUri, str), content);
            var ResponseResult = await Response.Content.ReadAsHttpRequestMessageAsync();
            return ResponseResult;
        }

        // POST: api/Test2
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Test2/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Test2/5
        public void Delete(int id)
        {
        }
    }
}
