using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simplified.Ring1;
using Simplified.Ring3;
namespace Essam
{
    [Database]
    public class FranchiseOffice: Address
    {
        public String FranchiseOfficeName;

        public Corporation ParentCorporation;
        public QueryResultRows<HomeSaleTransaction> HomeSaleTransactions => Db.SQL<HomeSaleTransaction>("SELECT hst FROM Essam.HomeSaleTransaction hst WHERE hst.ParentFranchiseOffice = ?", this);

        public decimal HomesTotal => Db.SQL<decimal>("SELECT Count(hst) FROM Essam.HomeSaleTransaction hst WHERE hst.ParentFranchiseOffice = ?", this).First;

        public decimal CommissionsTotal => Db.SQL<decimal>("SELECT Sum(hst.Commission) FROM Essam.HomeSaleTransaction hst WHERE hst.ParentFranchiseOffice = ?", this).First;

        public decimal CommissionsAverage => Db.SQL<decimal>("SELECT Avg(hst.Commission) FROM Essam.HomeSaleTransaction hst WHERE hst.ParentFranchiseOffice = ?", this).First;

        public decimal Trend => 1;
    }
}
