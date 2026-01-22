using UnityEngine;

public sealed class AnimationEventRelay : MonoBehaviour
{
    MeleeEnemyAttacker attacker;

    void Awake()
    {
        attacker = GetComponentInParent<MeleeEnemyAttacker>();
    }

    public void AnimEvent_HitStart()
    {
        if (attacker != null) attacker.AnimEvent_HitStart();
    }

    public void AnimEvent_HitEnd()
    {
        if (attacker != null) attacker.AnimEvent_HitEnd();
    }

    public void AnimEvent_AttackFinished()
    {
        if (attacker != null) attacker.AnimEvent_AttackFinished();
    }
}
