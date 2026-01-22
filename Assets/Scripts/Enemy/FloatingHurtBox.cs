using UnityEngine;

public sealed class FloatingHurtBox : MonoBehaviour, IEnemyHurtbox
{
    [SerializeField] Health health;
    [SerializeField] int damageMultiplier = 1;

    void Awake()
    {
        if (health == null) health = GetComponentInParent<Health>();
    }

    public void ApplyDamage(int amount)
    {
        if (health == null) return;
        health.Damage(amount * damageMultiplier);
    }
}