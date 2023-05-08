using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTest.FactoryList
{
    public enum TotalDemandType
    {
        [Description("样板")]
        Template = 0,
        [Description("量产")]
        Output = 1,
    }
}
