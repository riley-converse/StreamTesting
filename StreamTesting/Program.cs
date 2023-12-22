using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace StreamTesting
{
    internal class Program
    {
        private static int _readAsync;

        static async Task Main(string[] args)
        {
            string inputFilePath =
                "C:\\Users\\riley.k.converse\\source\\repos\\StreamTesting\\StreamTesting\\InputText.txt";

            string outputFile = "C:\\Users\\riley.k.converse\\source\\repos\\StreamTesting\\StreamTesting\\OutputText.txt";

            /*FileStream inputStream = new FileStream(inputFilePath, FileMode.Open);
            // length
            int length = (int)inputStream.ReadByte();
            byte[] readBytes = new byte[length];
            inputStream.Read(readBytes, 0, readBytes.Length);
            inputStream.Close();
            Console.WriteLine(Encoding.Default.GetString(readBytes));*/

            //ReadStreamInput(inputFilePath);

            await ReadByByte(inputFilePath);

            // write to file
            string toWrite = "This is a test stream, checking to see if it writes.";
            //WriteStreamOutput(outputFile, toWrite);
            
            Console.WriteLine("\n\n\n\n");
        }

        public static async Task ReadByByte(string filePath)
        {
            byte[] bytes;

            await using (FileStream readStream = new FileStream(filePath,FileMode.Open))
            {

                bytes = new byte[readStream.ReadByte()];
               
                //Console.WriteLine(bytes.Length);
                //Console.WriteLine("byte" + sizeof(byte) + ": char" + sizeof(byte) + ": byte array ");
                int length = readStream.ReadByte();

                var cancelTokenSource = new CancellationTokenSource();
                var token = cancelTokenSource.Token;
                
                while ((_readAsync = await readStream.ReadAsync(bytes, 0, bytes.Length, token)) > 0)
                {

                    Console.Write(Encoding.UTF8.GetString(bytes, 0, _readAsync));
                   // bytes = new byte[readStream.ReadByte()];
                }

                Console.WriteLine();
            }
           
            
        }

        public static async void ReadStreamInput(string filePath)
        {
            byte[] unit;

            await using (FileStream InputStream = File.OpenRead(filePath))
            {
                

                unit = new byte[InputStream.Length];
                //Console.WriteLine(InputStream.Length);
                //Console.WriteLine(byte.MaxValue);

                await InputStream.ReadAsync(unit, 0, (int)InputStream.Length);
                Console.WriteLine(Encoding.UTF8.GetString(unit));
            }
            //Console.WriteLine(unit);
            string stringConversion = Encoding.UTF8.GetString(unit);

            

            //Console.WriteLine(stringConversion);
        }

        public static async void WriteStreamOutput(string filePath, string toWrite)
        {
            byte[] byteArray = new byte[toWrite.Length];

            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = (byte)toWrite[i];
            }
       
            ReadOnlyMemory<byte> outputBytes = new ReadOnlyMemory<byte>(byteArray);


            await using FileStream outputStream = File.OpenWrite(filePath);
            {

                await outputStream.WriteAsync(outputBytes);
            }
        }
    }
}
