using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace K_Burns_JewelleryStore.Models
{
    //Hash (one-way) encryption
    //Uses hashes to verify the integrity of transmitted messages
    //Hash encryption uses a hash table that contains a hash function
    //Used for information that will not be decrypted or read
    //Hash algorithms: MD2, MD4, and MD5, SHA
    public class StringHasher
    {
        public static string SHA1(string text)
        {
            var algo = new SHA1Managed();
            var result = GenerateHashString(algo, text);
            return result;
        }

        public static string SHA256(string text)
        {
            var algo = new SHA256Managed();
            var result = GenerateHashString(algo, text);
            return result;
        }

        public static string SHA384(string text)
        {
            var algo = new SHA384Managed();
            var result = GenerateHashString(algo, text);
            return result;
        }

        public static string SHA512(string text)
        {
            var algo = new SHA512Managed();
            var result = GenerateHashString(algo, text);
            return result;
        }

        public static string MD5(string text)
        {
            var algo = new MD5CryptoServiceProvider();
            var result = GenerateHashString(algo, text);
            return result;
        }

        public static string GenerateHashString(HashAlgorithm algo, string text)
        {
            //Compute hash from text parameter
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));

            //Get has value in array of bytes
            var result = algo.Hash;

            //Return as hexadecimal string
            return string.Join(string.Empty, result.Select(x => x.ToString("x2")));
        }
    }
}