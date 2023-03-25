using MS.Common.Constans;
using MS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.WebSite.Controllers
{
    public partial class BaseController : Controller
    {
        public int LanguageId { get; set; }


        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var language = filterContext.RouteData.Values[CommonConstants.Lang].ToString();
        //    ViewBag.Language = language.ToLower();
        //    using(ManagmentSystemContext context = new ManagmentSystemContext())
        //    {
        //        LanguageId = 2;//context.Languages.FirstOrDefault(x => x.Code == language).LanguageId;
        //    }
        //}
    }
}