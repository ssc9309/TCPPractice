using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace TCPClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            p.SendMessage();
            

        }

        void SendMessage()
        {
            TcpClient client = new TcpClient();

            client.Connect("192.168.1.15", 8888);

            NetworkStream ns = client.GetStream();

            Console.WriteLine("Enter message. q to quit");

            
            string input = "";
            /*
            while (input != "q")
            {

                input = Console.ReadLine();

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input + "Hank");

                ns.Write(bytes, 0, bytes.Length);
                ns.Flush();
            }
            */

            for (int x= 0; x<1000; x++)
            {
                input = "I hate shakespeare. 16843215846351358463818438483768438463";
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input + "Hank");

                ns.Write(bytes, 0, bytes.Length);
                ns.Flush();

                byte[] bytes2 = new byte[70000];

                ns.Read(bytes2, 0, client.ReceiveBufferSize);

                string msg = System.Text.Encoding.ASCII.GetString(bytes2);
                msg = msg.Substring(0, msg.IndexOf("Hank"));

                Console.WriteLine(msg);
            }

            ns.Close();
            client.Close();
            
        }
    }
}
