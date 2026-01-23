using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Animator animator;
    [SerializeField] string deathStateName = "rig_Anim_Die";
    [SerializeField] bool disableCollidersOnDeath = false;

    bool isDead;

    void Awake()
    {
        if (health == null) health = GetComponent<Health>();
        if (animator == null) animator = GetComponentInChildren<Animator>();

        if (health != null) health.OnDied += OnDied;
    }

    void OnDestroy()
    {
        if (health != null) health.OnDied -= OnDied;
    }

   void OnDied()
{
    if (isDead) return;
    isDead = true;

    if (HUDController.Instance != null)
        HUDController.Instance.AddKill();

    var agent = GetComponent<NavMeshAgent>();
    if (agent != null)
    {
        agent.isStopped = true;
        agent.ResetPath();
        agent.enabled = false;
    }

    var attacker = GetComponent<MeleeEnemyAttacker>();
    if (attacker != null) attacker.enabled = false;

    var relays = GetComponentsInChildren<AnimationEventRelay>(true);
    for (int i = 0; i < relays.Length; i++) relays[i].enabled = false;

    var hitboxes = GetComponentsInChildren<EnemyMeleeHitbox>(true);
    for (int i = 0; i < hitboxes.Length; i++) hitboxes[i].gameObject.SetActive(false);

    if (disableCollidersOnDeath)
    {
        var cols = GetComponentsInChildren<Collider>(true);
        for (int i = 0; i < cols.Length; i++) cols[i].enabled = false;
    }

    if (animator != null)
    {
        animator.Play(deathStateName, 0, 0f);
        animator.Update(0f);
    }
}

}
