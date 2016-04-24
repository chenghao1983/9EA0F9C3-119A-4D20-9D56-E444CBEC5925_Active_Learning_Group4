using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ActiveLearning.DB;
using System.Collections.Generic;
using AutoMapper;

namespace ActiveLearning.Business.Common
{
    public class Util
    {
        #region Copy values
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
        public static void CopyNonNullProperty(object objFrom, object objTo)
        {
            List<Type> typeList = new List<Type>() { typeof(byte), typeof(sbyte), typeof(int), typeof(uint), typeof(short), typeof(ushort), typeof(long), typeof(ulong)
            , typeof(float) , typeof(double),  typeof(char) ,  typeof(bool),  typeof(string), typeof(decimal) ,  typeof(DateTime), typeof(DateTime?), typeof(Enum),  typeof(Guid),
            typeof(IntPtr), typeof(TimeSpan),  typeof(UIntPtr) };
            if (objFrom == null || objTo == null) return;
            PropertyInfo[] allProps = objFrom.GetType().GetProperties();
            PropertyInfo toProp;
            foreach (PropertyInfo fromProp in allProps)
            {
                if (typeList.Contains(fromProp.PropertyType))
                {
                    //find property on "to" with same name

                    toProp = objTo.GetType().GetProperty(fromProp.Name);
                    if (toProp == null) continue; //not here
                    if (!toProp.CanWrite) continue; //only if writeable
                                                    //set the property value from "from" to "to"
                                                    // Debug.WriteLine(toProp.Name + " = from." + fromProp.Name + ";");
                    if (fromProp.GetValue(objFrom, null) == null) continue;
                    toProp.SetValue(objTo, fromProp.GetValue(objFrom, null), null);
                }
            }


            //PropertyInfo[] propertyFrom = objFrom.GetType().GetProperties();
            //PropertyInfo[] propertyTo = objTo.GetType().GetProperties();
            //foreach (PropertyInfo pTo in propertyTo)
            //{
            //    if (pTo.CanWrite)
            //    {
            //        foreach (PropertyInfo pFrom in propertyFrom)
            //        {
            //            if (pTo.Name == pFrom.Name && pTo.PropertyType == pFrom.PropertyType)
            //            {
            //                object value = pFrom.GetValue(objFrom, null);
            //                if (value != null)
            //                    pTo.SetValue(objTo, value, null);
            //                else if (pTo.PropertyType == typeof(Nullable))
            //                    pTo.SetValue(objTo, value, null);
            //                else if (value == null)
            //                    pTo.SetValue(objTo, null, null);


            //                break;
            //            }
            //        }
            //    }
            //}
        }
        #endregion

        #region Hash Password

        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        //public const int ITERATION_INDEX = 0;
        //public const int SALT_INDEX = 1;
        //public const int PBKDF2_INDEX = 2;

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

        public static bool ValidatePassword(string password, string correctHash, string correctSalt)
        {
            // Extract the parameters from the hash
            //char[] delimiter = { ':' };
            //string[] split = correctHash.Split(delimiter);
            //int iterations = Int32.Parse(split[ITERATION_INDEX]);
            //byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] salt = Convert.FromBase64String(correctSalt);
            //byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            byte[] hash = Convert.FromBase64String(correctHash);

            byte[] testHash = PBKDF2(password, salt, PBKDF2_ITERATIONS, hash.Length);
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
        #endregion
    }
}
