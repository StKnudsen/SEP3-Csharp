using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedLibrary.Models
{
    public class Group
    {
        public string Id { get; set; }
        public User GroupOwner { get; set; }
        public IList<User> Users { get; set; }

        public Group(){}
        
        public Group(User owner, string id)
        {
            GroupOwner = owner;
            Id = id;
            Users = new List<User>
            {
                GroupOwner
            };
        }
    }
}