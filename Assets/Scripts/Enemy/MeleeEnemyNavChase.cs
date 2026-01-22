using UnityEngine;
using UnityEngine.AI;

public sealed class MeleeEnemyNavChase : MonoBehaviour
{
    [SerializeField] float aggroRange = 12f;
    [SerializeField] float repathInterval = 0.15f;

    NavMeshAgent agent;
    Transform player;
    float nextRepathTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        var playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo != null) player = playerGo.transform;
    }

    void Update()
    {
        if (agent == null || player == null) return;

        var dist = Vector3.Distance(transform.position, player.position);
        if (dist > aggroRange)
        {
            if (agent.hasPath) agent.ResetPath();
            return;
        }

        if (Time.time >= nextRepathTime)
        {
            agent.SetDestination(player.position);
            nextRepathTime = Time.time + repathInterval;
        }
    }
}