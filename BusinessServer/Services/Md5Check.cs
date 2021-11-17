using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessServer.Services
{
    public class Md5Check : IHashCheck
    {
        public string GenerateHash(string source)
        {
            byte[] hash;

            MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
            
            hash = cryptoServiceProvider.ComputeHash(
                ASCIIEncoding.ASCII.GetBytes(source)
                );
            cryptoServiceProvider.Dispose(); //Af sikkerhedsmessige årsager skal denne dumpes!
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            
            return sb.ToString();
        }

        public bool Compare(string hashSum1, string hashSum2)
        {
            return hashSum1.Equals(hashSum2);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}