using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essam
{
    [Database]
    public class FranchiseOffice:Address
    {
        public String Name;

        public Corporation ParentCorporation;
        public QueryResultRows<SaleTransaction> SaleTransactions => Db.SQL<SaleTransaction>("SELECT hst FROM SaleTransaction hst WHERE hst.ParentFranchiseOffice = ? ORDER BY hst.TransactionDate DESC", this);

        public long HomesTotal => Db.SQL<long>("SELECT Count(hst) FROM SaleTransaction hst WHERE hst.ParentFranchiseOffice = ?", this).First;

        public long CommissionsTotal => Db.SQL<long>("SELECT Sum(hst.Commission) FROM SaleTransaction hst WHERE hst.ParentFranchiseOffice = ?", this).First;

        public long CommissionsAverage
        {
            get
            {
                if (HomesTotal > 0)
                    return CommissionsTotal / HomesTotal;
                else return 0;
            }
        }
        public long Trend
        {
            get
            {
                DateTime today = DateTime.Now;
                DateTime quarterStart = new DateTime(today.Year, (((today.Month - 1) / 3) * 3) + 1, 1);

                long lastYearSales = Db.SQL<long>("SELECT Sum(hst.Commission) FROM SaleTransaction hst WHERE hst.ParentFranchiseOffice = ? AND hst.TransactionDate >= ? and hst.TransactionDate <= ?", this, quarterStart.AddYears(-1), today.AddYears(-1)).First;
                long thisYearSales = Db.SQL<long>("SELECT Sum(hst.Commission) FROM SaleTransaction hst WHERE hst.ParentFranchiseOffice = ? AND hst.TransactionDate >= ? and hst.TransactionDate <= ?", this, quarterStart, today).First;
                if (thisYearSales > 0)
                    return (thisYearSales * 100) / lastYearSales;
                return 0;
            }
        }
    }
}
