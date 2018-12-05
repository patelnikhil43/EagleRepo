using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace TPWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FindFriends")]
    public class FindFriendsController : Controller
    {
        [HttpPost]
        [Route("FindFriendsDS")]
        public List<FindFriendsClass> Post([FromBody] FindFriendsClass ffObject)
        {
            FindFriendsClass FFClassObject = new FindFriendsClass();
            return FFClassObject.GetFriendMethod(ffObject.userEmail.ToString());
        }
    }
}