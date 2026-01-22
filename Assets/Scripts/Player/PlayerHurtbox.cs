using UnityEngine;

public sealed class PlayerHurtbox : MonoBehaviour
{
    [SerializeField] Health health;

    void Awake()
    {
        if (health == null) health = GetComponent<Health>();
    }

    public void ApplyDamage(int amount)
    {
        if (health != null) health.Damage(amount);
    }
}