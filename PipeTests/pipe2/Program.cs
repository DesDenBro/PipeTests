using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipe2
{
    class Program
    {
        static void Main(string[] args)
        {
            ReceiveByteAndRespond();
        }

        private static void ReceiveByteAndRespond()
        {
            using (NamedPipeClientStream namedPipeClient = new NamedPipeClientStream("test-pipe"))
            {
                namedPipeClient.Connect();
                Console.WriteLine(namedPipeClient.ReadByte());
                namedPipeClient.WriteByte(2);
                Console.Read();
            }
        }
    }
}
