using System;
using Code.Source.Lessons.Base;
using Code.Source.Lessons.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Source.Lessons.RaycastAttack
{
    public class RaycastAttackLesson : AttackBehaviour
    {
        [Header("Damage")]
        [SerializeField, Min(0f)] private float _damage = 10f;
        
        [Header("Ray")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Min(0)] private float _distance = Mathf.Infinity;
        [SerializeField, Min(0)] private int _shotCount = 1;
        
        [Header("Spread")]
        [SerializeField] private bool _useSpread;
        [SerializeField, Min(0)] private float _spreadFactor = 1f;

        [Header("Particle System")]
        [SerializeField] private ParticleSystem _muzzleEffect;
        [SerializeField] private ParticleSystem _hitEffectPrefab;
        [SerializeField, Min(0f)] private float _hitEffectDestroyDelay = 2f;
        
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        [Header("Debug")] 
        [SerializeField] private DrawRayType _drawRayType = DrawRayType.Raycast;

        [ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack()
        {
            for (var i = 0; i < _shotCount; i++)
            {
                PerformRaycast();
            }
            
            PerformEffects();
        }

        private void PerformRaycast()
        {
            var direction = _useSpread ? transform.forward + CalculateSpread() : transform.forward;
            var ray = new Ray(transform.position, direction);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
            {
                var hitCollider = hitInfo.collider;

                if (hitCollider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damage);
                }
                else
                {
                    // On IDamageable is not found.
                }
                
                SpawnParticleEffectOnHit(hitInfo);
            }
        }

        private void PerformEffects()
        {
            if (_muzzleEffect != null)
            {
                _muzzleEffect.Play();
            }

            if (_audioSource != null && _audioClip != null)
            {
                _audioSource.PlayOneShot(_audioClip);
            }
        }

        private void SpawnParticleEffectOnHit(RaycastHit hitInfo)
        {
            if (_hitEffectPrefab != null)
            {
                var hitEffectRotation = Quaternion.LookRotation(hitInfo.normal);
                var hitEffect = Instantiate(_hitEffectPrefab, hitInfo.point, hitEffectRotation);
                    
                Destroy(hitEffect.gameObject, _hitEffectDestroyDelay);
            }
        }

        private Vector3 CalculateSpread()
        {
            return new Vector3
            {
                x = Random.Range(-_spreadFactor, _spreadFactor),
                y = Random.Range(-_spreadFactor, _spreadFactor),
                z = Random.Range(-_spreadFactor, _spreadFactor)
            };
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_drawRayType == DrawRayType.None)
                return;
            
            var ray = new Ray(transform.position, transform.forward);
            
            switch (_drawRayType)
            {
                case DrawRayType.None: break;
                case DrawRayType.Raycast: DrawRaycast(ray); break;
                case DrawRayType.BoxCast: DrawBoxCast(ray); break;
                case DrawRayType.SphereCast: DrawSphereCast(ray); break;
                
                default: throw new ArgumentOutOfRangeException(nameof(_drawRayType));
            }
        }
        
        private void DrawRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hitInfo, _distance, _layerMask))
            {
                DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
            }
            else
            {
                var hitPosition = ray.origin + ray.direction * _distance;
                
                DrawRay(ray, hitPosition, _distance, Color.green);
            }
        }

        private void DrawBoxCast(Ray ray)
        {
            var boxHalfExtents = transform.lossyScale / 2f;
            
            if (Physics.BoxCast(ray.origin, boxHalfExtents, ray.direction, 
                    out var hitInfo, transform.rotation, _distance, _layerMask))
            {
                DrawRay(ray, ray.origin + ray.direction * hitInfo.distance, hitInfo.distance, Color.red);
            }
            else
            {
                DrawRay(ray, ray.origin + ray.direction * _distance, _distance, Color.green);
            }
        }

        private void DrawSphereCast(Ray ray)
        {
            var sphereRadius = transform.lossyScale.x / 2f;

            if (Physics.SphereCast(ray.origin, sphereRadius, ray.direction, out var hitInfo, _distance, _layerMask))
            {
                DrawRay(ray, ray.origin + ray.direction * hitInfo.distance, hitInfo.distance, Color.red);
            }
            else
            {
                DrawRay(ray, ray.origin + ray.direction * _distance, _distance, Color.green);
            }
        }
        
        private void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
        {
            const float hitPointRadius = 0.15f;

            Debug.DrawRay(ray.origin, ray.direction * distance, color);

            Gizmos.color = color;

            switch (_drawRayType)
            {
                case DrawRayType.None: break;
                case DrawRayType.Raycast: Gizmos.DrawSphere(hitPosition, hitPointRadius); break;
                case DrawRayType.BoxCast: Gizmos.DrawWireCube(hitPosition, transform.lossyScale); break;
                case DrawRayType.SphereCast: Gizmos.DrawWireSphere(hitPosition, transform.lossyScale.x / 2f); break;
                
                default: throw new ArgumentOutOfRangeException(nameof(_drawRayType), _drawRayType, null);
            }
        }
#endif
        
        private enum DrawRayType
        {
            None,
            Raycast,
            BoxCast,
            SphereCast
        }
    }
}