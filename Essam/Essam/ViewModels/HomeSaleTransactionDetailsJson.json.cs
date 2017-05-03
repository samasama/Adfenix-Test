using Starcounter;
using System;

namespace Essam
{
    partial class HomeSaleTransactionDetailsJson : Json
    {
        public string FullAddress => Street + " " + House + ", " + PostalCode + " " + City + ", " + Country;

        private string dateString;
        private DateTime parsedDate;
        public string DateString
        {
            get
            {
                if (Data != null && ((HomeSaleTransaction)Data).TransactionDate != DateTime.MinValue)
                    return ((HomeSaleTransaction)Data).TransactionDate.ToString("yyyy-MM-dd");
                return dateString;
            }
            set
            {
                dateString = value;
                if (DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate) && Data != null)
                {

                    ((HomeSaleTransaction)Data).TransactionDate = parsedDate;

                }


            }
        }
        public HomeSaleTransaction Save(FranchiseOffice office)
        {
            HomeSaleTransaction newTrans = new HomeSaleTransaction
            {
                ParentFranchiseOffice = office,
                Street = this.Street,
                House = this.House,
                PostalCode = this.PostalCode,
                City = this.City,
                Country = this.Country,
                TransactionDate = this.parsedDate,
                SalesPrice = this.SalesPrice,
                Commission = this.Commission
            };

            Transaction.Commit();
            return newTrans;
        }
    }
}
