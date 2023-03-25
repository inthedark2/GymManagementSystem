using System;

namespace MS.Common.Services
{
    public static class PathHelper
    {
        public static string GetVirtualPath(string physicalPath)
        {
            if (string.IsNullOrEmpty(physicalPath))
            {
                return "";
            }

            if (!physicalPath.StartsWith(System.Web.HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("Physical path is not within the application root");
            }

            return "~/" + physicalPath.Substring(System.Web.HttpContext.Current.Request.PhysicalApplicationPath.Length)
                  .Replace("\\", "/");
        }
    }
}
