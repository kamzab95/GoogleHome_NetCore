# Google Home .Net Core, Authentication and SYNC - Creating IoT device

## Description

My goal is to create server, which will allow me to connect IoT device(s) to Google Home (with as deep integration as possible).
I have been working with .Net Core for few months till now. It appeared to be realy easy to work with. As for now I have not found 
any similar sollution I have decided to share my work with you. And of course, I am counting on your feedback.

## How does it work? (Briefly)

https://developers.google.com/actions/smarthome/create#request

Google Home requrie authenticaton OAuth 2.0, well our server has to be be able to provide this.
To fulfill this need, our server use IdentityServer4. Usually I have seen IdentityServer as separate service, 
but I wanted my server to work as one (also on one port).
Next we have to setup controller witch will receive request from Google.

How to test this? Ngrok appeared to be really usefull. It prides us also with https connection ability (which is requried). 

## Step 1 - Authentication - "Hey Google Servers, can you accept mine to your comunity? pls"

##### Actions on Google setup

We have to setup account linking. Google allows us to choose in between Implicit or Authorization Code. In this case we will 
decide for implicit sollution. 

```
Linking type:
OAuth
Implicit

Client information: 
Client ID: mvc
Authorization URL: https://<i.e. ngroc url>/connect/authorize

Configure your client:
scope: api1
```

Google will ask for authorization under this (https://<i.e. ngroc url>/connect/authorize) url. I have spent a lot of time trying to understand how does it work,
but it appeared to be fairly easy. 

/connect/authorize is standardized path. When server runs in implicit mode it accepts few settings which google server sends to it,
like Client ID, Scope and (Important!) **Return URL** which we don't know right now.
Durring authentication process (through Gooogle Home app) we will be taken to the authorization page on our server,
this few information will be passed to it at the same moment. When we log in (BTW default login for now is alice:alice), 
our server will pass the token to the "return url".

##### Server setup

First, there is one thing that require some tweaking. 
In the config file, under GetClients() there is setting called RedirectUris. 
You have to edit this string:
https://oauth-redirect.googleusercontent.com/r/myhome-c023c
For our server to accept google request, we have to add return url to this list. What you have to change is project id
(as you can see in my case "myhome-c023c"). If the url wont be correct our server wont allow google to authenticate.

##### Info

http://docs.identityserver.io/en/latest/quickstarts/3_interactive_login.html#refimplicitquickstart
You dont have to bother to create own interface at this moment. I was't neither. Its all available to download from
IdentityServer repository (check the link for more info about this and whole Identity flow setup).

## Step 2 - SYNC - lets create some devices

#### Actions on Google 
Create Smart Home action -> Add fulfillment URL
```
https:/<i.e. ngrok url>/api/googlehome
```


https://developers.google.com/actions/smarthome/create#request
Just after succesfull authentication, google will send request to our server to the fulfilment url. First request is SYNC request. More about this is unther the link. As a response to this request google requires from as to return information 
about our devices. Creating devices is nothing more like adding new object to array. Why? Because for now there is no database in this project. Google servers don't need any proof that device like this exists. All they want is JSON response with information about them. 
```
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
```
I have defined model for response, device etc. well it should be fairly easy to understand how to create something by yourself. 
Purpose to this code (hmm not the only one you see above, I mean whole code which creates reponse for google request) is to create device, response etc. as on object, which is easy to maintain from the code and at the converting it to JSON response which will be accepted by the google servers.

### Is it all? 

No. At this point we should be able to authenticate in Google Home App and new device should be dicovered by the app.

There is no action for now i.e. when you try to turn on created device.
I will try to extend features of this server soon.

## PS - (hmmm ... I have to orginize this description a bit beter)

Assuming that your project is already setup on Actions on Google. (Also i will try to explain this a bit better in the future)

How to test and connect to our server?
Open Home app -> + Add -> + Set up device -> **Works with Google** Have something... -> name of yout service should appear on top of the list with the "[test]" in front of its name. That way we can use our own action without publishing it. 










