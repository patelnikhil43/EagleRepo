using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace TPWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/LoadFeed")]
    public class LoadFeedController : Controller
    {
        [HttpPost]
        [Route("GetProfileFeed")]
        public List<ProfileFeedClass> Post([FromBody] ProfileRequest RequestObject)
        {
            List<ProfileFeedClass> emptyArray = new List<ProfileFeedClass>();
            Profile ProfileObj = new Profile();
            ProfileFeedClass ProfileFeedObject = new ProfileFeedClass();
            //Check token
            if (RequestObject.Token == "1234")
            {
                String settingRetrieved = ProfileObj.CheckRequestedUserFeedSetting(RequestObject.RequestedEmail);
                return ProfileFeedObject.RetrieveProfileFeed(RequestObject.RequestingEmail, RequestObject.RequestedEmail, settingRetrieved);
            }
            else {
               return emptyArray;
            }
        }
    }
}