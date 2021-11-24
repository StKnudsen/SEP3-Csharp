﻿using System;

namespace BusinessServer.Services.Security
{
    public interface IHashCheck : IDisposable
    {
        string GenerateHash(string source);
        public bool Compare(string hashSum1, string hashSum2);
    }
}