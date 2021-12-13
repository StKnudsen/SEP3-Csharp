﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Network.Group
{
    public interface IGroupDataLink
    {
        Task<List<CustomPair>> GetShuffledRecipesAsync();
        Task<List<CustomPair>> GetShuffledRestaurantsAsync();
    }
}