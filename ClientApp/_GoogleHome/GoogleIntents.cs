using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClientApp._GoogleHome{
    public static class GoogleIntents{
        public static List<Device> devices = new List<Device>{
            new Device {
                id = "19.12",
                type = "action.devices.types.LIGHT",
                traits = new List<string> {
                    "action.devices.traits.OnOff", "action.devices.traits.Brightness"
                },
                name = new DeviceName{
                    defaultNames = new string[] {"kejw product"},
                    name = "Raspberry",
                    nicknames = new string[] {"kejw dev"}
                },
                willReportState = false,
                roomHint = "office",
                deviceInfo = new {manufacturer = "kejw"},
                online = true,
                on = false,
                brightness = 30
            }
        };

        public static void AddDevice(Device device){
            devices.Add(device);
        }
        
        public static SyncResponse Sync(GoogleSyncRequest request){
            //TODO: there is some problem with casting, even Array.ConvertAll
            // issue: somehow on and online property is kept and later can be seen in JSON
            //in this case google won't accept our SYNC response

            var devices_info = new List<DeviceInfo>();
            foreach(var dev in devices){
                devices_info.Add(dev.CloneToDeviceInfo());
            }
            
            var response = new SyncResponse{
                requestId = request.requestId,
                payload = new PayloadSync{
                    agentUserId = "123.123",
                    devices = devices_info.ToArray()
                }
            };
            return response;
        }

        public static QueryResponse Query(GoogleQueryRequest request){

            var devices_query = request.inputs[0].payload.devices;
            var devices_status = new Dictionary<string, dynamic>();
            foreach(var dev_query in devices_query){
                var dev_obj = GetDeviceById(dev_query.id);
                devices_status.Add(dev_obj.id, new {
                        on = dev_obj.on,
                        online = dev_obj.online
                });
            }
    
            var response = new QueryResponse{
                requestId = request.requestId,
                payload = new PayloadQueryResponse{
                    devices = devices_status
                }
            };
            return response;
        }

        public static ExecuteResponse Execute(GoogleExecuteRequest request){

            var commands = request.inputs[0].payload.commands;
            var command_responses = new List<CommandResponse>();
            foreach(var command in commands){
                var devices = command.devices;
                var executions = command.execution;
                foreach(var exe in executions){
                    if(exe.command == "action.devices.commands.OnOff"){
                        foreach(var dev in devices){
                            var id = dev.id;
                            var dev_obj = GetDeviceById(dev.id);
                            dev_obj.on = exe.parameters["on"];
                            command_responses.Add(new CommandResponse{
                                ids = new string[]{id},
                                status = "SUCCESS",
                                states = new{
                                    on = dev_obj.on,
                                    online = dev_obj.online
                                }
                            });
                        } 
                    }
                }
            }

            var response = new ExecuteResponse{
                requestId = request.requestId,
                payload = new PayloadExecuteResponse{
                    commands = command_responses.ToArray()
                }
            };
            return response;
        }

        public static Device GetDeviceById(string id){
            return devices.Find(x => x.id == id);
        }
        
    }
}