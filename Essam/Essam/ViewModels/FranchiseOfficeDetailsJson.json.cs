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

        public void AddHomeSaleTransaction(HomeSaleTransaction trans)
        {
            var transJson = new HomeSaleTransactionJson()
            {
                Data = trans
            };
            this.HomeSaleTransactions.Add(transJson);
        }
        public void RefreshHomeSaleTransactions(IEnumerable<HomeSaleTransaction> transactions)
        {
            this.HomeSaleTransactions.Clear();
            foreach (HomeSaleTransaction trans in transactions)
            {
                AddHomeSaleTransaction(trans);
            }
        }

        void Handle(Input.SaveTransactionTrigger action)
        {
            HomeSaleTransactionDetailsJson transJson = ((HomeSaleTransactionDetailsJson)HomeSaleTransactionNew);
            HomeSaleTransaction newTrans = transJson.Save((FranchiseOffice)Data);
            AddHomeSaleTransaction(newTrans);
        }
    }
}
