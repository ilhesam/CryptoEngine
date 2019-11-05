using System;
using System.Xml;

namespace CryptoEngine
{
    class Program
    {
        private static readonly ICrypto Crypto = new Crypto();

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter operation number :");
            Console.WriteLine("1 - Encrypt");
            Console.WriteLine("2 - Decrypt");
            var opNum = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter text...");
            var text = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            switch (opNum)
            {
                case 1:
                {
                    Console.WriteLine(Crypto.Encrypt(text, "dflt-3dd8-yolo74"));
                    break;
                }

                case 2:
                {
                    Console.WriteLine(Crypto.Decrypt(text, "dflt-3dd8-yolo74"));
                    break;
                }

                default:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Operation number out of range...");
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}
