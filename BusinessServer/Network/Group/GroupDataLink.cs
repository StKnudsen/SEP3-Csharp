﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Network.Group
{
    public class GroupDataLink : IGroupDataLink
    {
        private readonly string uri = "http://localhost:8080/group";
        
        public async Task<List<CustomPair>> GetShuffledRecipesAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/recipes");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string listAsJson = await response.Content.ReadAsStringAsync();
            List<CustomPair> list = JsonSerializer.Deserialize<List<CustomPair>>(listAsJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return list;
        }

        public async Task<List<CustomPair>> GetShuffledRestaurantsAsync()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/restaurants");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"StatusCode: {response.StatusCode}");
            }

            string listAsJson = await response.Content.ReadAsStringAsync();
            List<CustomPair> list = JsonSerializer.Deserialize<List<CustomPair>>(listAsJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return list;
        }
    }
}