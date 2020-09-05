using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;


namespace FlightSimulatorApp.Model
{

    //the interface that implements the model of the project.
    public class MyModel : IModel
    {
        //variable of the class.
        Queue<string> queue = new Queue<string>();
        ITelnetClient telnetClient;
        volatile Boolean stop;

        //constroctor.
        public MyModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            //stop = false;
            stop = true;
        }

        //implementation of PropertyChangedEventHandler.
        public event PropertyChangedEventHandler PropertyChanged;

        //this function connects the model to the server.
        public void connect(string ip, int port)
        {

            try
            {
                var task = Task.Run(() => telnetClient.connect(ip, port));
                if (task.Wait(TimeSpan.FromSeconds(10)))
                {
                    stop = false;
                    ErrorMessage = " ";
                }
                else
                {
                    //shoe the error message for no information from the server.
                    ErrorMessage = "Theres no information from the\nserver for over 10 seconds.";
                }
            }
            catch
            {
                ErrorMessage = "The ip or port you entered is incorrect.\n please try again.";
                stop = true;
            }
        }
        //this function disconnect the model to the server.
        public void disconnect()
        {
            stop = true;
            telnetClient.disconnect();
            ErrorMessage = "Plesde restart your server again and\n press connect to start the simulator.";
        }
        //this function start the opertation of model.
        String value;
        String property;
        String str;
        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    //push the message to the server.
                    pushMassege();
                    while (queue.Count > 0)
                    {
                        str = queue.Dequeue();
                        telnetClient.write(str);
                        var task = Task.Run(() => (value = telnetClient.read()));
                        //check for timeout of 10 seconds.
                        if (task.Wait(TimeSpan.FromSeconds(10)))
                        {
                            property = checkProperty(str);
                            initializeProperty(property, value);
                        }
                        //shoe the right message of timeout.
                        else
                        {
                            ErrorMessage = "Theres no information from the\nserver for over 10 seconds.";
                        }
                    }
                    Thread.Sleep(250); //read the data in 4Hz
                }
            }).Start();
        }

        //the function that holdes the values of the different object of the airplane.
        private double throttle, aileron, elevator, rudder, latitude, longitude, airspeed, altitude, roll, pitch, altimeter, heading, groundSpeed, verticalSpeed;
        public double Throttle { set { throttle = value; NotifyPropertyChanged("Throttle"); } get { return throttle; } }
        public double Aileron { set { aileron = value; NotifyPropertyChanged("Aileron"); } get { return aileron; } }
        public double Elevator { set { elevator = value; NotifyPropertyChanged("Elevator"); } get { return elevator; } }
        public double Rudder { set { rudder = value; NotifyPropertyChanged("Rudder"); } get { return rudder; } }
        public double Latitude { set { latitude = value; NotifyPropertyChanged("Latitude"); } get { return latitude; } }
        public double Longitude { set { longitude = value; NotifyPropertyChanged("Longitude"); } get { return longitude; } }
        public double Airspeed { set { airspeed = value; NotifyPropertyChanged("Airspeed"); } get { return airspeed; } }
        public double Altitude { set { altitude = value; NotifyPropertyChanged("Altitude"); } get { return altitude; } }
        public double Roll { set { roll = value; NotifyPropertyChanged("Roll"); } get { return roll; } }
        public double Pitch { set { pitch = value; NotifyPropertyChanged("Pitch"); } get { return pitch; } }
        public double Altimeter { set { altimeter = value; NotifyPropertyChanged("Altimeter"); } get { return altimeter; } }
        public double Heading { set { heading = value; NotifyPropertyChanged("Heading"); } get { return heading; } }
        public double GroundSpeed { set { groundSpeed = value; NotifyPropertyChanged("GroundSpeed"); } get { return groundSpeed; } }
        public double VerticalSpeed { set { verticalSpeed = value; NotifyPropertyChanged("VerticalSpeed"); } get { return verticalSpeed; } }
        //the objects for the error messages.
        private string errorMessage;
        public string ErrorMessage { set { errorMessage = value; NotifyPropertyChanged("ErrorMessage"); } get { return errorMessage; } }


        //the function that parser over the string we get from the simulator.
        //cut the string and insert the values to the matching values in the programm. 
        public void parserProperty(string str)
        {
            string[] words = str.Split('\n');
            Throttle = Double.Parse(words[0]);
            Aileron = Double.Parse(words[1]);
            Elevator = Double.Parse(words[2]);
            Rudder = Double.Parse(words[3]);
            if (!((Double.Parse(words[4]) < -90) || (Double.Parse(words[4]) > 90)))
            {
                Latitude = Double.Parse(words[4]);
            }
            if (!((Double.Parse(words[5]) < -180) || (Double.Parse(words[5]) > 180)))
            {
                Longitude = Double.Parse(words[5]);
            }
            Airspeed = Double.Parse(words[6]);
            Altitude = Double.Parse(words[7]);
            Roll = Double.Parse(words[8]);
            Pitch = Double.Parse(words[9]);
            Altimeter = Double.Parse(words[10]);
            Heading = Double.Parse(words[11]);
            GroundSpeed = Double.Parse(words[12]);
            VerticalSpeed = Double.Parse(words[13]);
        }

        //implementation of NotifyPropertyChanged.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //the function that updates the value from the joystick.
        void IModel.update(string dir, double value)
        {

            String sendMe = value.ToString();
            if (sendMe.Length > 6)
            {
                sendMe = sendMe.Substring(0, 6);
            }
            string command = "set " + dir + " " + sendMe + " \r\n";
            queue.Enqueue(command);
        }

        //return the boolean value of stop.
        public bool getStop()
        {
            return this.stop;
        }


        //this function sends to the server the right messgae to get the right value.
        void pushMassege()
        {
            queue.Enqueue("get /controls/engines/current-engine/throttle\r\n");
            queue.Enqueue("get /controls/flight/aileron\r\n");
            queue.Enqueue("get /controls/flight/elevator\r\n");
            queue.Enqueue("get /controls/flight/rudder\r\n");
            queue.Enqueue("get /position/latitude-deg\r\n");
            queue.Enqueue("get /position/longitude-deg\r\n");
            queue.Enqueue("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
            queue.Enqueue("get /instrumentation/gps/indicated-altitude-ft\r\n");
            queue.Enqueue("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
            queue.Enqueue("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
            queue.Enqueue("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
            queue.Enqueue("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
            queue.Enqueue("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
            queue.Enqueue("get /instrumentation/gps/indicated-vertical-speed\r\n");
        }

        //this function compare between the mathcing object and the path and return the values.
        string checkProperty(string str)
        {
            if (str.Equals("get /controls/engines/current-engine/throttle\r\n"))
            {
                return "Throttle";
            }
            else if (str.Equals("get /controls/flight/aileron\r\n"))
            {
                return "Aileron";
            }
            else if (str.Equals("get /controls/flight/elevator\r\n"))
            {
                return "Elevator";
            }
            else if (str.Equals("get /controls/flight/rudder\r\n"))
            {
                return "Rudder";
            }
            else if (str.Equals("get /position/latitude-deg\r\n"))
            {
                return "Latitude";
            }
            else if (str.Equals("get /position/longitude-deg\r\n"))
            {
                return "Longitude";
            }
            else if (str.Equals("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n"))
            {
                return "Airspeed";
            }
            else if (str.Equals("get /instrumentation/gps/indicated-altitude-ft\r\n"))
            {
                return "Altitude";
            }
            else if (str.Equals("get /instrumentation/attitude-indicator/internal-roll-deg\r\n"))
            {
                return "Roll";
            }
            else if (str.Equals("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n"))
            {
                return "Pitch";
            }
            else if (str.Equals("get /instrumentation/altimeter/indicated-altitude-ft\r\n"))
            {
                return "Altimeter";
            }
            else if (str.Equals("get /instrumentation/heading-indicator/indicated-heading-deg\r\n"))
            {
                return "Heading";
            }
            else if (str.Equals("get /instrumentation/gps/indicated-ground-speed-kt\r\n"))
            {
                return "GroundSpeed";
            }
            else if (str.Equals("get /instrumentation/gps/indicated-vertical-speed\r\n"))
            {
                return "VerticalSpeed";
            }

            return " ";
        }

        //this function get a value and an object and puts the value in the matching object .
        void initializeProperty(String str, String value)
        {
            // if the information from the server is err, show the mathcing notafication
            if (value.Equals("ERR"))
            {
                ErrorMessage = str + " = ERR";
            }
            else if (str.Equals("Throttle"))
            {
                // Throttle = Double.Parse(value);
            }
            else if (str.Equals("Aileron"))
            {
                // Aileron = Double.Parse(value);
            }
            else if (str.Equals("Elevator"))
            {
                Elevator = Double.Parse(value);
            }
            else if (str.Equals("Rudder"))
            {
                Rudder = Double.Parse(value);
            }
            //check the Latitude value are in between -90 to 90
            else if (str.Equals("Latitude"))
            {
                if (!((Double.Parse(value) < -90) || (Double.Parse(value) > 90)))
                {
                    Latitude = Double.Parse(value);
                }
                else
                {
                    ErrorMessage = str + " = ERR";
                }
            }
            //check the Longitude value are in between -180 to 180
            else if (str.Equals("Longitude"))
            {
                if (!((Double.Parse(value) < -180) || (Double.Parse(value) > 180)))
                {
                    Longitude = Double.Parse(value);
                }
                else
                {
                    ErrorMessage = str + " = ERR";
                }
            }
            else if (str.Equals("Airspeed"))
            {
                Airspeed = Double.Parse(value);
            }
            else if (str.Equals("Altitude"))
            {
                Altitude = Double.Parse(value);
            }
            else if (str.Equals("Roll"))
            {
                Roll = Double.Parse(value);
            }
            else if (str.Equals("Pitch"))
            {
                Pitch = Double.Parse(value);
            }
            else if (str.Equals("Altimeter"))
            {
                Altimeter = Double.Parse(value);
            }
            else if (str.Equals("Heading"))
            {
                Heading = Double.Parse(value);
            }
            else if (str.Equals("GroundSpeed"))
            {
                GroundSpeed = Double.Parse(value);
            }
            else if (str.Equals("VerticalSpeed"))
            {
                VerticalSpeed = Double.Parse(value);
            }

        }
    }
}
