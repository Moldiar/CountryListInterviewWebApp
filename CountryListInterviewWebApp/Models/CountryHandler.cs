using CountryListInterview.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CountryListInterview.Models.Types;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace CountryListInterviewWebApp.Models
{
    public class CountryHandler : ICountryHandler
    {
        public bool AddCountry(Country country)
        {
            var file = HttpContext.Current.Server.MapPath(Settings.CountryFile);
            var doc = XDocument.Load(file);

            string id = doc.Element("Root").Element("NextId").Value;

            XElement root = doc.Element("Root").Element("Countries");

            XElement countryNode = new XElement("Country");
            countryNode.SetAttributeValue("Id", id);
            countryNode.Add(new XElement("Name", country.Name));
            countryNode.Add(new XElement("Capitol", country.Capitol));

            root.Add(countryNode);

            doc.Element("Root").Element("NextId").Value = (int.Parse(id) + 1).ToString();

            doc.Save(file);

            return true;
        }

        public bool DeleteCountryById(int id)
        {
            var file = HttpContext.Current.Server.MapPath(Settings.CountryFile);
            var doc = XDocument.Load(file);

            doc.Descendants("Root").Descendants("Countries").Descendants("Country").Where(x => (string)x.Attribute("Id") == id.ToString()).Remove();

            doc.Save(file);

            return true;
        }

        public bool GenerateXmlFile()
        {
            var file = HttpContext.Current.Server.MapPath(Settings.CountryFile);

            if (XmlFileExists(file))
            {
                File.Delete(file);
                //return false;
            }
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement(string.Empty, "Root", string.Empty);
            doc.AppendChild(root);
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement nextId = doc.CreateElement(string.Empty, "NextId", string.Empty);
            nextId.InnerText = "1";
            root.AppendChild(nextId);

            XmlElement countriesRoot = doc.CreateElement(string.Empty, "Countries", string.Empty);
            root.AppendChild(countriesRoot);

            doc.Save(file);
            return true;
        }

        public IList<Country> GetCountries()
        {
            var file = HttpContext.Current.Server.MapPath(Settings.CountryFile);
            var doc = XDocument.Load(file);

            var entries = doc.Descendants("Root").Descendants("Countries").Elements("Country");
            var countries = new List<Country>();

            foreach (var entry in entries) { 
            var country = new Country
            {
                Id = int.Parse(entry.Attribute("Id").Value),
                Name = entry.Element("Name").Value,
                Capitol = entry.Element("Capitol").Value,
            };
                countries.Add(country);
        }
            return countries;
        }

        public Country GetCountryById(int id)
        {
            var file = HttpContext.Current.Server.MapPath(Settings.CountryFile);
            var doc = XDocument.Load(file);

            var entry = doc.Descendants("Root").Descendants("Countries").Descendants("Country").Where(x => (string)x.Attribute("Id") == id.ToString()).FirstOrDefault();

            var country = new Country
            {
                Id = int.Parse(entry.Attribute("Id").Value),
                Name = entry.Element("Name").Value,
                Capitol = entry.Element("Capitol").Value,
            };

            return country;
        }

        public bool ModifyCountry(int id, Country country)
        {
            var file = HttpContext.Current.Server.MapPath(Settings.CountryFile);
            var doc = XDocument.Load(file);

            var entry = doc.Descendants("Root").Descendants("Countries").Descendants("Country").Where(x => (string)x.Attribute("Id") == id.ToString()).FirstOrDefault();

            entry.Element("Name").Value = country.Name;
            entry.Element("Capitol").Value = country.Capitol;

            doc.Save(file);

            return true;
        }

        public bool XmlFileExists(string file)
        {
            return File.Exists(file);
        }
    }
}