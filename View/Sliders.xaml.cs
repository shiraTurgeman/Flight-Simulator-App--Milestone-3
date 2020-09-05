using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.View
{
    //Interaction logic for Sliders.xaml
    public partial class Sliders : UserControl
    {
        private IModel model;
        VM_Joystick joy;
        //constroctor.
        public Sliders(IModel model)
        {
            InitializeComponent();
            this.model = model;
            this.joy = new VM_Joystick(model);
            DataContext = joy;
        }


        //for the aileron
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (model.getStop() == false)
            {
                joy.updateAileron(e.NewValue);
            }

        }

        //for throthel
        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (model.getStop() == false)
            {
                joy.updateThrottle(e.NewValue);
            }
        }
    }
}
