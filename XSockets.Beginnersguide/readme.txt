ReadMe.txt

First of all I would like to clarify something about XSockets.NET
XSockets is suitable for using in webcommunication such as a ASP.NET MVC3 projects etc.
However we have also large usage of XSockets in other areas... 
You can send messages through XSockets from for example:
	- A netduino (I made a demo a few days ago)
		link: http://twitter.yfrog.com/0bvluwjdjnwhquzuvprjeigwz
	- Kinect (We did a virtual receptionist for Voxeo)
		link: http://www.youtube.com/watch?v=VilJpRXAafs

To wrap that short intro up... 
Do not look at XSokcets as a websocket server. Look at it as a realtimecommunication framework

Soo... lets get started...

I have created a phase for each file and each new phase starts as a copy of the previous one.
This way you can follow the development. Note: serverside will only be one file.
Use any HTML5 compatible browser, but I recommend Chrome.

1:	Created this solution
2:	Deleted all junk in the solution.. fodler and scripts not used.
3:	Opened the package manager console and ran Install-package XSockets
	Note: to understand what the install-package did. Read this: http://average-uffe.blogspot.se/2012/09/xsocketsnet-20-getting-started-with.html
__________________________________
Phase 1 default.htm - Creating a html page and a controller to connect to
4:	Created a default-1.htm file that I will use to show the basics.
	I think that a chat where you can select which city to send to can be a good start.
4:1	In this new file I added script references to jQuery and XSocket
4:2 XSockets has a default controller named "Generic" but we will create our own (in step 5), so we connect to the one named GeoLocation.
4:3 We bind (subscribe) to the open and the error events. And write some info to the console.
5:  Created a realtime controll (in the XSocketHandler project) named GeoLocation. It is just a empty class right now, but since it inherits XSocketController we can already use it.
6:	Compiled the application so that the plugin (GeoLocation) gets copied to the plugin directory.
7:	Set default-1.htm as startpage by rightclicking it and select set as startpage
8:	Hit F5.. The default page will start and in the console (ctrl + shift + j in chrome) you can see information that we are connected.
	You will also see an error message saying that you did not provide a city parameter... This will be explained in Phase 3
__________________________________
Phase 2 default-2.htm - Sending messages to ALL clients
9:	By default any message sent (published) into the server will be dispatched to everyone listening for the subject (all subscribers)
	So now I added a input field in the default-2.htm and also a button.
	I also added a div with id=log so that we can output information there
10:	Added some jQuery in the GUIEvents part in the javascript...
	When the button gets clicked we trigger (publish) a event called 'sendtoall'
11: Added a bind (subscription) for the 'sendtoall' event. And when we recieve info we write to the log div
12: Run default-2.htm in 2 of more browsers... you now have a simple chat in realtime.
	You will still get the error message about the city parameter (see number 8 in the list), but everything will work just fine!
__________________________________
Phase 3 default-3.htm - Sending to client depending on city.
13:	Now I wrote some backend code in the so far empty controller GeoLocation.
	I added a viewmodel class with properties City and Message and added an instance of this as a property on my controller
	I added a public method SetCity for telling what city I am in
		This method will update my ViewModel property and then send a message back that it was updated
	I also added a public method SendToCity for sending messages to a specific city
	Note: we can do all this with just javascript, but to buíld good architecture and a "real application" you will need the controllers etc.
14: Added a dropdownlist with cities in the default-3.htm so that you can fake a city.
15: I will also modify the connection so that we pass in a city as a parameter when connecting
16: I use the OnClientConnect event in the controller to register the city that was passed when connecting. Note: Also added some try catch statements
17: Listening for the change event on the dropdown so that I can tell the server that I changed my city
18: Added button for sendtocity and javascript listening to the click... An then trigger the message to teh correct city
19: Added a bind (subscription) for the 'sendtocity' event. And when we recieve info we write to the log div

Thats it.

Please just ask if you have any questions...
The next step would to be howto use XSockets with a servicelayer talking to hardware etc.
Maybe also showing how to do longrunning tasks (a.k.a internel controllers) that can push data to clients.

Regards
Uffe on behalf of Team XSockets.NET
