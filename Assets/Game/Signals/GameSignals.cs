namespace Game.Signals
{
    public class UpdateScoreSignal : Signal<int>
    {
    }

    public class DisplayScoreSignal : Signal<int>
    {
    }

    public class UpdatePlayerLivesSignal : Signal<int>
    {
    }

    public class PlayerDiedSignal : Signal
    {
    }

    public class PlayerReviveSignal : Signal
    {
    }
}