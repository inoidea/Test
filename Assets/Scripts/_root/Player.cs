using UnityEngine;

internal class Player : MonoBehaviour, IAttackable
{
    private float _maxHealth = 100;
    private float _health = 100;

    public float Health => _health;
    public float MaxHealth => _maxHealth;

    public void RecieveDamage(float damage)
    {
        Debug.Log($"Урон {damage}");
    }
}
