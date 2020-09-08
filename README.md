# Flight-Simulator-App--Milestone-3


#### Creators: 
##### Shira Turgeman & Noa Elishmereni
#####  [our GitHub](https://github.com/noaElish/Flight-Simulator-App--Milestone-3)

### **Basic information**
* Purpose of this extercise-
we created an interface for the operation of an aircraft. our plain response to instructions from the user regarding the rudder and the variouse control surfaces. 
the simulartor is desplayed as graphical interface of the cockpit, as shown in the picture below:

 <p align="center">
 <img src=".\1.png" width="500" height="260">
</p>

* Four control surfaces:
   * Joystick:
   Steering wheel - steering wheel direction 
   can be controlled using right and left keys. 
   Elevator - rudder
    can be controlled using up and down keys. 
   * Sliders:
   Aileron - balances
   Throttle - throttle

   
### **How does it work?**
Download FlightSimulator (Or any other simulator) in the next link- https://flightgear.en.uptodown.com/windows. 
open the simulator, go to Setting, go to Additional Setting and add the next line: "--telnet=socket,in,10,127.0.0.1,5403,tcp --httpd=8080".
NOTE! you can choose to use python server. we added a server to this project for your convenience.

In order to log to our programm, a user needs to insert port and ip on the left side of the screen.
note that theres is already an ip and port set by default (they match to the same id and port we inserted the simulator)
, that can be changed by typing new ones in the boxes.
if communication is formed sucssesfully- a green light will appear and a new flight will be presented on the map using an airplain pin. 
to set the direction of the plain, the user can use the four control surfaces as decsribed above.
the programm know to pin the location of the plain every few second- which gives an exact location. 


### **How to use**
1. when downloading the code from GitHub, a new zip directory will appear. 
please open the zip and Extract the code to a new directory that will be used from now- called "FlightSimulator".
2. comprass all the file into a zip file called "FlightSimulator"- all except the bat and server.
3. place the server and bat in the same directory in the same level as the zip called "FlightSimulator".
4. make sure to keep only the zip, server and bat and nothing else but that!
5. open the server.
6. open the bat.


