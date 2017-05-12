using Starcounter;
using System.Collections.Generic;

namespace Essam
{
    partial class FranchiseOfficeDetailsJson : Json
    {
        public string FullAddress => Street + " " + House + ", " + PostalCode + " " + City + ", " + Country;
      
        
        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
        }

        public void AddSaleTransaction(SaleTransaction trans)
        {
            var transJson = new SaleTransactionJson()
            {
                Data = trans
            };
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
