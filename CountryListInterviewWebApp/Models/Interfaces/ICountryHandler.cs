using CountryListInterview.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryListInterview.Models.Interfaces
{
    public interface ICountryHandler
    {
        IList<Country> GetCountries();
        Country GetCountryById(int id);
        bool AddCountry(Country country);
        bool ModifyCountry(int id, Country country);
        bool DeleteCountryById(int id);
        bool XmlFileExists(string file);
        bool GenerateXmlFile();
    }
}
