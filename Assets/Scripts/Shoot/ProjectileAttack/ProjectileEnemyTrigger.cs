using Code.Source.Lessons.Enums;
using Code.Source.Lessons.Units;
using UnityEngine;

namespace Code.Source.Lessons.ProjectileAttack
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileEnemyTrigger : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private EnemyUnitTypeFlags _enemiesToDestroy;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyUnit enemyUnit))
            {
                if (enemyUnit.IsAlive() == false)
                    return;

                enemyUnit.ApplyDamage(_projectile.Damage);

                if (enemyUnit.IsAlive())
                {
                    OnEnemyIsAliveAfterAttack();
                }
                else
                {
                    OnEnemyIsDeadAfterAttack(enemyUnit);
                }
            }
        }

        private void OnEnemyIsAliveAfterAttack()
        {
            _projectile.DisposeProjectile();
        }

        private void OnEnemyIsDeadAfterAttack(EnemyUnit enemyUnit)
        {
            if (_enemiesToDestroy.HasFlag((EnemyUnitTypeFlags) enemyUnit.EnemyUnitType))
            {
                Destroy(enemyUnit.gameObject);
            }
            else
            {
                _projectile.DisposeProjectile();
            }
        }
    }
}