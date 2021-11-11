using System;

namespace BusinessServer.Services
{
    public class Md5Check : IHashCheck
    {

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