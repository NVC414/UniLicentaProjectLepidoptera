using UnityEngine;

public sealed class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] Health health;

    DeathScreenController deathScreen;

    void Awake()
    {
        if (health == null) health = GetComponent<Health>();
        deathScreen = FindFirstObjectByType<DeathScreenController>();

        health.OnDied += OnDied;
    }

    void OnDestroy()
    {
        if (health != null) health.OnDied -= OnDied;
    }

    void OnDied()
    {
        if (deathScreen == null)
            deathScreen = FindFirstObjectByType<DeathScreenController>();

        if (deathScreen != null)
            deathScreen.Show();
    }
}