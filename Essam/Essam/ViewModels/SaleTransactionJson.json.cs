using Starcounter;
using System;

namespace Essam
{
    partial class SaleTransactionJson : Json
    {
        public string FullAddress
        {
            get
            {
                if (Data != null)
                {
                    SaleTransaction trans = (SaleTransaction)Data;
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
                if (Data != null && ((SaleTransaction)Data).TransactionDate != DateTime.MinValue)
                    return ((SaleTransaction)Data).TransactionDate.ToString("yyyy-MM-dd");
                return dateString;
            }
            set
            {
                if (Data != null)
                {

                    DateTime result;
                    if (DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out result))
                        ((SaleTransaction)Data).TransactionDate = result;
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
