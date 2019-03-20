using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KomunikatorKlient
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip_address = IPAddress.Parse("25.60.93.130");
            int port = 8080;
            try
            {
                Console.WriteLine($"Attempting to connect to server at IP address: {ip_address} port: {port}");
                TcpClient client = new TcpClient(ip_address.ToString(), port);
                Console.WriteLine("Connection successful!");
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                Thread listenerThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine(reader.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Connection with server lost");
                            return;
                        }
                    }
                });
                listenerThread.Start();

                string s = String.Empty;
                while (!s.Equals("Exit"))
                {
                    s = Console.ReadLine();
                    writer.WriteLine(s);
                    writer.Flush();
                }
                reader.Close();
                writer.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
