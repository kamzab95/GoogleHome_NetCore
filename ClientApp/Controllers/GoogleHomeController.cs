
using System;
using ClientApp._GoogleHome;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientApp.Controllers
{
    [Route("api/googlehome")]
    public class GoogleHomeController : Controller
    {
        //[Authorize]
        public JsonResult Index([FromBody] GoogleRequest request){
            Console.WriteLine("------------------"+request.inputs[0].intent+"--------------------");
            if(request.inputs[0].intent == "action.devices.SYNC"){
                var resp = GoogleIntents.Sync(request);
                Console.WriteLine("----SYNC----" + resp.ToString());
                return new JsonResult(resp);
            }
            var error = new ErrorResponse(request.requestId, "unknown intent", "intent " + request.inputs[0].intent);
            Console.WriteLine("*******************"+request.inputs[0].intent+"*******************");
            return new JsonResult(error);
        }
    }
}