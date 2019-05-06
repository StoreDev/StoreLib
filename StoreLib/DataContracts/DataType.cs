using System;
using System.Collections.Generic;
using System.Text;

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
namespace StoreLib.DataContracts
{
    public enum IdentiferType
    {
        ProductID,
        XboxTitleID,
        PackageFamilyName,
        ContentID,
        LegacyWindowsPhoneProductID,
        LegacyWindowsStoreProductID,
        LegacyXboxProductID
    }

    public enum ImagePurpose
    {
        Logo,
        Tile,
        Screenshot,
        BoxArt,
        BrandedKeyArt,
        Poster,
        FeaturePromotionalSquareArt,
        ImageGallery,
        SuperHeroArt,
        TitledHeroArt
    }

    public enum ProductKind
    {
        Game,
        Application,
        Book,
        Movie,
        Physical,
        Software
    }

    public enum DeviceFamily
    {
        Desktop,
        Mobile,
        Xbox,
        ServerCore,
        IotCore,
        HoloLens,
        Andromeda,
        Universal,
        WCOS
    }

    public enum DisplayCatalogResult
    {
        NotFound,
        Restricted,
        TimedOut, 
        Error,
        Found
    }
}
