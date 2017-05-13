using Starcounter;

namespace Essam
{
    partial class AddressDetailsJson : Json
    {
        public string FullAddress => StreetValue + " " + HouseValue + ", " + ZipCodeValue + " " + CityValue + ", " + CountryValue;

       
        private string houseValue = null;
        public string HouseValue
        {
            get
            {
                if (houseValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    houseValue = address.House != null? address.House.Value: null;
                }

                return houseValue;

            }
            set
            {
                houseValue = value;
            }
        }
        private string streetValue = null;
        public string StreetValue
        {
            get
            {
                if (streetValue == null && Data != null)
                {
                    Address address = (Address)Data;
                    streetValue = address.House != null && address.House.Street != null ? address.House.Street.Value : null;
                }

                return streetValue;

            }
            set
            {
                streetValue = value;
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
                    cityValue = address.House != null && address.House.Street != null && address.House.Street.City != null? address.House.Street.City.Value:null;
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
                    countryValue = address.House != null && address.House.Street != null && address.House.Street.City != null && address.House.Street.City.Country != null? address.House.Street.City.Country.Value:null;
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
                    zipCodeValue = address.ZipCode != null? address.ZipCode.Value:null;
                }

                return zipCodeValue;

            }
            set
            {
                zipCodeValue = value;
            }
        }


        public void Resolve(Address address)
        {
            address.Resolve(CountryValue, CityValue, StreetValue, HouseValue, ZipCodeValue);
        }
    }
}
