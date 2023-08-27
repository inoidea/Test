using Code.Source.Lessons.Base;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;

    private const KeyCode LeftMouseButton = KeyCode.Mouse0;

    private void Update()
    {
        if (Input.GetKeyDown(LeftMouseButton))
        {
            _playerAttack.PerformRaycast();
        }
    }
}
