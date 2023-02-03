using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Config
{
    public static class GlobalConstant
    {
        public static string DomainUrl = ConfigurationManager.AppSettings["DomainUrl"];
        public static string ApiDomainUrl = ConfigurationManager.AppSettings["ApiDomainUrl"];
        
    }
}