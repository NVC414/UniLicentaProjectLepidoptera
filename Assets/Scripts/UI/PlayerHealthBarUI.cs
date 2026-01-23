using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] Image fillImage;

    Health health;

    void OnEnable()
    {
        TryBind();
    }

    void OnDisable()
    {
        Unbind();
    }

    void TryBind()
    {
        if (health != null) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        health = player.GetComponent<Health>();
        if (health == null) return;

        health.OnChanged += HandleChanged;
        HandleChanged(health.Current, health.Max);
    }

    void Unbind()
    {
        if (health == null) return;
        health.OnChanged -= HandleChanged;
        health = null;
    }

    void HandleChanged(int current, int max)
    {
        if (fillImage == null) return;
        fillImage.fillAmount = max <= 0 ? 0f : (float)current / max;
    }
}