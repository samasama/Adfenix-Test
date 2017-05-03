using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simplified.Ring1;
using Simplified.Ring3;
using Starcounter;

namespace Essam
{
    [Database]
    public class HomeSaleTransaction:Address
    {

        public FranchiseOffice ParentFranchiseOffice;
        public DateTime Date;
        public decimal SalesPrice;
        public decimal Commission;
    }
}
