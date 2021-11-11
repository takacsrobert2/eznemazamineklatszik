using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Configuration;
using System.Net.Sockets;
using System.IO;

namespace Szolgáltatás_Orientált_Programozás_ZH2
{
    class Communication
    {
        TcpClient client = null;
        StreamReader SR = null;
        StreamWriter SW = null;

        public Communication(TcpClient connect)
        {
            client = connect;
            SR = new StreamReader(connect.GetStream(), Encoding.UTF8);
            SW = new StreamWriter(connect.GetStream(), Encoding.UTF8);
        }

        public void StartCommunication()
        {
            Console.WriteLine(SR.ReadLine());
            string answer = string.Empty;
            string[] answerparameters;
            bool end = false;
            Console.WriteLine("Use the 'HELP' to see the commands.");
            while (!end)
            {
                SW.WriteLine(Console.ReadLine());
                SW.Flush();
                answer = SR.ReadLine();
                answerparameters = answer.Split('|');
                switch (answerparameters[0])
                {
                    case "Ok*":
                        Console.ForegroundColor = ConsoleColor.Red;
                        while (answer != "Ok!")
                        {
                            if (answer != "Ok*")
                                Console.WriteLine(answer);
                            answer = SR.ReadLine();

                        }
                        Console.ResetColor();
                        break;

                    case "dc":
                        end = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(answerparameters[0]);
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
