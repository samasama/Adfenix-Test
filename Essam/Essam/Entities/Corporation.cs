using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
using Simplified.Ring1;
namespace Essam
{
    [Database]
    public class Corporation:Something
    {
        public QueryResultRows<FranchiseOffice> FranchiseOffices => Db.SQL<FranchiseOffice>("SELECT fo FROM Essam.FranchiseOffice fo WHERE fo.ParentCorporation = ?", this);
    }
}
