using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
namespace Essam
{
    public class Start
    {
        public string CorporationName;

        public QueryResultRows<Corporation> Corporations => Db.SQL<Corporation>("SELECT c FROM Essam.Corporation c");
    }
}
