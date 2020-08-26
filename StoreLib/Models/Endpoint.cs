using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLib.Models
{
    public enum DCatEndpoint //Xbox, Production and Int are accessible on the publicly. I assume Dev is Corpnet only.
    {
        Production,
        Int,
        Xbox,
        XboxInt,
        Dev,
        OneP,
        OnePInt
    }

    public static class Endpoints //Defining these here to allow for easy updating across the codebase should any change.
    {
        public static readonly Uri FE3Delivery = new Uri("https://fe3.delivery.mp.microsoft.com/ClientWebService/client.asmx");
        public static readonly Uri FE3DeliverySecured = new Uri("https://fe3.delivery.mp.microsoft.com/ClientWebService/client.asmx/secured");
        public static readonly Uri FE3CRDelivery = new Uri("https://fe3cr.delivery.mp.microsoft.com/ClientWebService/client.asmx");
        public static readonly Uri FE3CRDeliverySecured = new Uri("https://fe3cr.delivery.mp.microsoft.com/ClientWebService/client.asmx/secured");
        public static readonly Uri DCATProd = new Uri("https://displaycatalog.mp.microsoft.com/v7.0/products/");
        public static readonly Uri DCATInt = new Uri("https://displaycatalog-int.mp.microsoft.com/v7.0/products/");
        public static readonly Uri DCATXbox = new Uri("https://xbox-displaycatalog.mp.microsoft.com/v7.0/products/");
        public static readonly Uri DCATXboxInt = new Uri("https://xbox-displaycatalog-int.mp.microsoft.com/v7.0/products");
        public static readonly Uri DCATDev = new Uri("https://displaycatalog-dev.mp.microsoft.com/v7.0/products/");
        public static readonly Uri DCATOneP = new Uri("https://displaycatalog1p.mp.microsoft.com/v7.0/products/");
        public static readonly Uri DCATOnePInt = new Uri("https://displaycatalog1p-int.mp.microsoft.com/v7.0/products/");
        public static readonly Uri DisplayCatalogSearch = new Uri("https://displaycatalog.mp.microsoft.com/v7.0/productFamilies/autosuggest?market=US&languages=en-US&query=");
        public static readonly Uri DisplayCatalogSearchInt = new Uri("https://displaycatalog-int.mp.microsoft.com/v7.0/productFamilies/autosuggest?market=US&languages=en-US&query=");    }
}
