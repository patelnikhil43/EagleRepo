using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ProfileFeedClass
    {
        public int postID { get; set; }
        public String userEmail { get; set; }
        public String postBody { get; set; }
        public String imageURL { get; set; }
        public String datePosted { get; set; }
        public String postType { get; set; }
        public String postingToUser { get; set; }


        public ProfileFeedClass() {

        }

        public List<ProfileFeedClass> RetrieveProfileFeed(String RequestingUser, String RequestedUserEmail, String privacySetting)
        {
            List<ProfileFeedClass> EmptyArray = new List<ProfileFeedClass>();
           
            Profile ProfileObject = new Profile();
            Boolean checkifFriends = ProfileObject.CheckIf2UsersAreFriends(RequestingUser, RequestedUserEmail);
            if (checkifFriends == true)
            {
                List<ProfileFeedClass> feedArray = new List<ProfileFeedClass>();
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_LoadProfileFeed";
                objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);
                DataSet FeedDS = objDB.GetDataSetUsingCmdObj(objCommand);

                if (FeedDS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < FeedDS.Tables[0].Rows.Count; i++)
                    {
                        ProfileFeedClass postObject = new ProfileFeedClass();

                        postObject.postID = int.Parse(FeedDS.Tables[0].Rows[i][0].ToString());
                        postObject.userEmail = FeedDS.Tables[0].Rows[i][1].ToString();
                        postObject.postBody = FeedDS.Tables[0].Rows[i][2].ToString();
                        postObject.datePosted = FeedDS.Tables[0].Rows[i][4].ToString();
                        postObject.imageURL = FeedDS.Tables[0].Rows[i][3].ToString();
                        postObject.postType = FeedDS.Tables[0].Rows[i][6].ToString();
                        postObject.postingToUser = FeedDS.Tables[0].Rows[i][7].ToString();
                        feedArray.Add(postObject);
                    }
                    return feedArray;
                }
                else {
                   return EmptyArray;
                }

            }
            if (privacySetting == "Public")
            {
                List<ProfileFeedClass> feedArray = new List<ProfileFeedClass>();
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_LoadProfileFeed";
                objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);
                DataSet FeedDS = objDB.GetDataSetUsingCmdObj(objCommand);
                if (FeedDS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < FeedDS.Tables[0].Rows.Count; i++)
                    {
                        ProfileFeedClass postObject = new ProfileFeedClass();

                        postObject.postID = int.Parse(FeedDS.Tables[0].Rows[i][0].ToString());
                        postObject.userEmail = FeedDS.Tables[0].Rows[i][1].ToString();
                        postObject.postBody = FeedDS.Tables[0].Rows[i][2].ToString();
                        postObject.datePosted = FeedDS.Tables[0].Rows[i][4].ToString();
                        postObject.imageURL = FeedDS.Tables[0].Rows[i][3].ToString();
                        postObject.postType = FeedDS.Tables[0].Rows[i][6].ToString();
                        postObject.postingToUser = FeedDS.Tables[0].Rows[i][7].ToString();
                        feedArray.Add(postObject);
                    }
                    return feedArray;
                }
                else
                {
                    return EmptyArray;
                }
            }
            if (privacySetting == "Friends")
            {
                List<ProfileFeedClass> feedArray = new List<ProfileFeedClass>();
                Boolean flag = ProfileObject.CheckIf2UsersAreFriends(RequestingUser, RequestedUserEmail);
                if (flag == true)
                {
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_LoadProfileFeed";
                    objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);
                    DataSet FeedDS = objDB.GetDataSetUsingCmdObj(objCommand);
                    if (FeedDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < FeedDS.Tables[0].Rows.Count; i++)
                        {
                            ProfileFeedClass postObject = new ProfileFeedClass();

                            postObject.postID = int.Parse(FeedDS.Tables[0].Rows[i][0].ToString());
                            postObject.userEmail = FeedDS.Tables[0].Rows[i][1].ToString();
                            postObject.postBody = FeedDS.Tables[0].Rows[i][2].ToString();
                            postObject.datePosted = FeedDS.Tables[0].Rows[i][4].ToString();
                            postObject.imageURL = FeedDS.Tables[0].Rows[i][3].ToString();
                            postObject.postType = FeedDS.Tables[0].Rows[i][6].ToString();
                            postObject.postingToUser = FeedDS.Tables[0].Rows[i][7].ToString();
                            feedArray.Add(postObject);
                        }
                        return feedArray;
                    }
                    else
                    {
                        return EmptyArray;
                    }
                }
            }
            if (privacySetting == "FOF")
            {
                List<ProfileFeedClass> feedArray = new List<ProfileFeedClass>();
                Boolean flag = ProfileObject.CheckIfFOF(RequestingUser, RequestedUserEmail);
                if (flag == true)
                {
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_LoadProfileFeed";
                    objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);
                    DataSet FeedDS = objDB.GetDataSetUsingCmdObj(objCommand);
                    if (FeedDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < FeedDS.Tables[0].Rows.Count; i++)
                        {
                            ProfileFeedClass postObject = new ProfileFeedClass();

                            postObject.postID = int.Parse(FeedDS.Tables[0].Rows[i][0].ToString());
                            postObject.userEmail = FeedDS.Tables[0].Rows[i][1].ToString();
                            postObject.postBody = FeedDS.Tables[0].Rows[i][2].ToString();
                            postObject.datePosted = FeedDS.Tables[0].Rows[i][4].ToString();
                            postObject.imageURL = FeedDS.Tables[0].Rows[i][3].ToString();
                            postObject.postType = FeedDS.Tables[0].Rows[i][6].ToString();
                            postObject.postingToUser = FeedDS.Tables[0].Rows[i][7].ToString();
                            feedArray.Add(postObject);
                        }
                        return feedArray;
                    }
                    else
                    {
                        return EmptyArray;
                    }
                }
                else
                {
                    return EmptyArray;
                }

            }
            else {
                return EmptyArray;
            }

         }
    }
}
