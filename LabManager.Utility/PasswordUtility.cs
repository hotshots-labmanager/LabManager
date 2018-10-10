using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;

namespace LabManager.Utility
{
    public class PasswordUtility
    {
        private const int SaltByteSize = 64;
        private const int HashByteSize = 64;
        private const int Iterations = 127200;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int HashIndex = 2;
        private const char Separator = ':';

        public static String HashPassword(String password)
        {
            byte[] salt = GetSalt(SaltByteSize);
            byte[] hash = GetHash(password, salt, Iterations, HashByteSize);

            String separatorAsString = Convert.ToString(Separator);
            return Iterations + separatorAsString + Convert.ToBase64String(salt) + separatorAsString + Convert.ToBase64String(hash);
        }

        private static byte[] GetHash(string password, byte[] salt, int iterations, int hashByteSize)
        {
            var pdb = new Pkcs5S2ParametersGenerator(new Sha256Digest());
            pdb.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()), salt, iterations);
            var key = (KeyParameter)pdb.GenerateDerivedMacParameters(hashByteSize * 8);
            return key.GetKey();
        }

        private static byte[] GetSalt(int size)
        {
            byte[] salt = new byte[size];
            SecureRandom cryptoRandom = new SecureRandom();
            cryptoRandom.NextBytes(salt);
            return salt;
        }

        public static bool ValidatePassword(String password, String correctHash)
        {
            String[] split = correctHash.Split(Separator);
            int iterations = Int32.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[HashIndex]);

            byte[] testHash = GetHash(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }
    }
}
