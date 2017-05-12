using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;

namespace Essam
{
    [Database]
    public class Corporation
    {
        public string Name;

        public QueryResultRows<FranchiseOffice> FranchiseOffices => Db.SQL<FranchiseOffice>("SELECT fo FROM FranchiseOffice fo WHERE fo.ParentCorporation = ?", this);

        public static QueryResultRows<Corporation> Corporations => Db.SQL<Corporation>("SELECT c FROM Corporation c");
    }
}
