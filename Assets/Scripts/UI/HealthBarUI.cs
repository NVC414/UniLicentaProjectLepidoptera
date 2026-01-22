using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image fillImage;

    Health health;

    void OnEnable()
    {
        TryBind();
    }

    void OnDisable()
    {
        if (health != null) health.OnChanged -= OnHealthChanged;
    }

    void TryBind()
    {
        if (health != null) return;

        health = FindFirstObjectByType<Health>();
        if (health == null) return;

        health.OnChanged += OnHealthChanged;
        OnHealthChanged(health.Current, health.Max);
    }

    void OnHealthChanged(int current, int max)
    {
        if (fillImage == null) return;
        fillImage.fillAmount = max > 0 ? (float)current / max : 0f;
    }
}