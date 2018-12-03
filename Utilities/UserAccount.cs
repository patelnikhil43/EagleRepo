using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class UserAccount
    {
        public UserAccount()
        {

        }
        public String Email { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public int Zip { get; set; }
        public String Password { get; set; }
        public String SecurityQ1 { get; set; }
        public String AnswerQ1 { get; set; }
        public String SecurityQ2 { get; set; }
        public String AnswerQ2 { get; set; }
        public String SecurityQ3 { get; set; }
        public String AnswerQ3 { get; set; }
    }
}
