using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerSwordAttack : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform attackOrigin;
    [SerializeField] float radius = 1.1f;
    [SerializeField] float forwardOffset = 1.0f;
    [SerializeField] LayerMask hitMask;
    [SerializeField] int damage = 20;
    [SerializeField] float cooldownSeconds = 0.5f;

    [SerializeField] Animator animator;

    float nextAttackTime;
    InputAction attackAction;
    readonly Collider[] results = new Collider[16];

    bool isAttacking;

    bool GizmoShow = true;

    void Awake()
    {
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();
        if (attackOrigin == null) attackOrigin = transform;
        if (animator == null) animator = GetComponent<Animator>();

        if (playerInput != null)
            attackAction = playerInput.actions["Attack"];
    }

    void OnEnable()
    {
        if (playerInput != null && attackAction == null)
            attackAction = playerInput.actions["Attack"];

        if (attackAction != null)
        {
            attackAction.performed -= OnAttack;
            attackAction.performed += OnAttack;
        }
    }

    void OnDisable()
    {
        if (attackAction != null)
            attackAction.performed -= OnAttack;
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + cooldownSeconds;

        if (animator != null)
            animator.SetTrigger("PlayerSwordAttack");
    }

    public void AnimEvent_AttackStart()
    {
        isAttacking = true;
    }

    public void AnEvent_DoDamageSafe()
    {
        AnimEvent_DoDamage();
    }

    public void AnimEvent_DoDamage()
    {
        if (attackOrigin == null) return;

       var baseCenter = attackOrigin.position + attackOrigin.forward * forwardOffset;

var bottom = baseCenter + Vector3.up * 0.2f;
var top    = baseCenter + Vector3.up * 2.0f;

var count = Physics.OverlapCapsuleNonAlloc(
    bottom,
    top,
    radius,
    results,
    ~0,
    QueryTriggerInteraction.Ignore
);

for (int i = 0; i < count; i++)
{
    var c = results[i];
    if (c == null) continue;

    var health = c.GetComponentInParent<Health>();
    if (health == null) continue;

    health.Damage(damage);
}

    }

    public void AnimEvent_AttackEnd()
    {
        isAttacking = false;
    }

    void EndAttackDebug()
    {
        isAttacking = false;
    }

    void OnDrawGizmos()
    {
        if (!GizmoShow) return;
        if (attackOrigin == null) return;

        var center = attackOrigin.position + attackOrigin.forward * forwardOffset;

        Gizmos.color = isAttacking ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(center, radius);
    }
}
