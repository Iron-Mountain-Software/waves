using System;
using UnityEngine;

namespace IronMountain.Waves
{
    [RequireComponent(typeof(Rigidbody))]
    public class BuoyancyPhysics : MonoBehaviour
    {
        [SerializeField] private Waves waves;
        
        [Header("Cache")]
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!waves || !waves.WavesSettings) return;
            Vector3 worldPosition = transform.position;
            Vector2 xzPosition = new Vector2(worldPosition.x, worldPosition.z);
            float wavesHeight = waves.transform.position.y;
            float wavesHeightOffset = waves.WavesSettings.GetWaveOffsetAtWorldPosition(xzPosition).y;
            if (worldPosition.y > wavesHeight + wavesHeightOffset)
            {
                _rigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
            }
            else _rigidbody.AddForce(-Physics.gravity, ForceMode.Acceleration);
        }
    }
}
