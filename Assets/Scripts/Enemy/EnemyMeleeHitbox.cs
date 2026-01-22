using UnityEngine;

public sealed class EnemyMeleeHitbox : MonoBehaviour
{
    [SerializeField] int damage = 15;

    bool hasHitThisSwing;

    void OnEnable()
    {
        hasHitThisSwing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHitThisSwing) return;

        var hurtbox = other.GetComponentInParent<PlayerHurtbox>();
        if (hurtbox == null) return;

        hurtbox.ApplyDamage(damage);
        hasHitThisSwing = true;
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}