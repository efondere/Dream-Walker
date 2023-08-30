using UnityEngine;

public class Pausable: MonoBehaviour
{
    private void Start()
    {
        PauseManager.Register(this);
    }

    private void OnDestroy()
    {
        PauseManager.Remove(this);
    }

    public void TriggerPause()
    {
        gameObject.SendMessage("OnPause");
    }

    public void TriggerResume()
    {
        gameObject.SendMessage("OnResume");
    }
}
