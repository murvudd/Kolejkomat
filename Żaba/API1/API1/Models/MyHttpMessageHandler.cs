//using System;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace API1
//{
//    //public class MyHttpMessageHandler : HttpMessageHandler
//    //{
//    //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//    //    {
//    //        IPrincipal principal = new GenericPrincipal(
//    //            new GenericIdentity("myuser"), new string[] { "myrole" });
//    //        Thread.CurrentPrincipal = principal;
//    //        HttpContext.Current.User = principal;

//    //        return Task<HttpResponseMessage>.Factory.StartNew(() => request.CreateResponse());
//    //    }
//    //}

//    /// <summary>
//    /// This sample HttpMessageHandler illustrates how to perform an async operation as part of the 
//    /// HTTP message handler pipeline using the async and await keywords from .NET 4.5
//    /// </summary>
//    public class SampleHandler : DelegatingHandler
//    {
//        private HttpClient _client;
//        private Uri _address;

//        public SampleHandler(Uri address)
//        {
//            if (address == null)
//            {
//                throw new ArgumentNullException("address");
//            }

//            if (!address.IsAbsoluteUri)
//            {
//                throw new ArgumentException("Value must be an absolute URI", "address");
//            }
//            _address = address;
//            _client = new HttpClient();
//        }

//        /// <summary>
//        /// We mark the SendAsync with 'async' so that we can await tasks without blocking in the method body.
//        /// </summary>
//        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            // Here I can inspect the request and do any pre-processing

//            // Submit intermediate request and await response
//            HttpResponseMessage intermediateResponse = await _client.GetAsync(_address);

//            // If intermediate response is not successful then return a 500 status code
//            if (!intermediateResponse.IsSuccessStatusCode)
//            {
//                return request.CreateResponse(HttpStatusCode.InternalServerError);
//            }

//            // Read intermediate response
//            byte[] intermediateContent = await intermediateResponse.Content.ReadAsByteArrayAsync();

//            // Now continue with the original request
//            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

//            // Here I can inspect the response and do any post-processing
//            return response;
//        }
//    }
//}