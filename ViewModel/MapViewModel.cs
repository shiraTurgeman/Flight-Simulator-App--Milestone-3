using FlightSimulatorApp.Model;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class MapViewModel : INotifyPropertyChanged
    {

        private IModel model;
        //constructor for the vm of the map
        public MapViewModel(IModel model)
        {
            this.model = model;
            //NotifyPropertyChanged imlementation.
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_Location");
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //NotifyPropertyChanged imlementation.
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //in change of the location for the pin of the map
        public Location VM_Location { get { return new Location(model.Latitude, model.Longitude); } }
    }

}
