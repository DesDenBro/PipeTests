using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write message");
            char[] message = Console.ReadLine().ToCharArray();
            int size = message.Length;

            //Создание участка разделяемой памяти
            //Первый параметр - название участка, 
            //второй - длина участка памяти в байтах: тип char  занимает 2 байта 
            //плюс четыре байта для одного объекта типа Integer
            MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", size * 2 + 4);
            //Создаем объект для записи в разделяемый участок памяти
            var writer = sharedMemory.CreateViewAccessor(0, size * 2 + 4);
            writer.Write(0, size);
            writer.WriteArray<char>(4, message, 0, size);

            Console.WriteLine("Wrote in shared memory");
            Console.WriteLine("Prs eny key");
            Console.Read();
        }
    }
}
