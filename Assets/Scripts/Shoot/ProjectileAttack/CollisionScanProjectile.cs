using Code.Source.Lessons.Interfaces;
using UnityEngine;

namespace Code.Source.Lessons.ProjectileAttack
{
    public class CollisionScanProjectile : Projectile
    {
        protected override void OnTargetCollision(Collision collision, IDamageable damageable)
        {
            damageable.ApplyDamage(Damage);
        }
    }
}