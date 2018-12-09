using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class GalleryImagesClass
    {
        public String userEmail { set; get; }
        public String ImageURL { set; get; }
        public String caption { set; get; }
        public int ImageID { set; get; }

        public GalleryImagesClass()
        {

        }

        public GalleryImagesClass(String email, String url, String captio, int id)
        {
            this.userEmail = email;
            this.ImageURL = url;
            this.caption = captio;
            this.ImageID = id;
        }

        public List<GalleryImagesClass> retrievePhotosList(String RequestingUser, String RequestedUserEmail, String privacySetting)
        {
            Profile ProfileObject = new Profile();
            List<GalleryImagesClass> TempProfileArray = new List<GalleryImagesClass>();
            Boolean checkifFriends = ProfileObject.CheckIf2UsersAreFriends(RequestingUser, RequestedUserEmail);
            if (checkifFriends == true)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_RetrieveImageGallery";
                objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                DataSet ImageGDS = objDB.GetDataSetUsingCmdObj(objCommand);

                List<GalleryImagesClass> ProfileArray = new List<GalleryImagesClass>();

                for (int i = 0; i < ImageGDS.Tables[0].Rows.Count; i++)
                {
                    GalleryImagesClass galleryobject = new GalleryImagesClass();
                    galleryobject.ImageID = int.Parse(ImageGDS.Tables[0].Rows[i]["ImageID"].ToString());
                    galleryobject.userEmail = ImageGDS.Tables[0].Rows[i]["userEmail"].ToString();
                    galleryobject.ImageURL = ImageGDS.Tables[0].Rows[i]["ImageURL"].ToString();
                    galleryobject.caption = ImageGDS.Tables[0].Rows[i]["caption"].ToString();
                    ProfileArray.Add(galleryobject);
                }
                return ProfileArray;
            }


            if (privacySetting == "Public")
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_RetrieveImageGallery";
                objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                DataSet ImageGDS = objDB.GetDataSetUsingCmdObj(objCommand);

                List<GalleryImagesClass> ProfileArray = new List<GalleryImagesClass>();

                for (int i = 0; i < ImageGDS.Tables[0].Rows.Count; i++)
                {
                    GalleryImagesClass galleryobject = new GalleryImagesClass();
                    galleryobject.ImageID = int.Parse(ImageGDS.Tables[0].Rows[i]["ImageID"].ToString());
                    galleryobject.userEmail = ImageGDS.Tables[0].Rows[i]["userEmail"].ToString();
                    galleryobject.ImageURL = ImageGDS.Tables[0].Rows[i]["ImageURL"].ToString();
                    galleryobject.caption = ImageGDS.Tables[0].Rows[i]["caption"].ToString();
                    ProfileArray.Add(galleryobject);
                }
                return ProfileArray;
            }

            if (privacySetting == "Friends")
            {
                Boolean flag = ProfileObject.CheckIf2UsersAreFriends(RequestingUser, RequestedUserEmail);
                if (flag == true)
                {
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_RetrieveImageGallery";
                    objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                    DataSet ImageGDS = objDB.GetDataSetUsingCmdObj(objCommand);

                    List<GalleryImagesClass> ProfileArray = new List<GalleryImagesClass>();

                    for (int i = 0; i < ImageGDS.Tables[0].Rows.Count; i++)
                    {
                        GalleryImagesClass galleryobject = new GalleryImagesClass();
                        galleryobject.ImageID = int.Parse(ImageGDS.Tables[0].Rows[i]["ImageID"].ToString());
                        galleryobject.userEmail = ImageGDS.Tables[0].Rows[i]["userEmail"].ToString();
                        galleryobject.ImageURL = ImageGDS.Tables[0].Rows[i]["ImageURL"].ToString();
                        galleryobject.caption = ImageGDS.Tables[0].Rows[i]["caption"].ToString();
                        ProfileArray.Add(galleryobject);
                    }
                    return ProfileArray;
                }
                else
                {
                    return TempProfileArray;
                }
            }

            if (privacySetting == "FOF")
            {
                //Check if Friends of Friends

                Boolean flag = ProfileObject.CheckIfFOF(RequestingUser, RequestedUserEmail);
                if (flag == true)
                {
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_RetrieveImageGallery";
                    objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                    DataSet ImageGDS = objDB.GetDataSetUsingCmdObj(objCommand);

                    List<GalleryImagesClass> ProfileArray = new List<GalleryImagesClass>();

                    for (int i = 0; i < ImageGDS.Tables[0].Rows.Count; i++)
                    {
                        GalleryImagesClass galleryobject = new GalleryImagesClass();
                        galleryobject.ImageID = int.Parse(ImageGDS.Tables[0].Rows[i]["ImageID"].ToString());
                        galleryobject.userEmail = ImageGDS.Tables[0].Rows[i]["userEmail"].ToString();
                        galleryobject.ImageURL = ImageGDS.Tables[0].Rows[i]["ImageURL"].ToString();
                        galleryobject.caption = ImageGDS.Tables[0].Rows[i]["caption"].ToString();
                        ProfileArray.Add(galleryobject);
                    }
                    return ProfileArray;
                }
                else
                {
                    return TempProfileArray;
                }

            }
            else {
                return TempProfileArray;
            }
        }
    }
}

