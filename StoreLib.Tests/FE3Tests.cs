using StoreLib.Models;
using StoreLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace StoreLib.Tests
{
    public class FE3Tests
    {
        private readonly ITestOutputHelper _output;
        public FE3Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Timeout=20000)]
        public async Task GetPackagesForNetflix()
        {
            DisplayCatalogHandler displayCatalog = new DisplayCatalogHandler(DCatEndpoint.Production, new Locale(Market.US, Lang.en, true));
            await displayCatalog.QueryDCATAsync("9wzdncrfj3tj");

            Assert.True(displayCatalog.IsFound);

            string xml = await FE3Handler.SyncUpdatesAsync(displayCatalog.ProductListing.Product.DisplaySkuAvailabilities[0].Sku.Properties.FulfillmentData.WuCategoryId);
            IList<string> RevisionIds = new List<string>();
            IList<string> PackageNames = new List<string>();
            IList<string> UpdateIDs = new List<string>();
            FE3Handler.ProcessUpdateIDs(xml, out RevisionIds, out PackageNames, out UpdateIDs);
            IList<Uri> FileUris = await FE3Handler.GetFileUrlsAsync(UpdateIDs, RevisionIds);
            foreach (Uri fileuri in FileUris)
            {
                _output.WriteLine($"GetPackagesForNetflix: {fileuri}");
            }
        }

        [Fact(Timeout=20000)]
        public async Task GetPackagesAndNamesForNetflix()
        {
            DisplayCatalogHandler displayCatalog = DisplayCatalogHandler.ProductionConfig();
            await displayCatalog.QueryDCATAsync("9wzdncrfj3tj");

            Assert.True(displayCatalog.IsFound);

            string xml = await FE3Handler.SyncUpdatesAsync(displayCatalog.ProductListing.Product.DisplaySkuAvailabilities[0].Sku.Properties.FulfillmentData.WuCategoryId);
            IList<string> RevisionIds = new List<string>();
            IList<string> PackageNames = new List<string>();
            IList<string> UpdateIDs = new List<string>();
            FE3Handler.ProcessUpdateIDs(xml, out RevisionIds, out PackageNames, out UpdateIDs);
            IList<Uri> FileUris = await FE3Handler.GetFileUrlsAsync(UpdateIDs, RevisionIds);
        }

        [Fact]
        public async Task GetCookie()
        {
            string cookie = await FE3Handler.GetCookieAsync();

            Assert.NotNull(cookie);
        }
    }
}
