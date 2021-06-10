
using StoreLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;


namespace StoreLib.Services
{
    public static class FE3Handler
    {
        private static readonly MSHttpClient _httpClient = new MSHttpClient();
        private static readonly String _msaToken = "<Device>dAA9AEUAdwBBAHcAQQBzAE4AMwBCAEEAQQBVADEAYgB5AHMAZQBtAGIAZQBEAFYAQwArADMAZgBtADcAbwBXAHkASAA3AGIAbgBnAEcAWQBtAEEAQQBMAGoAbQBqAFYAVQB2AFEAYwA0AEsAVwBFAC8AYwBDAEwANQBYAGUANABnAHYAWABkAGkAegBHAGwAZABjADEAZAAvAFcAeQAvAHgASgBQAG4AVwBRAGUAYwBtAHYAbwBjAGkAZwA5AGoAZABwAE4AawBIAG0AYQBzAHAAVABKAEwARAArAFAAYwBBAFgAbQAvAFQAcAA3AEgAagBzAEYANAA0AEgAdABsAC8AMQBtAHUAcgAwAFMAdQBtAG8AMABZAGEAdgBqAFIANwArADQAcABoAC8AcwA4ADEANgBFAFkANQBNAFIAbQBnAFIAQwA2ADMAQwBSAEoAQQBVAHYAZgBzADQAaQB2AHgAYwB5AEwAbAA2AHoAOABlAHgAMABrAFgAOQBPAHcAYQB0ADEAdQBwAFMAOAAxAEgANgA4AEEASABzAEoAegBnAFQAQQBMAG8AbgBBADIAWQBBAEEAQQBpAGcANQBJADMAUQAvAFYASABLAHcANABBAEIAcQA5AFMAcQBhADEAQgA4AGsAVQAxAGEAbwBLAEEAdQA0AHYAbABWAG4AdwBWADMAUQB6AHMATgBtAEQAaQBqAGgANQBkAEcAcgBpADgAQQBlAEUARQBWAEcAbQBXAGgASQBCAE0AUAAyAEQAVwA0ADMAZABWAGkARABUAHoAVQB0AHQARQBMAEgAaABSAGYAcgBhAGIAWgBsAHQAQQBUAEUATABmAHMARQBGAFUAYQBRAFMASgB4ADUAeQBRADgAagBaAEUAZQAyAHgANABCADMAMQB2AEIAMgBqAC8AUgBLAGEAWQAvAHEAeQB0AHoANwBUAHYAdAB3AHQAagBzADYAUQBYAEIAZQA4AHMAZwBJAG8AOQBiADUAQQBCADcAOAAxAHMANgAvAGQAUwBFAHgATgBEAEQAYQBRAHoAQQBYAFAAWABCAFkAdQBYAFEARQBzAE8AegA4AHQAcgBpAGUATQBiAEIAZQBUAFkAOQBiAG8AQgBOAE8AaQBVADcATgBSAEYAOQAzAG8AVgArAFYAQQBiAGgAcAAwAHAAUgBQAFMAZQBmAEcARwBPAHEAdwBTAGcANwA3AHMAaAA5AEoASABNAHAARABNAFMAbgBrAHEAcgAyAGYARgBpAEMAUABrAHcAVgBvAHgANgBuAG4AeABGAEQAbwBXAC8AYQAxAHQAYQBaAHcAegB5AGwATABMADEAMgB3AHUAYgBtADUAdQBtAHAAcQB5AFcAYwBLAFIAagB5AGgAMgBKAFQARgBKAFcANQBnAFgARQBJADUAcAA4ADAARwB1ADIAbgB4AEwAUgBOAHcAaQB3AHIANwBXAE0AUgBBAFYASwBGAFcATQBlAFIAegBsADkAVQBxAGcALwBwAFgALwB2AGUATAB3AFMAawAyAFMAUwBIAGYAYQBLADYAagBhAG8AWQB1AG4AUgBHAHIAOABtAGIARQBvAEgAbABGADYASgBDAGEAYQBUAEIAWABCAGMAdgB1AGUAQwBKAG8AOQA4AGgAUgBBAHIARwB3ADQAKwBQAEgAZQBUAGIATgBTAEUAWABYAHoAdgBaADYAdQBXADUARQBBAGYAZABaAG0AUwA4ADgAVgBKAGMAWgBhAEYASwA3AHgAeABnADAAdwBvAG4ANwBoADAAeABDADYAWgBCADAAYwBZAGoATAByAC8ARwBlAE8AegA5AEcANABRAFUASAA5AEUAawB5ADAAZAB5AEYALwByAGUAVQAxAEkAeQBpAGEAcABwAGgATwBQADgAUwAyAHQANABCAHIAUABaAFgAVAB2AEMAMABQADcAegBPACsAZgBHAGsAeABWAG0AKwBVAGYAWgBiAFEANQA1AHMAdwBFAD0AJgBwAD0A</Device>";

