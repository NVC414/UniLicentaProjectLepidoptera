using UnityEngine;

public class FloatingEnemyShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform muzzle;
    [SerializeField] private EnemyProjectile projectilePrefab;

    [Header("Aiming")]
    [SerializeField] private bool yawOnly = true;
    [SerializeField] private float turnSpeed = 360f;

    [Header("Shooting")]
    [SerializeField] private float fireInterval = 1.25f;
    [SerializeField] private float projectileSpeed = 12f;
    [SerializeField] private float maxRange = 30f;

    private float nextFireTime;

    private void Start()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) target = playerObj.transform;
        }
    }

    private void Update()
    {
        if (target == null || muzzle == null || projectilePrefab == null) return;

        Vector3 toTarget = target.position - transform.position;
        float dist = toTarget.magnitude;

        if (dist > 0.001f)
        {
            Vector3 lookDir = toTarget;
            if (yawOnly) lookDir.y = 0f;

            if (lookDir.sqrMagnitude > 0.0001f)
            {
                Quaternion desired = Quaternion.LookRotation(lookDir.normalized, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, turnSpeed * Time.deltaTime);
            }
        }

        if (Time.time >= nextFireTime && dist <= maxRange)
        {
            Fire();
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void Fire()
    {
        Vector3 aimPoint = target.position;
        Vector3 dir = (aimPoint - muzzle.position).normalized;

        EnemyProjectile p = Instantiate(projectilePrefab, muzzle.position, Quaternion.LookRotation(dir, Vector3.up));
        p.Launch(dir * projectileSpeed);
    }
}