using Code.Source.Lessons.Base;
using UnityEngine;

namespace Code.Source.Lessons.Input
{
    public class InvokeAttackByInput : MonoBehaviour
    {
        [SerializeField] private AttackBehaviour _attackBehaviour;

        private const KeyCode LeftMouseButton = KeyCode.Mouse0;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(LeftMouseButton))
            {
                _attackBehaviour.PerformAttack();
            }
        }
    }
}