        /// <summary>
        /// Returns raw xml containing various (Revision, Update, Package) IDs and info.
        /// </summary>
        /// <param name="WuCategoryID"></param>
        /// <returns></returns>
        public static async Task<string> SyncUpdatesAsync(string WuCategoryID, string MSAToken)
        {
            HttpContent httpContent = new StringContent(String.Format(GetResourceTextFile("WUIDRequest.xml"), await GetCookieAsync(), WuCategoryID, MSAToken ?? _msaToken), Encoding.UTF8, "application/soap+xml"); //Load in the Xml for this FE3 request and format it a cookie and the provided WuCategoryID.
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = Endpoints.FE3Delivery;
            httpRequest.Content = httpContent;
            httpRequest.Method = HttpMethod.Post;
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest,  new System.Threading.CancellationToken());
            string content = await httpResponse.Content.ReadAsStringAsync();
            content = HttpUtility.HtmlDecode(content);
            return content;
        }

        public static async Task<IList<PackageInstance>> GetPackageInstancesAsync(string WuCategoryID, string MSAToken)
        {
            IList<PackageInstance> PackageInstances = new List<PackageInstance>();
            HttpContent httpContent = new StringContent(String.Format(GetResourceTextFile("WUIDRequest.xml"), await GetCookieAsync(), WuCategoryID, MSAToken ?? _msaToken), Encoding.UTF8, "application/soap+xml"); //Load in the Xml for this FE3 request and format it a cookie and the provided WuCategoryID.
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = Endpoints.FE3Delivery;
            httpRequest.Content = httpContent;
            httpRequest.Method = HttpMethod.Post;
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest, new System.Threading.CancellationToken());
            string content = await httpResponse.Content.ReadAsStringAsync();
            content = HttpUtility.HtmlDecode(content);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            XmlNodeList nodes = doc.GetElementsByTagName("AppxMetadata");
            foreach(XmlNode node in nodes)
            {
                if(node.Attributes.Count >= 3)
                {
                    PackageInstance package = new PackageInstance(node.Attributes.GetNamedItem("PackageMoniker").Value, new Uri("http://test.com"), Utilities.TypeHelpers.StringToPackageType(node.Attributes.GetNamedItem("PackageType").Value));
                    PackageInstances.Add(package);
                }
            }
            return PackageInstances;

        }

        /// <summary>
        /// Gets a FE3 Cookie, required for all FE3 requests.
        /// </summary>
        /// <returns>Cookie extracted from returned XML</returns>
        public static async Task<String> GetCookieAsync() //Encrypted Cookie Data is needed for FE3 requests. It doesn't expire for a very long time but I still refresh it as the Store does. 
        {
            XmlDocument doc = new XmlDocument();
            HttpContent httpContent = new StringContent(GetResourceTextFile("GetCookie.xml"), Encoding.UTF8, "application/soap+xml");//Loading the request xml from a file to keep things nice and tidy.
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = Endpoints.FE3Delivery;
            httpRequest.Content = httpContent;
            httpRequest.Method = HttpMethod.Post;
            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest, new System.Threading.CancellationToken()); 
            doc.LoadXml(await httpResponse.Content.ReadAsStringAsync());
            XmlNodeList xmlNodeList = doc.GetElementsByTagName("EncryptedData");
            string cookie = xmlNodeList[0].InnerText;
            return cookie;
        }
        /// <summary>
        /// This function takes in the xml returned via SyncUpdatesAsync and parses out the Revision IDs, Package Names, and Update IDs. The resulting Update IDs and Revisions IDs are used for GetFileUrlsAsync.
        /// </summary>
        /// <param name="Xml"></param>
        /// <param name="RevisionIDs"></param>
        /// <param name="PackageNames"></param>
        /// <param name="UpdateIDs"></param>
        public static void ProcessUpdateIDs(string Xml, out IList<string> RevisionIDs, out IList<string> PackageNames, out IList<string> UpdateIDs)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Xml);
            UpdateIDs = new List<string>(); 
            PackageNames = new List<string>();
            RevisionIDs = new List<string>();
            XmlNodeList nodes = doc.GetElementsByTagName("SecuredFragment"); //We need to find updateIDs that actually have a File URL. Only nodes that have SecuredFragment will have an UpdateID that connects to a url. 
            foreach (XmlNode node in nodes)
            {
                string UpdateID = node.ParentNode.ParentNode.FirstChild.Attributes[0].Value; //Go from SecuredFragment to Properties to XML then down to the first child being UpdateIdentity. The first attribute is the UpdateID, second is the revisionID. 
                string RevisionID = node.ParentNode.ParentNode.FirstChild.Attributes[1].Value;
                UpdateIDs.Add(UpdateID);
                RevisionIDs.Add(RevisionID);
            }
        }
        /// <summary>
        /// Returns the Uris for the listing's packages. Each Uri will be for an appx or eappx. The blockmap is filtered out before returning the list.
        /// </summary>
        /// <param name="UpdateIDs"></param>
        /// <param name="RevisionIDs"></param>
        /// <returns>IList of App Package Download Uris</returns>
        public static async Task<IList<Uri>> GetFileUrlsAsync(IList<string> UpdateIDs, IList<string> RevisionIDs, string MSAToken)
        {
            XmlDocument doc = new XmlDocument();
            IList<Uri> uris = new List<Uri>();
            foreach (string ID in UpdateIDs)
            {
                HttpContent httpContent = new StringContent(String.Format(GetResourceTextFile("FE3FileUrl.xml"), ID, RevisionIDs[UpdateIDs.IndexOf(ID)], MSAToken ?? _msaToken), Encoding.UTF8, "application/soap+xml");//Loading the request xml from a file to keep things nice and tidy.
                HttpRequestMessage httpRequest = new HttpRequestMessage();
                httpRequest.RequestUri = Endpoints.FE3DeliverySecured;
                httpRequest.Content = httpContent;
                httpRequest.Method = HttpMethod.Post;
                HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest, new System.Threading.CancellationToken()); 
                doc.LoadXml(await httpResponse.Content.ReadAsStringAsync());
                XmlNodeList XmlUrls = doc.GetElementsByTagName("FileLocation");
                foreach (XmlNode node in XmlUrls)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "Url")
                        {
                            if (child.InnerText.Length != 99)//We need to make sure we grab the package url and not the blockmap. The blockmap will always be 99 in length. A cheap hack but it works. 
                            {
                                uris.Add(new Uri(child.InnerText));
                            }
                        }
                    }
                }
            }
            return uris;
        }

        /// <summary>
        /// Internal function used to read premade xml. 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetResourceTextFile(string filename)
        {
            string result = string.Empty;

            using (Stream stream = Assembly.GetExecutingAssembly().
                       GetManifestResourceStream("StoreLib.Xml." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }





    }

}
