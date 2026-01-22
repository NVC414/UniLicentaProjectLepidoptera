using UnityEngine;

public sealed class EnemyDamager : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float cooldownSeconds = 0.5f;

    float nextDamageTime;

    void OnTriggerStay(Collider other)
    {
        if (Time.time < nextDamageTime) return;

        var hurtbox = other.GetComponentInParent<PlayerHurtbox>();
        if (hurtbox == null) return;

        hurtbox.ApplyDamage(damage);
        nextDamageTime = Time.time + cooldownSeconds;
    }
}