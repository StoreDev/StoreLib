using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.XPath;
using Newtonsoft.Json;

namespace StoreLib.Models
{
    public class PackageInstance
    {
        public string PackageMoniker;
        public Uri PackageUri;
        public PackageType PackageType;
        public ApplicabilityBlob ApplicabilityBlob;

        public PackageInstance(string PackageMoniker, Uri PackageUri, PackageType PackageType, ApplicabilityBlob ApplicabilityBlob)
        {
            this.PackageMoniker = PackageMoniker;
            this.PackageUri = PackageUri;
            this.PackageType = PackageType;
            this.ApplicabilityBlob = ApplicabilityBlob;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ContentTargetPlatform
    {
        [JsonProperty("platform.maxVersionTested")]
        public long PlatformMaxVersionTested { get; set; }

        [JsonProperty("platform.minVersion")]
        public long PlatformMinVersion { get; set; }

        [JsonProperty("platform.target")]
        public int PlatformTarget { get; set; }
    }

    public class Policy
    {
        [JsonProperty("category.first")]
        public string CategoryFirst { get; set; }

        [JsonProperty("category.second")]
        public string CategorySecond { get; set; }

        [JsonProperty("category.third")]
        public string CategoryThird { get; set; }

        [JsonProperty("optOut.backupRestore")]
        public bool OptOutBackupRestore { get; set; }

        [JsonProperty("optOut.removeableMedia")]
        public bool OptOutRemoveableMedia { get; set; }
    }

    public class ThirdPartyAppRating
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("systemId")]
        public int SystemId { get; set; }
    }

    public class Policy2
    {
        [JsonProperty("ageRating")]
        public int AgeRating { get; set; }

        [JsonProperty("optOut.DVR")]
        public bool OptOutDVR { get; set; }

        [JsonProperty("thirdPartyAppRatings")]
        public List<ThirdPartyAppRating> ThirdPartyAppRatings { get; set; }
    }

    public class ApplicabilityBlob
    {
        [JsonProperty("blob.version")]
        public long BlobVersion { get; set; }

        [JsonProperty("content.isMain")]
        public bool ContentIsMain { get; set; }

        [JsonProperty("content.packageId")]
        public string ContentPackageId { get; set; }

        [JsonProperty("content.productId")]
        public string ContentProductId { get; set; }

        [JsonProperty("content.targetPlatforms")]
        public List<ContentTargetPlatform> ContentTargetPlatforms { get; set; }

        [JsonProperty("content.type")]
        public int ContentType { get; set; }

        [JsonProperty("policy")]
        public Policy Policy { get; set; }

        [JsonProperty("policy2")]
        public Policy2 Policy2 { get; set; }
    }
}
