using UnityEngine;
using UnityEngine.InputSystem;

public sealed class DeathScreenController : MonoBehaviour
{
    [SerializeField] GameObject root;

    PlayerLook look;
    PlayerInput playerInput;
    MonoBehaviour[] disableGameplay;

    void Awake()
    {
        if (root != null) root.SetActive(false);
        BindPlayer();
    }

    void BindPlayer()
    {
        var player = FindFirstObjectByType<PlayerLook>();
        if (player == null) return;

        look = player;
        playerInput = player.GetComponent<PlayerInput>();

        disableGameplay = new MonoBehaviour[]
        {
            player.GetComponent<PlayerMotor>(),
            player.GetComponent<PlayerLook>(),
            player.GetComponent<PlayerPauseToggle>()
        };
    }

    public void Show()
    {
        if (root != null) root.SetActive(true);

        if (disableGameplay != null)
            for (int i = 0; i < disableGameplay.Length; i++)
                if (disableGameplay[i] != null)
                    disableGameplay[i].enabled = false;

        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("UI");

        if (look != null)
        {
            look.SetLookEnabled(false);
            look.SetCursorForUI(true);
        }
    }

    public void ReloadLevel()
    {
        SceneLoader.ReloadActive();
    }

    public void GoToMenu()
    {
        SceneLoader.Load("MainMenu");
    }
}