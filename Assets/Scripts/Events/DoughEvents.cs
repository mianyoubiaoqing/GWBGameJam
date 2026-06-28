namespace GWBGameJam
{
    public readonly struct OnDoughStateChanged
    {
        public readonly DoughState NewState;
        public readonly DoughState PreviousState;
        public OnDoughStateChanged(DoughState newState, DoughState previousState)
        {
            NewState = newState;
            PreviousState = previousState;
        }
    }
}
