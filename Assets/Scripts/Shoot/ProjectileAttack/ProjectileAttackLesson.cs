using Code.Source.Lessons.Base;
using UnityEngine;

namespace Code.Source.Lessons.ProjectileAttack
{
    public class ProjectileAttackLesson : AttackBehaviour
    {
        [SerializeField] private Transform _weaponMuzzle;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
        [SerializeField, Min(0f)] private float _force = 10f;

        [ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack()
        {
            var projectile = Instantiate(_projectilePrefab, _weaponMuzzle.position, _weaponMuzzle.rotation);
            
            projectile.Rigidbody.AddForce(_weaponMuzzle.forward * _force, _forceMode);
        }
    }
}