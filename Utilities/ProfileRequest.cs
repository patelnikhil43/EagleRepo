using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
   public class ProfileRequest
    {
        public String RequestingEmail { get; set; }
        public String RequestedEmail { get; set; }
        public String Token { get; set; }
    }
}
