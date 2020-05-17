using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace StoreLib.Models
{
    public class PackageInstance
    {
        public string PackageMoniker;
        public Uri PackageUri;
        public PackageType PackageType;

        public PackageInstance(string PackageMoniker, Uri PackageUri, PackageType packageType)
        {
            this.PackageMoniker = PackageMoniker;
            this.PackageUri = PackageUri;
            this.PackageType = packageType;
        }

        
    }
}
