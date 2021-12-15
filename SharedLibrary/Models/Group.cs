using System.Collections.Generic;


namespace SharedLibrary.Models
{
    public class Group
    {
        public int WaitingUsers { get; set; }
        public string Id { get; set; }
        public string SwipeType { get; set; }
        public User.User GroupOwner { get; set; }
        public IList<User.User> Users { get; set; }
        public IList<CustomPair> SwipeObject { get; set; }
        public IList<Vote> Votes { get; set; }

        public Group() { }
        
        public Group(User.User owner, string id)
        {
            GroupOwner = owner;
            Id = id;
            Users = new List<User.User>
            {
                GroupOwner
            };
            Votes = new List<Vote>();
        }
    }
}