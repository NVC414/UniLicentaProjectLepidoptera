using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFireballLauncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera povCamera;
    [SerializeField] private FireballProjectile fireballPrefab;

    [Header("Launch")]
    [SerializeField] private float speed = 18f;
    [SerializeField] private float spawnDistance = 0.6f;

    [Header("Cooldown")]
    [SerializeField] private float cooldownSeconds = 5f;

    [Header("Input")]
    [SerializeField] private InputActionReference fireballAction;

    private float nextReadyTime;

    public bool IsReady => Time.time >= nextReadyTime;
    public float Cooldown01
    {
        get
        {
            if (IsReady) return 1f;
            var remaining = nextReadyTime - Time.time;
            return Mathf.Clamp01(1f - (remaining / cooldownSeconds));
        }
    }

    private void OnEnable()
    {
        if (fireballAction != null) fireballAction.action.Enable();
    }

    private void OnDisable()
    {
        if (fireballAction != null) fireballAction.action.Disable();
    }

    private void Update()
    {
        if (fireballAction == null) return;
        if (!fireballAction.action.WasPressedThisFrame()) return;

        TryFire();
    }

    public bool TryFire()
    {
        if (!IsReady) return false;
        if (povCamera == null) return false;
        if (fireballPrefab == null) return false;

        var camT = povCamera.transform;
        var spawnPos = camT.position + camT.forward * spawnDistance;

        var proj = Instantiate(fireballPrefab, spawnPos, Quaternion.LookRotation(camT.forward, Vector3.up));
        proj.Init(gameObject);

        var rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = camT.forward * speed;
        }

        nextReadyTime = Time.time + cooldownSeconds;
        return true;
    }
}
