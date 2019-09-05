# StoreLib
Storelib is a DotNet core library that provides APIs to interact with the various Microsoft Store endpoints. 


## Usage:

First, you must initization of the DisplayCatalogHandler with the settings of your choice. During initization the handler can be set to us any market, locale, or endpoint.
```csharp
DisplayCatalogHandler dcathandler = new DisplayCatalogHandler(DataContracts.DCatEndpoint.Production, new DataContracts.Locale(DataContracts.Market.US, DataContracts.Lang.en, true));
```
The above snippet will create a handler that queries the production endpoint, specifiying the US/English market.

From there, the handler can query a product listing.
```csharp
await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
```

Once you have a product queried, and ensure it was found using `dcathandler.IsFound`, then you can fetch all appx, msixvcs, xvcs and eappxs for the listing using `GetPackagesForProductAsync();`

### Example:
Fetches and prints the FE3 download links for Netflix's app packages.
```csharp
DisplayCatalogHandler dcathandler = DisplayCatalogHandler.ProductionConfig();
await dcathandler.QueryDCATAsync("9wzdncrfj3tj");
foreach(Uri download in await dcathandler.GetPackagesForProductAsync())
{
  Console.WriteLine(download.ToString());
}


### Tips:

The DisplayCatalogHandler also supports querying with an auth token. (The Store supports both the MSA format and the XBL3.0 token format) This allows you to query products in other Xbox Live Sandboxes and query flighted listings.
```csharp
DisplayCatalogHandler dcathandler = DisplayCatalogHandler.ProductionConfig();
await dcathandler.QueryDCATAsync("9wzdncrfj3tj", "AuthToken");
```
