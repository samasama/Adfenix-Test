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
              
                TransactionDate = parsedDate,
                SalesPrice = this.SalesPrice,
                Commission = this.Commission
            };
            ((AddressDetailsJson)Address).Resolve(newTrans);
            Transaction.Commit();
            return newTrans;
        }
    }
}
