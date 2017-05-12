using Starcounter;
using System.Collections.Generic;

namespace Essam
{
    partial class FranchiseOfficeDetailsJson : Json
    {
        void Handle(Input.SaveTrigger action)
        {
            AddressDetailsJson addressJson = ((AddressDetailsJson)Address);
            Address address = addressJson.Resolve();
            ((FranchiseOffice)Data).Address = address;
            Transaction.Commit();
        }

        public void AddSaleTransaction(SaleTransaction trans)
        {
            var transJson = (SaleTransactionJson)Self.GET("/Essam/partials/transaction/" + trans.GetObjectID());
            this.SaleTransactions.Add(transJson);
        }
        public void RefreshSaleTransactions(IEnumerable<SaleTransaction> transactions)
        {
            this.SaleTransactions.Clear();
            foreach (SaleTransaction trans in transactions)
            {
                AddSaleTransaction(trans);
            }
        }

        void Handle(Input.SaveTransactionTrigger action)
        {
            SaleTransactionDetailsJson transJson = ((SaleTransactionDetailsJson)SaleTransactionNew);
            SaleTransaction newTrans = transJson.Save((FranchiseOffice)Data);
            AddSaleTransaction(newTrans);
        }
    }
}
