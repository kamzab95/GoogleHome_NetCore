
using System;
using ClientApp._GoogleHome;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientApp.Controllers
{
    [Route("api")]
    public class GoogleHomeController : Controller
    {
        //[Authorize]
        [HttpPost("googlehome")]
        public JsonResult Index([FromBody] JObject d_request){
            var request = d_request.ToObject<GoogleRequest>();
            if(request.inputs[0]["intent"] == "action.devices.SYNC"){
                var req = d_request.ToObject<GoogleSyncRequest>();
                return Sync(req);
            }else if(request.inputs[0]["intent"] == "action.devices.QUERY"){
                var req = d_request.ToObject<GoogleQueryRequest>();
                return Query(req);
            }else if(request.inputs[0]["intent"] == "action.devices.EXECUTE"){
                var req = d_request.ToObject<GoogleExecuteRequest>();
                return Execute(req);
            }


            var error = new ErrorResponse(request.requestId, "unknown intent", "intent " + request.inputs[0].intent);
            Console.WriteLine("*******************"+request.inputs[0].intent+"*******************");
            return new JsonResult(error);
        }

        
        public JsonResult Sync(GoogleSyncRequest request){
            Console.WriteLine("------------------"+request.inputs[0].intent+"--------------------");
            var resp = GoogleIntents.Sync(request);
            Console.WriteLine("----SYNC----");
            Console.WriteLine(resp.ToString());
            return new JsonResult(resp);
        }

        
        public JsonResult Query([FromBody] GoogleQueryRequest request){
            Console.WriteLine("------------------"+request.inputs[0].intent+"--------------------");
            var resp = GoogleIntents.Query(request);
            Console.WriteLine("----QUERY----" + resp.ToString());
            return new JsonResult(resp);
        }

        
        public JsonResult Execute([FromBody] GoogleExecuteRequest request){
            Console.WriteLine("------------------"+request.inputs[0].intent+"--------------------");
            var payload = request.inputs[0].payload;
            var st = JsonConvert.SerializeObject(payload);
            Console.WriteLine(st);
            var resp = GoogleIntents.Execute(request);
            var resp_st = JsonConvert.SerializeObject(resp);
            Console.WriteLine(resp_st);
            return new JsonResult(resp);
        }
    }
}