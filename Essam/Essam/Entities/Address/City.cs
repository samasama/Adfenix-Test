using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essam
{
    [Database]
    public class City
    {
        public Country Country;
        public string Value;
    }
}
