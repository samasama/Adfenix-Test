using Starcounter;
using System;

namespace Essam
{
    partial class HomeSaleTransactionDetailsJson : Json
    {
        public string FullAddress => Street + " " + House + ", " + PostalCode + " " + City + ", " + Country;

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
        public void Save()
        {
            Transaction.Commit();
        }
    }
}
