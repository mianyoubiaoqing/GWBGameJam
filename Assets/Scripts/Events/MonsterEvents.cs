namespace GWBGameJam
{
    public readonly struct OnMonsterSpawned
    {
        public readonly int LaneIndex;
        public readonly MonsterData Data;
        public OnMonsterSpawned(int laneIndex, MonsterData data) { LaneIndex = laneIndex; Data = data; }
    }

    public readonly struct OnMonsterDefeated
    {
        public readonly int LaneIndex;
        public OnMonsterDefeated(int laneIndex) { LaneIndex = laneIndex; }
    }

    public readonly struct OnMonsterReachedTable
    {
        public readonly int LaneIndex;
        public OnMonsterReachedTable(int laneIndex) { LaneIndex = laneIndex; }
    }
}
