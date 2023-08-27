using UnityEngine;

internal class Enemy : MonoBehaviour, IAttackable
{
    private float _maxHealth = 100;
    private float _health = 100;

    public float Health => _health;
    public float MaxHealth => _maxHealth;

    public void RecieveDamage(float amount)
    {
        if (_health <= 0)
        {
            return;
        }

        _health -= amount;

        if (_health <= 0)
        {
            Invoke(nameof(Destroy), 1f);
        }

        Debug.Log($"пиу {gameObject.name}, здоровье {_health}");
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}