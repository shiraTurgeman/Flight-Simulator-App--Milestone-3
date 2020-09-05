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
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.View;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp
{

    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //create new telnet client
            ITelnetClient telnetClient = new MyTelnetClient();
            //create new model
            IModel model = new MyModel(telnetClient);
            Start s = new Start(model);

            //send the model to each of the object 
            DashBoard dbView = new DashBoard(model);
            Map mapView = new Map(model);
            Joystick JoystickMain = new Joystick(model);
            Sliders slide = new Sliders(model);

            //add the object to the mainwindow
            dbSpace.Children.Add(dbView);
            mapSpace.Children.Add(mapView);
            joystick.Children.Add(JoystickMain);
            sliders.Children.Add(slide);
            screen.Children.Add(s);
        }
    }
}
