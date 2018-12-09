using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;
namespace TPWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ImageGallery")]
    public class ImageGalleryController : Controller
    {

        [HttpPost]
        [Route("GetImages")]
        public List<GalleryImagesClass> Post([FromBody] ProfileRequest RequestObject)
        {
            Profile ProfileObj = new Profile();
            GalleryImagesClass GalleryObject = new GalleryImagesClass();
            //Check token
            if (RequestObject.Token == "1234")
            {
                //Check Requested User Profile Info Privacy Setting

                String settingRetrieved = ProfileObj.CheckRequestedUserPrivacySetting(RequestObject.RequestedEmail);

                //Retreive Info
                return GalleryObject.retrievePhotosList(RequestObject.RequestingEmail, RequestObject.RequestedEmail, settingRetrieved);

            }
            else
            {
                return null;
            }
        }
    }
}