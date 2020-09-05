using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    //the class that implement the telnet client interface.
    public class MyTelnetClient : ITelnetClient
    {
        //variables for the project.
        TcpClient client;
        public static Mutex MutexTelnet = new Mutex();

        //this function connects the model to the server.
        void ITelnetClient.connect(string ip, int port)
        {
            client = new TcpClient();
            client.Connect(ip, port);
        }
        //this function writes the message to the server.
        void ITelnetClient.write(string command)
        {

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            client.GetStream().Write(data, 0, data.Length);

        }
        //this function read the values from the simulator.
        string ITelnetClient.read()
        {
            StreamReader reader2 = new StreamReader(client.GetStream());
            string buff = reader2.ReadLine();
            return buff;
        }
        //this function disconnect the model to the server.
        void ITelnetClient.disconnect()
        {
            if (client == null)
            {
                Console.WriteLine("Client not connected- can't disconnect");
                return;
            }
            client.Close();
            client = null;
        }


    }
}
