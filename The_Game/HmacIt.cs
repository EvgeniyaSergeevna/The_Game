using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace The_Game
{
    class HmacIt
    {
        private static short keySize = 128;
        private static byte[] secretKey = new byte[keySize / 8];
        private static RandomNumberGenerator generator = new RNGCryptoServiceProvider();

        private static string Convert(byte[] toConvert)
        {
            return BitConverter.ToString(toConvert).Replace("-", string.Empty);
        }
        internal static string FirstStep(string choice)
        {
            generator.GetBytes(secretKey);
            HMAC pcHMAC = new HMACSHA256(secretKey);
            var pcHASH = pcHMAC.ComputeHash(Encoding.Default.GetBytes(choice));
            Console.WriteLine($"HMAC: {Convert(pcHASH)}");
            return Convert(pcHMAC.Key);
        }
        internal static void ShowMeTheKey(string key)
        {
            Console.WriteLine($"Key: {key}");
        }
    }
}

