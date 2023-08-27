using UnityEngine;

[CreateAssetMenu(fileName = nameof(Weapon), menuName = "Settings/" + nameof(Weapon), order = 0)]
public class Weapon : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float Damage { get; private set; }

    [Header("Effects")]
    [field: SerializeField] public ParticleSystem effectPrefab;
    [field: SerializeField, Min(0f)] public float effectDestroyDelay = 2f;
}
