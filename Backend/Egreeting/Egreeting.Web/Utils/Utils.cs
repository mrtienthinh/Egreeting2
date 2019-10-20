using Egreeting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Egreeting.Web.Utils
{
    public static class Utils
    {
        public static bool CheckExtentionFile(this string extention, int ecardType)
        {
            switch (ecardType)
            {
                case (int)EcardType.Picture:
                    return AcceptExtensionFile.ListAcceptPicture.Contains(extention.ToUpper());
                case (int)EcardType.Video:
                    return AcceptExtensionFile.ListAcceptVideo.Contains(extention.ToUpper());
                case (int)EcardType.GIF:
                    return AcceptExtensionFile.ListAcceptGIF.Contains(extention.ToUpper());
                default:
                    return false;
            }
        }

        public static bool CheckExtentionFile(this string extention)
        {
            return AcceptExtensionFile.ListAcceptPicture.Contains(extention.ToUpper());
        }
    }
}