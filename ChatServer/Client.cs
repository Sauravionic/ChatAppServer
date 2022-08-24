using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ChatServer
{
    class Client
    {
        public string username { get; set; }
        public Guid UID { get; set; }
        public TcpClient tcpClient { get; set; }
        public Client(string username)
        {
            UID = Guid.NewGuid();
            this.username = username;
        }

    }
}
