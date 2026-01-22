using UnityEngine;
using UnityEngine.AI;

public sealed class MeleeEnemyAttacker : MonoBehaviour
{
    [Header("Ranges")]
    [SerializeField] float aggroRange = 12f;
    [SerializeField] float attackRange = 1.8f;

    [Header("Repath")]
    [SerializeField] float repathInterval = 0.15f;

    [Header("Cooldown")]
    [SerializeField] float cooldownSeconds = 0.8f;

    [Header("Damage")]
    [SerializeField] int damage = 15;

    [Header("Refs")]
    [SerializeField] GameObject attackZone;
    [SerializeField] EnemyMeleeHitbox hitbox;
    [SerializeField] Animator animator;

    NavMeshAgent agent;
    Transform player;
    float nextRepathTime;
    float nextAttackTime;
    bool isAttacking;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        var playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo != null) player = playerGo.transform;

        if (animator == null) animator = GetComponentInChildren<Animator>();

        if (attackZone != null) attackZone.SetActive(false);
        if (hitbox != null) hitbox.SetDamage(damage);
    }

    void Update()
    {
        if (agent == null || player == null) return;

        var dist = Vector3.Distance(transform.position, player.position);

        if (dist > aggroRange)
        {
            if (agent.hasPath) agent.ResetPath();
            SetMoving(false);
            return;
        }

        agent.stoppingDistance = attackRange;

        if (isAttacking)
        {
            agent.isStopped = true;
            SetMoving(false);
            return;
        }

        if (dist > attackRange)
        {
            agent.isStopped = false;
            if (Time.time >= nextRepathTime)
            {
                agent.SetDestination(player.position);
                nextRepathTime = Time.time + repathInterval;
            }
            SetMoving(agent.velocity.sqrMagnitude > 0.01f);
            return;
        }

        agent.isStopped = true;
        SetMoving(false);

        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + cooldownSeconds;
        isAttacking = true;

        if (animator != null) animator.SetTrigger("Attack");
    }

    void SetMoving(bool value)
    {
        if (animator != null) animator.SetBool("IsMoving", value);
    }

    public void AnimEvent_HitStart()
    {
        if (attackZone != null) attackZone.SetActive(true);
    }

    public void AnimEvent_HitEnd()
    {
        if (attackZone != null) attackZone.SetActive(false);
    }

    public void AnimEvent_AttackFinished()
    {
        if (attackZone != null) attackZone.SetActive(false);
        isAttacking = false;
    }
}
