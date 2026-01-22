using System;
using UnityEngine;

public sealed class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    public int Max => maxHealth;
    public int Current { get; private set; }
    public bool IsDead => Current <= 0;

    public event Action<int,int> OnChanged;
    public event Action OnDied;

    void Awake()
    {
        Current = maxHealth;
        OnChanged?.Invoke(Current, maxHealth);
    }

    public void Damage(int amount)
    {
        if (IsDead) return;
        Current = Mathf.Clamp(Current - amount, 0, maxHealth);
        OnChanged?.Invoke(Current, maxHealth);
        if (Current <= 0) OnDied?.Invoke();
    }

    public void Heal(int amount)
    {
        if (IsDead) return;
        Current = Mathf.Clamp(Current + amount, 0, maxHealth);
        OnChanged?.Invoke(Current, maxHealth);
    }
}