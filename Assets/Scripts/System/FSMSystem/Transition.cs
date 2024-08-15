/// <summary>
/// Place the labels for the Transitions in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum Transition
{
    NullTransition = 0, // Use this transition to represent a non-existing transition in your system
    // ReadyState
    StartGame,
    LoadGame,
    ContinueGame,

    // FightState
    Pause,
    WaveEnd,
    PlayerDie,
    Clear20Wave,

    // PauseState
    Restart,

    // BuyState
    BuyEnd,
    BackMenu,

    // GameoverState
    Confirm,

    // GameClearState
    Endless,
    // Confirm,

}
