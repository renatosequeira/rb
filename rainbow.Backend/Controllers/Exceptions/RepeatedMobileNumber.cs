using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rainbow.Backend.Controllers.Exceptions
{
    public class RepeatedMobileNumber : Exception
    {

        public RepeatedMobileNumber(int severity, string message) : base(message)
        {
            // do whatever you want with severity
        }
 
    }
}