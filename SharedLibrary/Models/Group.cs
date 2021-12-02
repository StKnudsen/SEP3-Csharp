using System.Collections.Generic;

namespace SharedLibrary.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string SwipeType { get; set; }
        public User GroupOwner { get; set; }
        public IList<User> Users { get; set; }
        public IList<CustomPair> SwipeObject { get; set; }

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