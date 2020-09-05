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

namespace FlightSimulatorApp.View
{
    /// Interaction logic for Start.xaml
    public partial class Start : UserControl
    {
        //variables.
        private IModel model;
        //constroctor.
        public Start(IModel model)
        {
            InitializeComponent();
            this.model = model;
        }

        //this function is activated when the user press the button "connect" of the opening screen.
        private void ButtonConnect(object sender, RoutedEventArgs e)
        {
            string ip = tbIP.Text;
            int port;
            try
            {
                port = Int32.Parse(tbPORT.Text);
                model.connect(ip, port);
            }
            catch
            {
                model.ErrorMessage = "The port you entered is oncorrect.\n please try again.";
            }
            if (model.getStop() == false)
            {
                cicle.Stroke = Brushes.Green;
            }
            else
            {
                cicle.Stroke = Brushes.Red;
            }

            model.start();
        }

        //this function is activated when the user press the button "disconnect" of the opening screen.
        private void ButtonDisconnect(object sender, RoutedEventArgs e)
        {
            cicle.Stroke = Brushes.Red;
            model.disconnect();
        }
    }
}
