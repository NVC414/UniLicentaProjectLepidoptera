using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image fillImage;

    Health health;
    float nextTryTime;

    void OnEnable()
    {
        TryBind();
    }

    void OnDisable()
    {
        Unbind();
    }

    void Update()
    {
        if (health != null) return;
        if (Time.time < nextTryTime) return;
        nextTryTime = Time.time + 0.25f;
        TryBind();
    }

    void TryBind()
    {
        if (fillImage == null) return;

        var found = FindFirstObjectByType<Health>();
        if (found == null) return;

        health = found;
        health.OnChanged += OnHealthChanged;
        OnHealthChanged(health.Current, health.Max);
    }

    void Unbind()
    {
        if (health == null) return;
        health.OnChanged -= OnHealthChanged;
        health = null;
    }

    void OnHealthChanged(int current, int max)
    {
        if (fillImage == null) return;
        fillImage.fillAmount = max > 0 ? (float)current / max : 0f;
    }
}
