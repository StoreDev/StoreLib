using StoreLib.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Copyright 2018 HexDecimal

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */

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
