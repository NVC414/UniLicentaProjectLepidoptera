using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerPauseToggle : MonoBehaviour
{
    [SerializeField] PlayerLook look;

    bool paused;

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        paused = !paused;
        look.SetLookEnabled(!paused);
        look.SetCursorForUI(paused);
    }
}