using UnityEngine;

internal class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    public void PerformRaycast()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            var hitCollider = hitInfo.collider;
            Debug.Log(hitCollider);

            if (hitCollider.TryGetComponent(out IAttackable unit))
            {
                unit.RecieveDamage(_weapon.Damage);
            }
            else
            {
                Debug.Log("nope");
                // On IAttackable is not found.
            }

            SpawnParticleEffectOnHit(hitInfo);
        } 
        else
        {
            Debug.Log("nope2");
        }
    }

    private void SpawnParticleEffectOnHit(RaycastHit hitInfo)
    {
        if (_weapon.effectPrefab != null)
        {
            var hitEffectRotation = Quaternion.LookRotation(hitInfo.normal);
            var hitEffect = Instantiate(_weapon.effectPrefab, hitInfo.point, hitEffectRotation);

            Destroy(hitEffect.gameObject, _weapon.effectDestroyDelay);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var ray = new Ray(transform.position, transform.forward);
        DrawRaycast(ray);
    }

    private void DrawRaycast(Ray ray)
    {
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * Mathf.Infinity;

            DrawRay(ray, hitPosition, Mathf.Infinity, Color.green);
        }
    }

    private void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
    {
        const float hitPointRadius = 0.15f;

        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition, hitPointRadius);
    }
#endif

}
