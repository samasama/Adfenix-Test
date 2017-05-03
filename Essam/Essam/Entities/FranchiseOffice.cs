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
        public Corporation ParentCorporation;
        public QueryResultRows<HomeSaleTransaction> HomeSaleTransactions => Db.SQL<HomeSaleTransaction>("SELECT hst FROM Essam.HomeSaleTransaction WHERE hst.ParentFranchiseOffice = ?", this);
    }
}
