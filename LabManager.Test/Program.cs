using LabManager.Utility;
using System;

namespace LabManager.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            String hash = PasswordUtility.HashPassword("abc");

            Console.WriteLine(hash);

            Console.ReadKey();
        }
    }
}
