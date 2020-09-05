using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightSimulatorApp.ViewModel
{
    //View model for the dashboard of the screen.
    class DashboardViewModel : INotifyPropertyChanged
    {
        //variables.
        private IModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        //constroctor.
        public DashboardViewModel(IModel model)
        {
            this.model = model;
            //NotifyPropertyChanged imlementation.
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
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

        //the function for the 8 values of the dashboard.
        public double VM_Heading { get { return model.Heading; } }
        public double VM_Altimeter { get { return model.Altimeter; } }
        public double VM_Roll { get { return model.Roll; } }
        public double VM_Pitch { get { return model.Pitch; } }
        public double VM_Altitude { get { return model.Altitude; } }
        public double VM_AirSpeed { get { return model.Airspeed; } }
        public double VM_VerticalSpeed { get { return model.VerticalSpeed; } }
        public double VM_GroundSpeed { get { return model.GroundSpeed; } }
        public string VM_ErrorMessage { get { return model.ErrorMessage; } }
    }
}
