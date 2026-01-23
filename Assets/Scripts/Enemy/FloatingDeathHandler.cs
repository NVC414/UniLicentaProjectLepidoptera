using UnityEngine;

public sealed class FloatingDeathHandler : MonoBehaviour
{
    [SerializeField] GameObject rootToDestroy;
    [SerializeField] float delay = 0f;

    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        if (rootToDestroy == null) rootToDestroy = gameObject;
    }

    void OnEnable()
    {
        if (health != null) health.OnDied += HandleDied;
    }

    void OnDisable()
    {
        if (health != null) health.OnDied -= HandleDied;
    }

   void HandleDied()
{
    if (HUDController.Instance != null)
        HUDController.Instance.AddKill();

    if (delay <= 0f) Destroy(rootToDestroy);
    else Destroy(rootToDestroy, delay);
}

}
