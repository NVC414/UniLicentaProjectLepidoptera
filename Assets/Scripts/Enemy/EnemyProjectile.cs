using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}