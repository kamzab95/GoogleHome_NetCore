using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClientApp._GoogleHome{
    public class SyncResponse{
        public string requestId {get; set;}
        public PayloadSync payload {get; set;}

        public override string ToString(){
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class QueryResponse{
        public string requestId {get; set;}
        public PayloadQueryResponse payload {get; set;}

        public override string ToString(){
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class ExecuteResponse{
        public string requestId {get; set;}
        public PayloadExecuteResponse payload {get; set;}

        public override string ToString(){
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class ErrorResponse{
        public string requestId {get; set;}
        public PayloadError payload {get; set;}

        public ErrorResponse(string requestId, string errorCode, string debugString){
            payload = new PayloadError{
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

    public class DeviceInfo{
        public string id {get; set;}
        public string type {get; set;}
        public List<string> traits {get; set;}
        public DeviceName name {get; set;}
        public bool willReportState {get; set;}
        public string roomHint {get; set;}
        public dynamic deviceInfo {get; set;}
    }

    public class Device: DeviceInfo{
        public bool on {get; set;}
        public bool online {get; set;}
        public int brightness {get; set;}

        public DeviceInfo CloneToDeviceInfo(){
            return new DeviceInfo{
                id = id,
                type = type,
                traits = traits,
                name = name,
                willReportState = willReportState,
                roomHint = roomHint,
                deviceInfo = deviceInfo
            };
        }
    }



    public class PayloadError{
        public string errorCode {get; set;}
        public string debugString {get; set;}
    }

    public class PayloadSync{
        public string agentUserId {get; set;}
        public DeviceInfo[] devices {get; set;}
    }

    public class PayloadQuery{
        public Device[] devices {get; set;}
    }

    public class PayloadQueryResponse{
        public dynamic devices {get; set;}
    }

    public class PayloadExecute{
        public CommandExecutionRequest[] commands {get; set;}
    }

    public class PayloadExecuteResponse{
        public CommandResponse[] commands {get; set;}
    }

    public class CommandResponse{
        public string[] ids {get; set;}
        public string status {get; set;}
        public dynamic states {get; set;}
    }

    public class CommandExecutionRequest{
        public Device[] devices {get; set;}
        public Execution[] execution {get; set;}
    }

    public class Execution{
        public string command {get; set;}

        [JsonProperty(PropertyName = "params")]
        public Dictionary<string, dynamic> parameters {get; set;}
    }

    public class GoogleSyncInput 
    {
        public string intent {get; set;}
    }

    public class GoogleInput<T> where T: class
    {
        public string intent {get; set;}
        public T payload {get; set;}
    }

    public class GoogleRequest{
        public string requestId {get; set;}
        public dynamic inputs {get; set;}
    }

    public class GoogleSyncRequest{
        public string requestId {get; set;}
        public GoogleSyncInput[] inputs {get; set;}
    }

    public class GoogleQueryRequest{
        public string requestId {get; set;}
        public GoogleInput<PayloadQuery>[] inputs {get; set;}

    }

    public class GoogleExecuteRequest{
        public string requestId {get; set;}
        public GoogleInput<PayloadExecute>[] inputs {get; set;}

    }

    // public class GoogleExecuteRequest{
    //     //public new GoogleInputs<PayloadExecute>[] inputs {get; set;}
    //     public override GoogleInputs<dynamic>[] inputs 
    //     { get => throw new System.NotImplementedException(); 
    //     set => throw new System.NotImplementedException(); }
    // }

 
}