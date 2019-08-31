using StoreLib.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace StoreLib.Services
{
    public class MSHttpClient : HttpClient
    {/// <summary>
    /// An override of the SendAsync Function from HttpClient. This is done to automatically add the needed MS-CV tracking header to every request (along with our user-agent).
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) //Overriding the SendAsync so we can easily add the CorrelationVector and User-Agent to every request. 
        {
            using (HttpClient client = new HttpClient())
            {
                CorrelationVector CV = new CorrelationVector();
                CV.Init();
                request.Headers.Add("MS-CV", CV.GetValue());
                request.Headers.TryAddWithoutValidation("User-Agent", "StoreLib");
                HttpResponseMessage response = await client.SendAsync(request);
                return response;
            }
        }
    }
}
