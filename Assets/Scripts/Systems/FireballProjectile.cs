using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float lifeSeconds = 3f;
    [SerializeField] private LayerMask hitMask = ~0;

    private GameObject owner;

    public void Init(GameObject ownerObj)
    {
        owner = ownerObj;
        Destroy(gameObject, lifeSeconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner != null)
        {
            if (other.GetComponentInParent<Transform>() != null)
            {
                if (other.transform.IsChildOf(owner.transform)) return;
            }
        }

        if (((1 << other.gameObject.layer) & hitMask.value) == 0) return;

        var health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.Damage(damage);
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}