using System.Collections.Generic;

public class PauseManager
{
    private static List<Pausable> _pausables = new();
    private static bool _isPaused = false;

    public static bool TogglePause()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }

        _isPaused = !_isPaused;
        return _isPaused;
    }

    public static bool IsPaused()
    {
        return _isPaused;
    }

    public static void Pause()
    {
        if (_isPaused)
            return;

        _isPaused = true;
        foreach (var p in _pausables)
        {
            p.TriggerPause();
        }
    }

    public static void Resume()
    {
        if (!_isPaused)
            return;

        _isPaused = false;
        foreach (var p in _pausables)
        {
            p.TriggerResume();
        }
    }

    public static void Register(Pausable p)
    {
        _pausables.Add(p);
    }

    public static void Remove(Pausable p)
    {
        _pausables.Remove(p);
    }
}
