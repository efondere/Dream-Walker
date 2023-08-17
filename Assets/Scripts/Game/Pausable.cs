using UnityEngine;

public abstract class Pausable: MonoBehaviour
{
    public bool isPaused;
    
    protected Pausable()
    {
        PauseManager.Register(this);
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }
}
