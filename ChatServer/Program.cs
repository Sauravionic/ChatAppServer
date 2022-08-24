using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ChatServer
{
    class Program
    {
        static TcpListener tcpListener;
        static List<Client> clientList;
        static TcpClient tcpClient;

        public static void messageTransaction()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("10.123.129.8"), 7892);
            tcpListener.Start();
            Console.WriteLine("Chatting server started");
            String messageTosend = "";
            while(true)
            {
                Socket socket = tcpListener.AcceptSocket();
                if(socket.Connected)
                {
                    try
                    {
                        //Receive Chat from user
                        NetworkStream ns = new NetworkStream(socket);
                        StreamReader sr = new StreamReader(ns);
                        string message = sr.ReadLine();
                        messageTosend = message;
                        Console.WriteLine(message);
                    }
                    catch(Exception e)
                    {

                    }
                    //Send chat back to client
                    try
                    {
                        NetworkStream ns = new NetworkStream(socket);
                        StreamWriter sw = new StreamWriter(ns);
                        Console.WriteLine("Sending chat to user");
                        sw.WriteLine(messageTosend);
                        sw.Flush();
                    }
                    catch(Exception e)
                    {

                    }

                }
            }
        }
        static void Main(string[] args)
        {
            clientList = new List<Client>();
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse("10.123.129.8"), 7891);
                tcpListener.Start();
                Console.WriteLine("Server started and waiting for clients");
                while (true)
                {

                    Socket socektForCLients = tcpListener.AcceptSocket();
                    if (socektForCLients.Connected)
                    {
                        //Sending Welcome Client Message to Client
                        NetworkStream ns = new NetworkStream(socektForCLients);
                        StreamWriter sw = new StreamWriter(ns);
                        Console.WriteLine("Welcome Client");
                        sw.WriteLine("Welcome Client");
                        sw.Flush();


                        //Reading Username from Client
                        StreamReader sr = new StreamReader(ns);
                        string username = sr.ReadLine();
                        Console.WriteLine(username);
                        username = username.Remove(0, 12);


                        //Storing username in ClientList
                        Client client = new Client(username);
                        clientList.Add(client);
                        //Close networkstream and streamwriter and socket;
                        sw.Close();
                        ns.Close();
                        socektForCLients.Close();

                        messageTransaction();
                    }  
                }
            }
            catch(Exception e) { }
        }
    }
}













                /*
                while (true)
                {

                    //Receive the data
                    tcpClient = tcpListener.AcceptTcpClient();
                    Client client = new Client(tcpClient);
                    clientList.Add(client);

                    
                    // Broadcast the connection to everyone on the server.
                    broadCastConnection();


                    //Receive the message on the server

                    //String messageReceived = receiveMessage();
                    //string fullMessage = client.username + " has sent " + messageReceived;
                    //Console.WriteLine("[ " + DateTime.Now + " ]" + fullMessage);


                    //BroadCast message to everyone

                   // broadcastMessage(fullMessage);

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e.Message + "poi");
            }
        }
        
        static void broadCastConnection()
        { 
             IPEndPoint ipClient = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7895);
             Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

             socket.Bind(ipClient);
             socket.Listen(10);

             Socket clientClient = socket.Accept();
             IPEndPoint clientep = (IPEndPoint)clientClient.RemoteEndPoint;

             string dataSend = "";
             for (int i = 0; i < clientList.Count; i++)
             {
                 if (i == clientList.Count - 1)
                 {
                     dataSend += clientList[i].username;
                 }
                 else
                 {
                     dataSend += clientList[i].username + System.Environment.NewLine;
                 }
             }

             byte[] data = new byte[1024];
             data = Encoding.ASCII.GetBytes(dataSend);
             clientClient.Send(data, data.Length, SocketFlags.None);
             socket.Close();
            
        }

        static string receiveMessage()
        {
            return null;
        }

        static void broadcastMessage(string message)
        {
            
        }
               
    }
}
                */