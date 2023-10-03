using IronMountain.Waves.Settings;
using UnityEngine;
using UnityEngine.Rendering;

namespace IronMountain.Waves
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(WaveMeshGenerator))]
    public class Waves : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private WavesSettings wavesSettings;
        [SerializeField] private ColorSettings colorSettings;
        [SerializeField] private TransparencySettings transparencySettings;
        [SerializeField] private Shader shader;

        [Header("Cache")]
        private Transform _transform;
        private MeshRenderer _meshRenderer;
        private WaveMeshGenerator _waveMeshGenerator;
        
        private static readonly int DimensionsX = Shader.PropertyToID("_DimensionsX");
        private static readonly int DimensionsZ = Shader.PropertyToID("_DimensionsZ");

        public WavesSettings WavesSettings => wavesSettings;

        private MeshRenderer MeshRenderer
        {
            get
            {
                if (!_meshRenderer) _meshRenderer = GetComponent<MeshRenderer>();
                if (!_meshRenderer) _meshRenderer = gameObject.AddComponent<MeshRenderer>();
                return _meshRenderer;
            }
        }
        
        private WaveMeshGenerator WaveMeshGenerator
        {
            get
            {
                if (!_waveMeshGenerator) _waveMeshGenerator = GetComponent<WaveMeshGenerator>();
                if (!_waveMeshGenerator) _waveMeshGenerator = gameObject.AddComponent<WaveMeshGenerator>();
                return _waveMeshGenerator;
            }
        }

        private void Start()
        {
            WaveMeshGenerator.Run();
            RefreshMaterial();
        }

        private void RefreshMaterial()
        {
            Material material = shader ? new Material(shader) : new Material("Standard");
            material.SetFloat(DimensionsX, WaveMeshGenerator.DimensionX);
            material.SetFloat(DimensionsZ, WaveMeshGenerator.DimensionZ);
            if (wavesSettings) wavesSettings.ApplyTo(material);
            if (colorSettings) colorSettings.ApplyTo(material);
            if (transparencySettings) transparencySettings.ApplyTo(material);
            MeshRenderer.sharedMaterial = material;
            MeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            MeshRenderer.receiveShadows = false;
        }
    }
}
