using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClientApp._GoogleHome{
    public class SyncResponse{
        public string requestId {get; set;}
        public PayloadSync payload {get; set;}

        public override string ToString(){
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ErrorResponse{
        public string requestId {get; set;}
        public Payload payload {get; set;}

        public ErrorResponse(string requestId, string errorCode, string debugString){
            payload = new Payload{
                errorCode = errorCode,
                debugString = debugString
            };
        }
    }

    public class DeviceName{
        public string[] defaultNames {get; set;}
        public string name {get; set;}
        public string[] nicknames {get; set;}
    }

    public class Device{
        public string id {get; set;}
        public string type {get; set;}
        public List<string> traits {get; set;}
        public DeviceName name {get; set;}
        public bool willReportState {get; set;}
        public dynamic attributes {get; set;}
        public string roomHint {get; set;}
        public dynamic deviceInfo {get; set;}
        public dynamic customData {get; set;}
    }

    public class Payload{
        public string errorCode {get; set;}
        public string debugString {get; set;}
    }

    public class PayloadSync: Payload{
        public string agentUserId {get; set;}
        public List<Device> devices {get; set;}
    }

    public class GoogleInputs{
        public string intent {get; set;}
        public dynamic payload {get; set;}
    }

    public class GoogleRequest{
        public string requestId {get; set;}
        public GoogleInputs[] inputs {get; set;}
    }
 
}