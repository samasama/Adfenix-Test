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

        public static Address Resolve(string country, string city, string street, string house, string zipCode)
        {
            var dbCountry = Db.SQL<Country>("SELECT c FROM Country c WHERE Value = ?", country).First;
            if (dbCountry != null)
            {
                var dbZipCode = Db.SQL<ZipCode>("SELECT z FROM ZipCode z WHERE Value = ? AND Country = ?", zipCode, dbCountry).First;
                if (dbZipCode == null)
                {
                    dbZipCode = new ZipCode { Country = dbCountry, Value = zipCode };
                }

                var dbCity = Db.SQL<City>("SELECT c FROM City c WHERE Value = ? AND Country = ?", city, dbCountry).First;
                if (dbCity != null)
                {
                    var dbStreet = Db.SQL<Street>("SELECT s FROM Street s WHERE Value = ? AND City = ?", street, dbCity).First;
                    if (dbStreet != null)
                    {
                        var dbHouse = Db.SQL<House>("SELECT h FROM House h WHERE Value = ? AND Street = ?", house, dbStreet).First;
                        if (dbHouse != null)
                        {
                            var dbAddress = Db.SQL<Address>("SELECT a FROM Address a WHERE ZipCode = ? AND House = ?", dbZipCode, dbHouse).First;
                            if (dbAddress != null)
                            {
                                return dbAddress;
                            }
                            else
                            {
                                dbAddress = new Address { House = dbHouse, ZipCode = dbZipCode };
                                return dbAddress;
                            }
                        }
                        else
                        {
                            dbHouse = new House { Value = house, Street = dbStreet };
                            var dbAddress = new Address { House = dbHouse, ZipCode = dbZipCode };
                            return dbAddress;
                        }
                    }
                    else
                    {
                        dbStreet = new Street { Value = street, City = dbCity };
                        var dbHouse = new House { Value = house, Street = dbStreet };
                        var dbAddress = new Address { House = dbHouse, ZipCode = dbZipCode };
                        return dbAddress;
                    }
                }
                else
                {
                    dbCity = new City { Value = city, Country = dbCountry };
                    var dbStreet = new Street { Value = street, City = dbCity };
                    var dbHouse = new House { Value = house, Street = dbStreet };
                    var dbAddress = new Address { House = dbHouse, ZipCode = dbZipCode };
                    return dbAddress;
                }

            }
            else
            {
                dbCountry = new Country { Value = country };
                var dbZipCode = new ZipCode { Country = dbCountry, Value = zipCode };
                var dbCity = new City { Value = city, Country = dbCountry };
                var dbStreet = new Street { Value = street, City = dbCity };
                var dbHouse = new House { Value = house, Street = dbStreet };
                var dbAddress = new Address { House = dbHouse, ZipCode = dbZipCode };
                return dbAddress;
            }
        }
    }
}
