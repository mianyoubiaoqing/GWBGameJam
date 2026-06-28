namespace GWBGameJam
{
    public readonly struct OnGameStateChanged
    {
        public readonly GameState NewState;
        public readonly GameState PreviousState;
        public OnGameStateChanged(GameState newState, GameState previousState)
        {
            NewState = newState;
            PreviousState = previousState;
        }
    }

    public readonly struct OnLevelStarted
    {
        public readonly int LevelIndex;
        public OnLevelStarted(int levelIndex) { LevelIndex = levelIndex; }
    }
}
