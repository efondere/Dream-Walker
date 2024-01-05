using UnityEngine;

// Use [RequireComponent(typeof(Pausable))] in all the classes that whose behaviour can be paused
// then, the OnPause() and OnResume() methods will be called when necessary. This is a separate
// MonoBehaviour that can be added on the GameObjects.
public class Pausable : MonoBehaviour
{
    private void Awake()
    {
        PauseManager.Register(this);
    }

    private void OnDestroy()
    {
        PauseManager.Remove(this);
    }

    // Called by PauseManager
    public void TriggerPause()
    {
        gameObject.SendMessage("OnPause");
    }

    // Called by PauseManager
    public void TriggerResume()
    {
        gameObject.SendMessage("OnResume");
    }
}
