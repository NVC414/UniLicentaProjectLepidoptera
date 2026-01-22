using UnityEngine;

public sealed class PlayerSwordAnimEventReceiver : MonoBehaviour
{
    [SerializeField] PlayerSwordAttack attack;

    void Awake()
    {
        if (attack == null) attack = GetComponent<PlayerSwordAttack>();
    }

    public void AttackStart()
    {
        if (attack != null) attack.AnimEvent_AttackStart();
    }

    public void DoDamage()
    {
        if (attack != null) attack.AnEvent_DoDamageSafe();
    }

    public void AttackEnd()
    {
        if (attack != null) attack.AnimEvent_AttackEnd();
    }
}