namespace GWBGameJam
{
    public readonly struct OnLaneHoverChanged
    {
        public readonly int LaneIndex; // -1 = 无悬停
        public OnLaneHoverChanged(int laneIndex) { LaneIndex = laneIndex; }
    }
}
