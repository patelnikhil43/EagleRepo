using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Posts
    {
        private String postID;
        private String userEmail;
        private String postingToUser;
        private String postBody;
        private String imageURL;
        private DateTime datePosted;
        private String tag;
        private String postType;

        public String PostID
        {
            get { return postID; }
            set { postID = value; }
        }
        public String UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }
        public String PostingToUser
        {
            get { return postingToUser; }
            set { postingToUser = value; }
        }
        public String PostBody
        {
            get { return postBody; }
            set { postBody = value; }
        }
        public String ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; }
        }
        public DateTime DatePosted
        {
            get { return datePosted; }
            set { datePosted = value; }
        }
        public String Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        public String PostType
        {
            get { return postType; }
            set { postType = value; }
        }
    }
}
