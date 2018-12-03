using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Message
    {
        private int messageID;
        private String userEmail;
        private String friendEmail;
        private String messageBody;
        private DateTime messageDate;

        public int MessageID { get; set; }
        public String UserEmail { get; set; }
        public String FriendEmail { get; set; }
        public String MessageBody { get; set; }
        public DateTime MessageDate { get; set; }

    }
}
