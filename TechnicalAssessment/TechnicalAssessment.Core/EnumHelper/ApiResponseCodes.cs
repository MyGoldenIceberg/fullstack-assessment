using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessment.Core.EnumHelper
{
    public enum ApiResponseCodes
    {
        [Description("AN ERROR OCURRED")]
        EXCEPTION = -5,
        [Description("UNAUTHORIZED ACCESS")]
        UNAUTHORIZED = -4,
        [Description("NOT FOUND")]
        NOT_FOUND = -3,
        [Description("BAD REQUEST")]
        INVALID_REQUEST = -2,
        [Description("SERVER ERROR OCCURED")]
        ERROR = -1,
        [Description("FAIL")]
        FAIL = 2,
        [Description("SUCCESS")]
        OK = 1,
    }
}
