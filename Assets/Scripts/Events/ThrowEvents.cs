namespace GWBGameJam
{
    public readonly struct OnThrowStarted
    {
        public readonly int LaneIndex;
        public OnThrowStarted(int laneIndex) { LaneIndex = laneIndex; }
    }

    public readonly struct OnThrowCompleted
    {
        public readonly int LaneIndex;
        public readonly ThrowResult Result;
        public OnThrowCompleted(int laneIndex, ThrowResult result) { LaneIndex = laneIndex; Result = result; }
    }
}
