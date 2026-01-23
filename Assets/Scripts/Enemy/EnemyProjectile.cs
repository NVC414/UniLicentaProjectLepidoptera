using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    [SerializeField] int damage = 10;

    Rigidbody rb;
    bool hasHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        var hurtbox = other.GetComponentInParent<PlayerHurtbox>();
        if (hurtbox != null)
            hurtbox.ApplyDamage(damage);

        Destroy(gameObject);
    }
}
