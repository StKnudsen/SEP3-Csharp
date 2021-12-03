namespace SharedLibrary.Models
{
    public class Vote
    {
        public int SwipeObjectId { get; private set; }
        public int Votes { get; set; }

        public Vote() { }

        public Vote(int swipeObjectId)
        {
            SwipeObjectId = swipeObjectId;
            Votes = 1;
        }
    }
}