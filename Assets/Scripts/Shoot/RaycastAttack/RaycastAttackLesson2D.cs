using Code.Source.Lessons.Base;
using UnityEngine;

namespace Code.Source.Lessons.RaycastAttack
{
    public class RaycastAttackLesson2D : AttackBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Min(0f)] private float _distance = Mathf.Infinity;
        
        [ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack()
        {
            var origin = (Vector2) transform.position;
            var direction = Vector2.right;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _distance, _layerMask);
            Collider2D hitCollider = hit.collider;
            
            if (hitCollider != null)
            {
                // Do something.
            }
        }
    }
}