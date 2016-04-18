using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace ActiveLearning.Business.Common
{
    public class Util
    {
        //public static void CopyNonNullProperty(ref object from, ref object to)
        //{
        //    if (from == null || to == null) return;
        //    PropertyInfo[] allProps = from.GetType().GetProperties();
        //    PropertyInfo toProp;
        //    foreach (PropertyInfo fromProp in allProps)
        //    {
        //        //find property on "to" with same name

        //        toProp = to.GetType().GetProperty(fromProp.Name);
        //        if (toProp == null) continue; //not here
        //        if (!toProp.CanWrite) continue; //only if writeable
        //                                        //set the property value from "from" to "to"
        //                                        // Debug.WriteLine(toProp.Name + " = from." + fromProp.Name + ";");
        //        if (fromProp.GetValue(from, null) == null) continue;
        //        toProp.SetValue(to, fromProp.GetValue(from, null), null);
        //    }
        //}

        public static void CopyNonNullProperty(object from, object to)
        {
            if (from == null || to == null) return;
            PropertyInfo[] allProps = from.GetType().GetProperties();
            PropertyInfo toProp;
            foreach (PropertyInfo fromProp in allProps)
            {
                //find property on "to" with same name

                toProp = to.GetType().GetProperty(fromProp.Name);
                if (toProp == null) continue; //not here
                if (!toProp.CanWrite) continue; //only if writeable
                                                //set the property value from "from" to "to"
                                                // Debug.WriteLine(toProp.Name + " = from." + fromProp.Name + ";");
                if (fromProp.GetValue(from, null) == null) continue;
                toProp.SetValue(to, fromProp.GetValue(from, null), null);
            }
        }

        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;


        public static string GenerateSalt()
        {
            // Generate a random salt
            byte[] salt = new byte[SALT_BYTE_SIZE];
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            csprng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string CreateHash(string passwordStr, string saltStr)
        {
            // Hash the password and encode the parameters
            byte[] salt = Convert.FromBase64String(saltStr);
            byte[] hash = PBKDF2(passwordStr, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return Convert.ToBase64String(hash);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = Int32.Parse(split[ITERATION_INDEX]);
            byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
