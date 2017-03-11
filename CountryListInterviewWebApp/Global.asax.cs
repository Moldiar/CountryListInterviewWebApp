using CountryListInterview.Models.Interfaces;
using CountryListInterview.Models.Types;
using CountryListInterviewWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CountryListInterviewWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ICountryHandler countryHandler = new CountryHandler();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            countryHandler.GenerateXmlFile();
            initXml();
        }
        //this part is for testing purposes only
        private void initXml()
        {
            Country country = new Country();

            country.Name = "Poland";
            country.Capitol = "Warsaw";

            countryHandler.AddCountry(country);

            country.Name = "Germany";
            country.Capitol = "Berlin";

            countryHandler.AddCountry(country);

            country.Name = "Great Britain";
            country.Capitol = "London";

            countryHandler.AddCountry(country);

            countryHandler.DeleteCountryById(2);

            country.Name = "Ireland";
            country.Capitol = "Belfast";

            countryHandler.ModifyCountry(3, country);
        }
    }
}
