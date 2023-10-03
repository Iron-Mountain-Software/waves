using UnityEngine;

namespace IronMountain.Waves
{
    public class WaveHeightMatcher : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float heightOffset;

        [Header("References")]
        [SerializeField] private Waves waves;

        [Header("Cache")]
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
        
        private void Update()
        {
            if (!waves || !waves.WavesSettings) return;
            Vector3 startPosition = _transform.position;
            float wavesHeight = waves.transform.position.y;
            float wavesHeightOffset = waves.WavesSettings
                .GetWaveOffsetAtWorldPosition(new Vector2(startPosition.x, startPosition.z)).y;
            _transform.position = new Vector3(
                startPosition.x,
                wavesHeight + wavesHeightOffset + heightOffset,
                startPosition.z
            );
        }
    }
}