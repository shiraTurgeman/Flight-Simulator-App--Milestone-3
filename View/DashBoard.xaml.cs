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
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp.View
{
    //Interaction logic for DashBoard.xaml
    public partial class DashBoard : UserControl
    {
        //variables
        DashboardViewModel db;
        private IModel model;

        //costroctor.
        public DashBoard(IModel model)
        {
            this.model = model;
            InitializeComponent();
            this.db = new DashboardViewModel(model);
            DataContext = db;
        }
    }
}
