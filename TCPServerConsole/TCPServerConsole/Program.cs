using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

//some change to test branching
//another change actually...
namespace TCPServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            p.StartServer();

        }

        void StartServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8888);
            TcpClient client = new TcpClient();

            listener.Start();

            Console.WriteLine("Server started.");
            //Console.WriteLine("IP: " + IPAddress.)

            client = listener.AcceptTcpClient();

            Console.WriteLine("Accepted TCP client");
            int count = 0;

            while (true)
            {
                try
                {
                    NetworkStream stream = client.GetStream();

                    byte[] bytes = new byte[70025];
                    //byte[] bytes;

                    stream.Read(bytes, 0, (int)client.ReceiveBufferSize);

                    if (bytes.Length == 0)
                        break;

                    string msg = System.Text.Encoding.ASCII.GetString(bytes);
                    msg = msg.Substring(0, msg.IndexOf("Hank"));

                    Console.WriteLine("Received: " + msg);

                    bytes = System.Text.Encoding.ASCII.GetBytes("HSHank");

                    stream.Write(bytes, 0, bytes.Length);
                    count++;
                }
                catch(Exception exc)
                {
                    Console.WriteLine("Error: " + exc);
                    Console.WriteLine("fuck it");
                    break;
                }
            }

            client.Close();
            listener.Stop();
            Console.WriteLine("The end " + count);
            Console.ReadLine();

        }
    }
}
