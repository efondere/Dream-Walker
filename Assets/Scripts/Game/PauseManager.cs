using System.Collections.Generic;

public class PauseManager
{
    private static List<Pausable> _pausables = new();
    private static bool _isPaused = false;

    // TODO: trigger pause from InputManager
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
            p.isPaused = true;
            p.OnPause();
        }
    }

    public static void Resume()
    {
        if (!_isPaused)
            return;
        
        _isPaused = false;
        foreach (var p in _pausables)
        {
            p.isPaused = false;
            p.OnResume();
        }
    }

    public static void Register(Pausable p)
    {
        _pausables.Add(p);
        p.isPaused = _isPaused;
    }
}
