using Code.Source.Lessons.Enums;
using UnityEngine;

namespace Code.Source.Lessons.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private EnemyUnitType _enemyUnitType;

        public EnemyUnitType EnemyUnitType => _enemyUnitType;
    }
}