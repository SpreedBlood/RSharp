using System;
using System.Security.Cryptography;

namespace RSharp.Network.Codec
{
    internal static class SecureRandom
    {
        private static readonly RNGCryptoServiceProvider _provider;

        static SecureRandom()
        {
            _provider = new RNGCryptoServiceProvider();
        }

        internal static long NextLong()
        {
            byte[] byteArray = new byte[8];
            _provider.GetBytes(byteArray);

            return BitConverter.ToInt64(byteArray, 0);
        }
    }
}
