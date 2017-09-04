using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program2
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] message;
            int size;

            //Получение существующего участка разделяемой памяти
            //Параметр - название участка
            var sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");

            //Сначала считываем размер сообщения, чтобы создать массив данного размера
            //Integer занимает 4 байта, начинается с первого байта, поэтому передаем цифры 0 и 4
            var reader = sharedMemory.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read);
            size = reader.ReadInt32(0);

            reader = sharedMemory.CreateViewAccessor(4, size * 2, MemoryMappedFileAccess.Read);
            message = new char[size];
            reader.ReadArray(0, message, 0, size);

            Console.WriteLine($"Message: {new string(message)}");
            Console.WriteLine("Prs any key");
            Console.Read();

        }
    }
}
