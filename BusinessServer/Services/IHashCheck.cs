using System;

namespace BusinessServer.Services
{
    public interface IHashCheck : IDisposable
    {
        public bool Compare(string hashSum1, string hashSum2);
    }
}