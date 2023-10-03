using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.Waves.Settings
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Gameplay/Waves/Waves Settings")]
    public class WavesSettings : ScriptableObject
    {
        [Serializable]
        public struct Wave
        {
            [SerializeField] [Range(-3.1459f, 3.1459f)] private float rotation;
            [SerializeField] [Range(0, 1)] private float steepness;
            [SerializeField] private float wavelength;

            public float Rotation => rotation;
            public float Steepness => steepness;
            public float Wavelength => wavelength;
        }
        
        private static readonly int UseWaveA = Shader.PropertyToID("_UseWaveA");
        private static readonly int WaveARotation = Shader.PropertyToID("_WaveARotation");
        private static readonly int WaveASteepness = Shader.PropertyToID("_WaveASteepness");
        private static readonly int WaveAWavelength = Shader.PropertyToID("_WaveAWavelength");

        private static readonly int UseWaveB = Shader.PropertyToID("_UseWaveB");
        private static readonly int WaveBRotation = Shader.PropertyToID("_WaveBRotation");
        private static readonly int WaveBSteepness = Shader.PropertyToID("_WaveBSteepness");
        private static readonly int WaveBWavelength = Shader.PropertyToID("_WaveBWavelength");

        private static readonly int UseWaveC = Shader.PropertyToID("_UseWaveC");
        private static readonly int WaveCRotation = Shader.PropertyToID("_WaveCRotation");
        private static readonly int WaveCSteepness = Shader.PropertyToID("_WaveCSteepness");
        private static readonly int WaveCWavelength = Shader.PropertyToID("_WaveCWavelength");

        private static readonly int UseWaveD = Shader.PropertyToID("_UseWaveD");
        private static readonly int WaveDRotation = Shader.PropertyToID("_WaveDRotation");
        private static readonly int WaveDSteepness = Shader.PropertyToID("_WaveDSteepness");
        private static readonly int WaveDWavelength = Shader.PropertyToID("_WaveDWavelength");

        private static readonly int UseWaveE = Shader.PropertyToID("_UseWaveE");
        private static readonly int WaveERotation = Shader.PropertyToID("_WaveERotation");
        private static readonly int WaveESteepness = Shader.PropertyToID("_WaveESteepness");
        private static readonly int WaveEWavelength = Shader.PropertyToID("_WaveEWavelength");
    
        [SerializeField] private List<Wave> waves;
        
        public List<Wave> Waves => waves;
        
        public float MaximumAmplitude
        {
            get
            {
                float amplitude = 0f;
                foreach (Wave wave in waves) amplitude += wave.Steepness / (2 * Mathf.PI / wave.Wavelength);
                return amplitude;
            }
        }

        public Vector3 GetWaveOffsetAtWorldPosition(Vector2 worldPositionXZ)
        {
            Vector3 offset = Vector3.zero;
            foreach (Wave wave in waves)
            {
                float k = 2 * Mathf.PI / wave.Wavelength;
                float phaseSpeed = Mathf.Sqrt(9.8f / k);
                Vector2 direction = new Vector2(Mathf.Cos(wave.Rotation), Mathf.Sin(wave.Rotation));
                float f = k * (Vector2.Dot(direction, worldPositionXZ) - phaseSpeed * Time.time);
                float amplitude = wave.Steepness / k;
                offset += new Vector3(
                    direction.x * (amplitude * Mathf.Cos(f)),
                    amplitude * Mathf.Sin(f),
                    direction.y * (amplitude * Mathf.Cos(f))
                );
            }
            return offset;
        }
        
        public void ApplyTo(Material material)
        {
            if (!material) return;
            if (waves.Count >= 1)
            {
                material.SetFloat(UseWaveA, 1);
                material.SetFloat(WaveARotation, waves[0].Rotation);
                material.SetFloat(WaveASteepness, waves[0].Steepness);
                material.SetFloat(WaveAWavelength, waves[0].Wavelength);
            }
            else material.SetFloat(UseWaveA, 0);

            if (waves.Count >= 2)
            {
                material.SetFloat(UseWaveB, 1);
                material.SetFloat(WaveBRotation, waves[1].Rotation);
                material.SetFloat(WaveBSteepness, waves[1].Steepness);
                material.SetFloat(WaveBWavelength, waves[1].Wavelength);
            }
            else material.SetFloat(UseWaveB, 0);
            
            if (waves.Count >= 3)
            {
                material.SetFloat(UseWaveC, 1);
                material.SetFloat(WaveCRotation, waves[2].Rotation);
                material.SetFloat(WaveCSteepness, waves[2].Steepness);
                material.SetFloat(WaveCWavelength, waves[2].Wavelength);
            }
            else material.SetFloat(UseWaveC, 0);
            
            if (waves.Count >= 4)
            {
                material.SetFloat(UseWaveD, 1);
                material.SetFloat(WaveDRotation, waves[3].Rotation);
                material.SetFloat(WaveDSteepness, waves[3].Steepness);
                material.SetFloat(WaveDWavelength, waves[3].Wavelength);
            }
            else material.SetFloat(UseWaveD, 0);
            
            if (waves.Count >= 5)
            {
                material.SetFloat(UseWaveE, 1);
                material.SetFloat(WaveERotation, waves[4].Rotation);
                material.SetFloat(WaveESteepness, waves[4].Steepness);
                material.SetFloat(WaveEWavelength, waves[4].Wavelength);
            }
            else material.SetFloat(UseWaveE, 0);
        }
    }
}