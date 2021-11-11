using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Configuration;
using System.Net.Sockets;


namespace Szolgáltatás_Orientált_Programozás_ZH2
{
    class Program
    {
        static void Main(string[] args)
        {
            string IP = ConfigurationManager.AppSettings["IP"];
            int PORT = int.Parse(ConfigurationManager.AppSettings["PORT"]);

            TcpClient connection = new TcpClient(IP, PORT);
            Communication start = new Communication(connection);
            start.StartCommunication();
        }
    }
}
