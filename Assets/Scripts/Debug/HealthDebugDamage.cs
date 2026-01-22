using UnityEngine;
using UnityEngine.InputSystem;

public sealed class HealthDebugDamage : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] int damagePerPress = 10;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.hKey.wasPressedThisFrame)
            health.Damage(damagePerPress);
    }
}