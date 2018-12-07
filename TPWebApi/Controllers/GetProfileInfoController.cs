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
    [Route("api/GetProfileInfo")]
    public class GetProfileInfoController : Controller
    {
        [HttpPost]
        [Route("GetProfileInfoMethod")]
        public List<Profile> Post([FromBody] ProfileRequest RequestObject)
        {
            Profile ProfileObj = new Profile();
            //Check token
            if (RequestObject.Token == "1234")
            {
                //Check Requested User Profile Info Privacy Setting

                String settingRetrieved = ProfileObj.CheckRequestedUserPrivacySetting(RequestObject.RequestedEmail);

                //Retreive Info
                return ProfileObj.RetreiveProfileInfo(RequestObject.RequestingEmail, RequestObject.RequestedEmail, settingRetrieved);

            }
            else
            {
                return null;
            }
        }
    }
}