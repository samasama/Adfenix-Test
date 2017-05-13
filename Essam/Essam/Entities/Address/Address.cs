using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essam
{
    [Database]
    public class Address
    {
        public House House;
        public ZipCode ZipCode;

        public void Resolve(string country, string city, string street, string house, string zipCode)
        {
            ZipCode dbZipCode;
            House dbHouse;

            var dbCountry = Db.SQL<Country>("SELECT c FROM Essam.Country c WHERE Value = ?", country).First;
            if (dbCountry != null)
            {
                dbZipCode = Db.SQL<ZipCode>("SELECT z FROM Essam.ZipCode z WHERE Value = ? AND Country = ?", zipCode, dbCountry).First;
                if (dbZipCode == null)
                {
                    dbZipCode = new ZipCode { Country = dbCountry, Value = zipCode };
                }

                var dbCity = Db.SQL<City>("SELECT c FROM Essam.City c WHERE Value = ? AND Country = ?", city, dbCountry).First;
                if (dbCity != null)
                {
                    var dbStreet = Db.SQL<Street>("SELECT s FROM Essam.Street s WHERE Value = ? AND City = ?", street, dbCity).First;
                    if (dbStreet != null)
                    {
                        dbHouse = Db.SQL<House>("SELECT h FROM Essam.House h WHERE Value = ? AND Street = ?", house, dbStreet).First;
                        if (dbHouse == null)
                        {
                            dbHouse = new House { Value = house, Street = dbStreet };
                        }
                    }
                    else
                    {
                        dbStreet = new Street { Value = street, City = dbCity };
                        dbHouse = new House { Value = house, Street = dbStreet };
                    }
                }
                else
                {
                    dbCity = new City { Value = city, Country = dbCountry };
                    var dbStreet = new Street { Value = street, City = dbCity };
                    dbHouse = new House { Value = house, Street = dbStreet };

                }

            }
            else
            {
                dbCountry = new Country { Value = country };
                dbZipCode = new ZipCode { Country = dbCountry, Value = zipCode };
                var dbCity = new City { Value = city, Country = dbCountry };
                var dbStreet = new Street { Value = street, City = dbCity };
                dbHouse = new House { Value = house, Street = dbStreet };
            }
            House = dbHouse;
            ZipCode = dbZipCode;
        }

      

    }
}
