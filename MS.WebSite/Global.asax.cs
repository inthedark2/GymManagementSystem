using MS.WebSite.Scheduler;

namespace MS.WebSite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.ConfigureContainer(new[] { typeof(MvcApplication).Assembly });
            var quartzProvider = new QuartzScheduler();
            quartzProvider.Run();
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string langCode = null;

            if (this.Request.RequestContext.RouteData.Values.ContainsKey("lang"))
            {
                if (string.IsNullOrEmpty(this.Request.RequestContext.RouteData.Values["lang"].ToString()))
                {
                    Request.RequestContext.RouteData.Values["lang"] = ConfigurationManager.AppSettings["DefaultTemplateLanguage"];
                }
                langCode = this.Request.RequestContext.RouteData.Values["lang"].ToString();
            }
            //language evaluation via query of ASP.NET standart error page url
            else if (this.Request.QueryString.AllKeys.Contains("aspxerrorpath"))
            {
                var aspxErrorPath = this.Request.QueryString["aspxerrorpath"];
                string langCodeCandidate = null;

                try
                {
                    langCodeCandidate = aspxErrorPath.Split('/')[1].ToLower();
                }
                catch (Exception)
                {
                    langCodeCandidate = null;
                }

                if (langCodeCandidate != null)
                {
                    foreach (var cn in LocalizationManager.AllCultureNames)
                    {
                        if (cn == langCodeCandidate)
                        {
                            langCode = langCodeCandidate;
                            break;
                        }
                    }
                }
            }
            if (langCode == null)
            {
                return;
            }
            LocalizationManager.ChangeCulture(langCode);
        }
    }
}
