using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    //view model for the joystick.
    class VM_Joystick : INotifyPropertyChanged
    {
        //variables.
        private IModel model;
        private double x;
        private double y;
        private string xDirRudder = "/controls/flight/rudder";
        private string yDirEleavtor = "/controls/flight/elevator";
        private string aileronDir = "/controls/flight/aileron";
        private string throttleDir = "/controls/engines/current-engine/throttle";
        public event PropertyChangedEventHandler PropertyChanged;

        //constroctor.
        public VM_Joystick(IModel model)
        {
            this.model = model;
            //NotifyPropertyChanged imlementation.
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        //NotifyPropertyChanged imlementation.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //update the simulator in the changes of the values. 
        public void updateSim(double xN, double yN)
        {
            this.x = xN;
            this.y = yN;
            model.update(xDirRudder, x);
            model.update(yDirEleavtor, y);
        }

        //function to update the server about throttle values.

        public void updateThrottle(double newValue)
        {
            model.Throttle = newValue;
            model.update(throttleDir, newValue);
        }

        //function to update the server about aileron values.
        public void updateAileron(double newValue)
        {
            model.Aileron = newValue;
            model.update(aileronDir, newValue);
        }


        public double VM_Throttle { get { return model.Throttle; } }
        public double VM_Aileron { get { return model.Aileron; } }
    }
}
