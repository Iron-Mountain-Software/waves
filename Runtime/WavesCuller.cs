using UnityEngine;

namespace IronMountain.Waves
{
    public class WavesCuller : MonoBehaviour
    {
        private static readonly int CullMode = Shader.PropertyToID("_CullMode");

        [SerializeField] private Transform viewer;
        
        [Header("Cache")]
        private MeshRenderer _meshRenderer;

        private MeshRenderer MeshRenderer
        {
            get
            {
                if (!_meshRenderer) _meshRenderer = GetComponent<MeshRenderer>();
                if (!_meshRenderer) _meshRenderer = gameObject.AddComponent<MeshRenderer>();
                return _meshRenderer;
            }
        }

        private void Start()
        {
            RefreshViewer();
            RefreshCulledSide();
        }

        private void Update()
        {
            RefreshCulledSide();
        }

        private void RefreshViewer()
        {
            if (viewer) return;
            Camera mainCamera = Camera.main;
            if (mainCamera) viewer = mainCamera.transform;
        }

        private void RefreshCulledSide()
        {
            if (!viewer) return;
            bool viewerIsAboveWaves = viewer.position.y > transform.position.y;
            MeshRenderer.sharedMaterial.SetFloat(CullMode, viewerIsAboveWaves ? 2f : 1f);
        }
    }
}
