namespace GWBGameJam
{
    public readonly struct OnBakingStateChanged
    {
        public readonly BakingState NewState;
        public readonly BakingState PreviousState;
        public OnBakingStateChanged(BakingState newState, BakingState previousState)
        {
            NewState = newState;
            PreviousState = previousState;
        }
    }

    public readonly struct OnThrowRequested
    {
        public readonly int LaneIndex;
        public OnThrowRequested(int laneIndex) { LaneIndex = laneIndex; }
    }
}
