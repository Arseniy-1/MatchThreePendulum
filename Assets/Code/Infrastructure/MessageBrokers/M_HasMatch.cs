namespace Code.Infrastructure.MessageBrokers
{
    public struct M_HasMatch
    {    
        public M_HasMatch(BallTypes matchType)
        {
            MatchType = matchType;
        }
    
        public BallTypes MatchType { get; private set; }
    }
}