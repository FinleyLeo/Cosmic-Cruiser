using System;
public static class GameEvents
{
    public static event Action OnLevelStart;
    public static event Action OnGameOver;
    public static event Action OnLevelReset;
    public static event Action OnGameWin;

    public static void InvokeLevelStarted() => OnLevelStart?.Invoke();
    public static void InvokeLevelCompleted() => OnGameWin?.Invoke();
    public static void InvokeLevelRestarted() => OnLevelReset?.Invoke();
    public static void InvokeLevelFailed()
    {
        OnGameOver?.Invoke();
        InvokeLevelRestarted();
    }
}
