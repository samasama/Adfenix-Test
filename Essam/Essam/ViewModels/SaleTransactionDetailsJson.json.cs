using Starcounter;
using System;

namespace Essam
{
    partial class SaleTransactionDetailsJson : Json
    {
        public string FullAddress => Street + " " + House + ", " + PostalCode + " " + City + ", " + Country;

        private string dateString;
        private DateTime parsedDate;
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
                dateString = value;
                if (DateTime.TryParseExact(value, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate) && Data != null)
                {

                    ((SaleTransaction)Data).TransactionDate = parsedDate;

                }


            }
        }
        public SaleTransaction Save(FranchiseOffice office)
        {
            SaleTransaction newTrans = new SaleTransaction
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
