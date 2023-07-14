using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons;

public class Enumerations
{
    [Description("SexType")]
    public enum SexType
    {
        [Description("")]
        None = 0,
        [Description("زن")]
        Female = 1,
        [Description("مرد")]
        Male = 2,
    }

    [Description("State")]
    public enum State
    {
        [Description("")]
        None = 0,
        [Description("فعال")]
        Active = 1,
        [Description("غیرفعال")]
        InActive = 2,
    }
}