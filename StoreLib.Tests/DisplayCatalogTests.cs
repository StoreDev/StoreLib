using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreLib;
using StoreLib.Services;
using StoreLib.Models;
using StoreLib.DataContracts;
using System.IO;

namespace StoreLib.Tests
{
    [TestClass]
    public class DisplayCatalogTests
    {
        public TestContext TestContext { get; set; }
        [TestMethod]
        public async Task QueryNetflix()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new DataContracts.Locale(DataContracts.Market.US, DataContracts.Lang.en, true));
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            if (dcathandler.IsFound)
            {
                Assert.AreEqual(dcathandler.ProductListing.Product.LocalizedProperties[0].ProductTitle, "Netflix");
            }
            if (!dcathandler.IsFound)
            {
                Assert.Fail(dcathandler.Error.Message);
            }
            
        }
        [TestMethod]
        public async Task QueryNetflixProdConfig()
        {
            DisplayCatalogHandler dcathandler = DisplayCatalogHandler.ProductionConfig();
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            if (dcathandler.IsFound)
            {
                Assert.AreEqual(dcathandler.ProductListing.Product.LocalizedProperties[0].ProductTitle, "Netflix");
            }
            if (!dcathandler.IsFound)
            {
                Assert.Fail(dcathandler.Error.Message);
            }

        }
        [TestMethod]
        public async Task QueryNetflixUsingContentId()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new DataContracts.Locale(DataContracts.Market.US, DataContracts.Lang.en, true));
            await dcathandler.QueryDCATAsync("2B7C6B9F-FC0D-41D2-8FD2-8DFB658EC16F", DataContracts.IdentiferType.ContentID);
            TestContext.WriteLine(dcathandler.ProductListing.Product.ProductId);
            if (!dcathandler.IsFound)
            {
                TestContext.WriteLine(dcathandler.ProductListing.Product.ProductId);
                Assert.Fail(dcathandler.Error.Message);
            }

        }
        [TestMethod]
        public async Task QueryNetflixUsingPackageFamilyName()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new DataContracts.Locale(DataContracts.Market.US, DataContracts.Lang.en, true));
            await dcathandler.QueryDCATAsync("Microsoft.SoDTest_8wekyb3d8bbwe", DataContracts.IdentiferType.PackageFamilyName);
            if (!dcathandler.IsFound)
            {
                Assert.Fail(dcathandler.Error.Message);
            }
        }
        [TestMethod]
        public async Task QueryNetflixInt()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Int, new DataContracts.Locale(DataContracts.Market.US, DataContracts.Lang.en, true));
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            if (dcathandler.IsFound)
            {
                Assert.AreEqual(dcathandler.ProductListing.Product.LocalizedProperties[0].ProductTitle, "Netflix");
            }
            if (!dcathandler.IsFound)
            {
                Assert.Fail(dcathandler.Error.Message);
            }

        }

        [TestMethod]
        public async Task SearchDesktop()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DCatEndpoint.Production, new Locale(Market.US, Lang.en, true));
            DCatSearch search = await dcathandler.SearchDCATAsync("Netflix", DeviceFamily.Desktop);
            if(search.Results[0].Products[0].Title != "Netflix")
            {
                Assert.Fail($"Netflix was not found. Result Count: {search.TotalResultCount}");
            }

        }
        [TestMethod]
        public async Task SearchXbox()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DCatEndpoint.Production, new Locale(Market.US, Lang.en, true));
            DCatSearch search = await dcathandler.SearchDCATAsync("Halo 5", DeviceFamily.Xbox);
            if (search.Results[0].Products[0].Title != "Halo 5: Guardians")
            {
                Assert.Fail($"Halo 5: Guardians was not found. Result Count: {search.TotalResultCount}");
            }

        }

        [TestMethod]
        public async Task GetSuperHeroArtForNetflix()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new Locale(Market.US, Lang.en, true));
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            if (dcathandler.IsFound)
            {
                Uri SuperHeroArt = StoreLib.Utilities.ImageHelpers.GetImageUri(DataContracts.ImagePurpose.SuperHeroArt, dcathandler.ProductListing);
                if (SuperHeroArt == null)
                {
                    Assert.Fail("Failed to get SuperHeroArt for Netflix!");
                }
            }
        }
        /*
        [TestMethod(), Timeout(TestTimeout.Infinite)] //This can take quite a while to complete, the test will automatically fail with TaskCanceledException after 2 minutes if we don't set our own timeout. 
        public async Task QueryLargeListProducts()
        {
            foreach(string line in File.ReadLines("products.txt"))
            {
                DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.IdentiferType.ProductID, DataContracts.DCatEndpoint.Production, new Locale(Market.US, Lang.en, true));
                await dcathandler.QueryDCATAsync(line);
                if (!dcathandler.IsFound)
                {
                    Assert.Inconclusive($"Failed to find ID: {line}, Error: {dcathandler.Error.Message}");
                }

            }

        }
        */
        [TestMethod]
        public async Task RandomLocale()
        {
            Array Markets = Enum.GetValues(typeof(Market));
            Array Langs = Enum.GetValues(typeof(Lang));
            Random ran = new Random();
            Market RandomMarket = (Market)Markets.GetValue(ran.Next(Markets.Length));
            Lang RandomLang = (Lang)Langs.GetValue(ran.Next(Langs.Length));
            TestContext.WriteLine($"RandomLocale: Testing with {RandomMarket}-{RandomLang}");
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new Locale(RandomMarket, RandomLang, true));
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            if (dcathandler.IsFound)
            {
                Assert.AreEqual(dcathandler.ProductListing.Product.LocalizedProperties[0].ProductTitle, "Netflix");
            }
            if (!dcathandler.IsFound)
            {
                Assert.Fail(dcathandler.Error.Message); 
            }
        }
        [TestMethod]
        public async Task CacheImage()
        {
            DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new Locale(Market.US, Lang.en, true));
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            if (dcathandler.IsFound)
            {
                Uri SuperHeroArt = StoreLib.Utilities.ImageHelpers.GetImageUri(DataContracts.ImagePurpose.SuperHeroArt, dcathandler.ProductListing);
                byte[] imagetest = await Utilities.ImageHelpers.CacheImageAsync(SuperHeroArt, Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), false); //The ExecutingAssembly path is only being used for this unit test, in an actual program, you would want to save to the temp. 
                if(imagetest == null)
                {
                    Assert.Fail();
                }
            }


        }

        [TestMethod]
        public async Task GetFiles()
        {
            DisplayCatalogHandler dcathandler = DisplayCatalogHandler.ProductionConfig();
            await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
            foreach(Uri download in await dcathandler.GetPackagesForProduct())
            {
                TestContext.WriteLine(download.ToString());
            }

        }

       
    }
}
