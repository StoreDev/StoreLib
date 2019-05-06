using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreLib.Models;
using StoreLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Tests
{
    [TestClass]
    public class FE3Tests
    {
        public TestContext TestContext { get; set; }
        [TestMethod, Timeout(20000)]
        public async Task GetPackagesForNetflix()
        {
            DisplayCatalogHandler displayCatalog = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new DataContracts.Locale(DataContracts.Market.US, DataContracts.Lang.en, true));
            await displayCatalog.QueryDCATAsync("9wzdncrfj3tj");
            if (displayCatalog.IsFound)
            {
                string xml = await FE3Handler.SyncUpdatesAsync(displayCatalog.ProductListing.Product.DisplaySkuAvailabilities[0].Sku.Properties.FulfillmentData.WuCategoryId);
                IList<string> RevisionIds = new List<string>();
                IList<string> PackageNames = new List<string>();
                IList<string> UpdateIDs = new List<string>();
                FE3Handler.ProcessUpdateIDs(xml, out RevisionIds, out PackageNames, out UpdateIDs);
                IList<Uri> FileUris = await FE3Handler.GetFileUrlsAsync(UpdateIDs, RevisionIds);
                foreach (Uri fileuri in FileUris)
                {
                    TestContext.WriteLine($"GetPackagesForNetflix: {fileuri}");
                }
            }
            else
            {
                Assert.Fail(displayCatalog.Error.Message);
            }
        }

        [TestMethod, Timeout(20000)]
        public async Task GetPackagesAndNamesForNetflix()
        {
            DisplayCatalogHandler displayCatalog = DisplayCatalogHandler.ProductionConfig();
            await displayCatalog.QueryDCATAsync("9wzdncrfj3tj");
            if (displayCatalog.IsFound)
            {
                string xml = await FE3Handler.SyncUpdatesAsync(displayCatalog.ProductListing.Product.DisplaySkuAvailabilities[0].Sku.Properties.FulfillmentData.WuCategoryId);
                IList<string> RevisionIds = new List<string>();
                IList<string> PackageNames = new List<string>();
                IList<string> UpdateIDs = new List<string>();
                FE3Handler.ProcessUpdateIDs(xml, out RevisionIds, out PackageNames, out UpdateIDs);
                IList<Uri> FileUris = await FE3Handler.GetFileUrlsAsync(UpdateIDs, RevisionIds);
                /*
                if(FileUris.Count > 0)
                {
                    TestContext.WriteLine($"FileURI Count: {FileUris.Count}");
                    foreach (Uri fileuri in FileUris)
                    {
                        TestContext.WriteLine($"GetPackagesForNetflix: {PackageNames[FileUris.IndexOf(fileuri)]} : {fileuri}");
                    }
                }
               */
            }
            else
            {
                Assert.Fail(displayCatalog.Error.Message);
            }
        }

        [TestMethod]
        public async Task GetCookie()
        {
            string cookie = await FE3Handler.GetCookieAsync();
            if(cookie == null)
            {
                Assert.Fail($"GetCookie: Cookie was null");
            }
        }
    }
}
