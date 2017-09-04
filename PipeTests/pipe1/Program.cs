using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipe1
{
    class Program
    {
        static void Main(string[] args)
        {
            SendByteAndReceiveResponse();
        }

        private static void SendByteAndReceiveResponse()
        {
            using (NamedPipeServerStream namedPipeServer = new NamedPipeServerStream("test-pipe"))
            {
                namedPipeServer.WaitForConnection();
                namedPipeServer.WriteByte(1);
                int byteFromClient = namedPipeServer.ReadByte();
                Console.WriteLine(byteFromClient);
                Console.Read();
            }
        }
    }
}
