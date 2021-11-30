using System;
using System.Collections.Generic;

namespace intproj
{
    public class Country
    {
        public int Depth;
        public string Code;
        public string Name;
        public List<String> Borders;

        public Country(int depth, string code, string name, List<String> borders)
        {
            this.Depth = depth;
            this.Code = code;
            this.Name = name;
            this.Borders = borders;
        }
    }

    public class Countries
    {

        //   CAN
        //   USA
        //   MEX -> BLZ
        //   GTM -> BLZ
        // SLV | HND
        //     | NIC
        //     | CRI
        //     | PAN

        private static List<Country> _countries = new List<Country>() {
            new Country(1, "CAN", "Canada",         new List<String>() { "USA" }),
            new Country(0, "USA", "United States" , new List<String>() { "CAN", "MEX" }),
            new Country(1, "MEX", "Mexico",         new List<String>() { "USA", "BLZ", "GTM" }),
            new Country(2, "GTM", "Guatemala",      new List<String>() { "MEX", "BLZ", "SLV", "HND" }),
            new Country(2, "BLZ", "Belize",         new List<String>() { "MEX", "GTM" }),
            new Country(3, "SLV", "El Salvador",    new List<String>() { "GTM", "HND" }),
            new Country(3, "HND", "Honduras",       new List<String>() { "GTM", "SLV", "NIC" }),
            new Country(4, "NIC", "Nicaragua",      new List<String>() { "HND", "CRI" }),
            new Country(5, "CRI", "Costa Rica",     new List<String>() { "NIC", "PAN" }),
            new Country(6, "PAN", "Panama",         new List<String>() { "CRI" }),
        };  // ideally we would retrieve data from a database

        public static bool IsAvailable(string countryCode) => _countries.Exists(x => x.Code == countryCode);

        public static Country GetCountry(string countryCode) => _countries.Find(x => x.Code == countryCode);

        public static List<Country> GetCountries(int depth) => _countries.FindAll(x => x.Depth == depth);

        public static List<Country> GetCountries(string border) => _countries.FindAll(x => x.Borders.Contains(border));

        public static List<Country> GetCountries(int depth, string border) => _countries.FindAll(x => x.Depth == depth && x.Borders.Contains(border));

    }
}

