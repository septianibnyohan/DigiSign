using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiSign.Helpers
{
    public class RandomGenerator
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public static string RandomString(int length, bool lowerCase = false)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            lock (syncLock)
            {
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}