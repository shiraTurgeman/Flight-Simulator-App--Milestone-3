using System.ComponentModel;


namespace FlightSimulatorApp.Model
{
    //The interface that defines the model of the project.
    public interface IModel : INotifyPropertyChanged
    {
        //funcions for the definiton of model.
        void connect(string ip, int port);
        void disconnect();
        void start();
        void update(string dir, double value);

        //the value of the simulator.
        double Throttle { set; get; }
        double Aileron { set; get; }
        double Elevator { set; get; }
        double Rudder { set; get; }
        double Latitude { set; get; }
        double Longitude { set; get; }
        double Airspeed { set; get; }
        double Altitude { set; get; }
        double Roll { set; get; }
        double Pitch { set; get; }
        double Altimeter { set; get; }
        double Heading { set; get; }
        double GroundSpeed { set; get; }
        double VerticalSpeed { set; get; }

        string ErrorMessage { set; get; }

        bool getStop();
    }
}
