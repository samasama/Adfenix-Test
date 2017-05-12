using Starcounter;
using System;

namespace Essam
{
    partial class SaleTransactionDetailsJson : Json
    {

        public string DateString;

        public SaleTransaction Save(FranchiseOffice office)
        {
            DateTime parsedDate = DateTime.MinValue;
            DateTime.TryParseExact(DateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);
            SaleTransaction newTrans = new SaleTransaction
            {
                ParentFranchiseOffice = office,
                Address = ((AddressDetailsJson)Address).Resolve(),
                TransactionDate = parsedDate,
                SalesPrice = this.SalesPrice,
                Commission = this.Commission
            };
            Transaction.Commit();
            return newTrans;
        }
    }
}
