using System.Collections.Generic;

namespace ClientApp._GoogleHome{
    public static class GoogleIntents{
        public static List<Device> devices = new List<Device>{
            new Device {
                id = "1836.152673892",
                type = "action.devices.types.LIGHT",
                traits = new List<string> {
                    "action.devices.traits.OnOff", "action.devices.traits.Brightness",
                    "action.devices.traits.ColorTemperature",
                    "action.devices.traits.ColorSpectrum"
                },
                name = new DeviceName{
                    defaultNames = new string[] {"kejw product"},
                    name = "Raspberry",
                    nicknames = new string[] {"kejw dev"}
                },
                willReportState = false,
                roomHint = "office",
                attributes = new {temperatureMinK = 2000, temperatureMaxK = 6000},
                deviceInfo = new {manufacturer = "kejw"},
                customData = new {testData = 1090}
            }
        };
        
        public static SyncResponse Sync(GoogleRequest request){
            var response = new SyncResponse{
                requestId = request.requestId,
                payload = new PayloadSync{
                    agentUserId = "123.123",
                    devices = devices
                }
            };
            return response;
        }
        
    }
}