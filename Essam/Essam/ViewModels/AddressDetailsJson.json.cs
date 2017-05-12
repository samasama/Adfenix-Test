using Starcounter;

namespace Essam
{
    partial class AddressDetailsJson : Json
    {
        public string FullAddress => StreetValue + " " + HouseValue + ", " + ZipCodeValue + " " + CityValue + ", " + CountryValue;

        private string streetValue = null;
        public string StreetValue
        {
            get
            {
                if (streetValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    streetValue = address.House.Street.Value;
                }

                return streetValue;

            }
            set
            {
                streetValue = value;
            }
        }

        private string houseValue = null;
        public string HouseValue
        {
            get
            {
                if (houseValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    houseValue = address.House.Value;
                }

                return houseValue;

            }
            set
            {
                houseValue = value;
            }
        }

        private string cityValue = null;
        public string CityValue
        {
            get
            {
                if (cityValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    cityValue = address.House.Street.City.Value;
                }

                return cityValue;

            }
            set
            {
                cityValue = value;
            }
        }


        private string countryValue = null;
        public string CountryValue
        {
            get
            {
                if (countryValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    countryValue = address.House.Street.City.Country.Value;
                }

                return countryValue;

            }
            set
            {
                countryValue = value;
            }
        }

        private string zipCodeValue = null;
        public string ZipCodeValue
        {
            get
            {
                if (zipCodeValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    zipCodeValue = address.ZipCode.Value;
                }

                return zipCodeValue;

            }
            set
            {
                zipCodeValue = value;
            }
        }


        public Address Resolve()
        {
            Address address = Address.Resolve(CountryValue, CityValue, StreetValue, HouseValue, ZipCodeValue);
            return address;
        }
    }
}
