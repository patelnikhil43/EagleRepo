using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Friend
    {
        private int friendID;
        private String userEmail;
        private String friendEmail;
        private DateTime friendDate;

        public int FriendID
        {
            get { return friendID; }
            set { friendID = value; }
        }
        public String UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }
        public String FriendEmail
        {
            get { return friendEmail; }
            set { friendEmail = value; }
        }
        public DateTime FriendDate
        {
            get { return friendDate; }
            set { friendDate = value; }
        }

    }
}
