using Starcounter;

namespace Essam
{
    partial class AddressJson : Json
    {
        public string FullAddress
        {
            get
            {
                if (Data != null)
                {
                    Address address = (Address)Data;
                    return address.House.Street.Value + " " + address.House.Value + ", " + address.ZipCode.Value + " " + address.House.Street.City.Value + ", " + address.House.Street.City.Country.Value;
                }
                return null;
            }
        }

    }
}
