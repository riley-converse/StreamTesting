using System.Text;

namespace StreamTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath =
                "C:\\Users\\riley.k.converse\\source\\repos\\StreamTesting\\StreamTesting\\InputText.txt";

            /*FileStream inputStream = new FileStream(inputFilePath, FileMode.Open);
            // length
            int length = (int)inputStream.ReadByte();
            byte[] readBytes = new byte[length];
            inputStream.Read(readBytes, 0, readBytes.Length);
            inputStream.Close();
            Console.WriteLine(Encoding.Default.GetString(readBytes));*/

            ReadStreamInput(inputFilePath);

        }

        public static async void ReadStreamInput(string filePath)
        {
            byte[] unit;

            using (FileStream InputStream = File.OpenRead(filePath))
            {
                //Console.WriteLine(InputStream.Length);

                unit = new byte[InputStream.Length];

                await InputStream.ReadAsync(unit, 0, (int)InputStream.Length);
               
            }
            //Console.WriteLine(unit);
            string stringConversion = Encoding.UTF8.GetString(unit);

            Console.WriteLine(stringConversion);
        }
    }
}
