# GoogleHome_NetCore

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
Client information: 
**Client ID**: mvc
**Authorization URL**: https://<i.e. ngroc url>/connect/authorize
Client, Scopes:
**scope**: api1

Google will ask for authorization under this (https://<i.e. ngroc url>/connect/authorize) url. I have spent a lot of time trying to understand how does it work,
but it appeared to be fairly easy. 

/connect/authorize is standardized path. When server runs in implicit modes it accepts few settings which google server sends to it,
like Client ID, Scope and (Important!) return url which we don't know right now.







