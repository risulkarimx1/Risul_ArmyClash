using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.FX
{
    public class HitEffect : MonoBehaviour
    {
        private Transform _transform;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        private void Reposition(Vector3 initPosition)
        {
            gameObject.SetActive(true);
            _transform.position = initPosition;
            _particleSystem.Play();
        }

        public class Pool : MemoryPool<Vector3, HitEffect>
        {
            protected override void Reinitialize(Vector3 position, HitEffect item)
            {
                item.Reposition(position);
            }

            protected override void OnDespawned(HitEffect item)
            {
                base.OnDespawned(item);
                item.OnDespawned();
            }
        }
    }
}
