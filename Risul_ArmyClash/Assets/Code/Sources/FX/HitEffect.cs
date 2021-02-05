using DG.Tweening;
using UnityEngine;

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
            gameObject.SetActive(false);
        }

        public void PlayEffect(Vector3 initPosition)
        {
            gameObject.SetActive(true);
            _transform.position = initPosition;
            _particleSystem.Play();
            DOTween.Sequence().AppendInterval(1).AppendCallback(() => { gameObject.SetActive(false); });
        }
    }
}