using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Joystick.xaml
    public partial class Joystick : UserControl
    {
        //vairables.
        private IModel model;
        VM_Joystick joy;
        double x, y;
        double Nx, Ny;
        double XPOS, YPOS;
        double XmaxValue = 28;
        double XminValue = -28;
        double YmaxValue = 28;
        double YminValue = -28;
        private Point P;
        private static Mutex Mutex = new Mutex();
        //constroctor.
        public Joystick(IModel model)
        {
            InitializeComponent();
            this.model = model;
            this.joy = new VM_Joystick(model);
            DataContext = joy;
        }

        //this function is activated when the user press the mouse on the joystick.
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (model.getStop() == false)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    P = e.GetPosition(this);
                    XPOS = knobPosition.X;
                    YPOS = knobPosition.Y;
                }
            }

        }
        //this function is activated when the user press the mouse up on the joystick.
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;

            knobPosition.Y = 0;
        }
        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
        //this function is activated when the user press the mouse down on the joystick.
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (model.getStop() == false)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //get the position of the joystick
                    Nx = e.GetPosition(this).X;
                    Ny = e.GetPosition(this).Y;

                    x = Nx - P.X;
                    y = Ny - P.Y;


                    if (Math.Sqrt(x * x + y * y) < Base.Width / 12)
                    {
                        knobPosition.X = x;
                        knobPosition.Y = y;

                        //normalize the value and send to server.
                        double xToSend = (x - XminValue) / (XmaxValue - XminValue);
                        double yToSend = (y - YminValue) / (YmaxValue - YminValue);
                        xToSend = (2 * xToSend) - 1;
                        yToSend = (2 * yToSend) - 1;

                        joy.updateSim(xToSend, yToSend);
                    }
                }
            }
        }

        //the next function are empty implementation for those object of the joystick.

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Knob_MouseUp(sender, e);

        }


        private void KnobBase_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Knob_MouseUp(sender, e);

        }


        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            Knob_MouseMove(sender, e);

        }

        private void Knob_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Knob_MouseLeave(sender, e);

        }

        void centerKnob_Completed(object sendet, EventArgs e)
        {
        }

    }
}
