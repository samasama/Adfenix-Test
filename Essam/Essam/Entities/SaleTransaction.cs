using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Starcounter;

namespace Essam
{
    [Database]
    public class SaleTransaction
    {

        public FranchiseOffice ParentFranchiseOffice;
        public Address Address;
        public DateTime TransactionDate;
        public long SalesPrice;
        public long Commission;
    }
}
