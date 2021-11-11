using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace Server
{
    class Program
    {
        static TcpListener listener = null;
        static void Main(string[] args)
        {
            string IP = ConfigurationManager.AppSettings["IP"];
            int PORT = int.Parse(ConfigurationManager.AppSettings["PORT"]);

            IPAddress address = IPAddress.Parse(IP);
            listener = new TcpListener(address, PORT);
            listener.Start();
            Console.WriteLine("Wait..");
            Thread t1 = new Thread(CommunicationStart);
            t1.Start();

            Console.ReadLine();
        }
        static void CommunicationStart()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Communication communication = new Communication(client);
                Thread t1 = new Thread(communication.StartCommunication);
                t1.Start();
            }
        }
    }
}
