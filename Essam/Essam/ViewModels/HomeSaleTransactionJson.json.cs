using Starcounter;
using System;

namespace Essam
{
    partial class HomeSaleTransactionJson : Json
    {
        public string FullAddress
        {
            get
            {
                if (Data != null)
                {
                    HomeSaleTransaction trans = (HomeSaleTransaction)Data;
                    return trans.Street + " " + trans.House + ", " + trans.PostalCode + " " + trans.City + ", " + trans.Country;
                }
                return null;
            }
        }

        private string dateString;
        public string DateString
        {
            get
            {
                if (Data != null && ((HomeSaleTransaction)Data).Date != DateTime.MinValue)
                    return ((HomeSaleTransaction)Data).Date.ToString("yyyy-MM-dd");
                return dateString;
            }
            set
            {
                if (Data != null)
                {

                    DateTime result;
                    if (DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out result))
                        ((HomeSaleTransaction)Data).Date = result;
                    else
                        dateString = value;
                }
                else
                {
                    dateString = value;
                }

            }
        }
    }
}
