using System.Collections.Generic;
using Code.Source.Lessons.Base;
using Code.Source.Lessons.Enums;
using Code.Source.Lessons.Interfaces;
//using NTC.OverlapSugar;
using UnityEngine;

namespace Code.Source.Lessons.OverlapAttack
{
    public class FastOverlapLessonWithAttack : AttackBehaviour
    {
        [SerializeField, Min(0f)] private float _damage = 10f;
        [SerializeField] private DrawGizmosType _drawGizmosType;
        //[SerializeField] private OverlapSettings _overlapSettings;
        
        private readonly List<IDamageable> _damageableResults = new(32);

        [ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack()
        {
            // For single target.
            //if (_overlapSettings.TryFind(out IDamageable damageable))
            //{
            //    ApplyDamage(damageable);
            //}

            // For many targets.
            //if (_overlapSettings.TryFind(_damageableResults))
            //{
            //    _damageableResults.ForEach(ApplyDamage);
            //}
        }

        private void ApplyDamage(IDamageable damageable)
        {
            damageable.ApplyDamage(_damage);
        }
        
#if UNITY_EDITOR
        //private void OnDrawGizmos()
        //{
        //    if (_drawGizmosType == DrawGizmosType.Always)
        //    {
        //        _overlapSettings.TryDrawGizmos();
        //    }
        //}

        //private void OnDrawGizmosSelected()
        //{
        //    if (_drawGizmosType == DrawGizmosType.OnSelected)
        //    {
        //        _overlapSettings.TryDrawGizmos();
        //    }
        //}
#endif
    }
}