using UnityEngine;

namespace IronMountain.Waves.Settings
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Gameplay/Waves/Transparency Settings")]
    public class TransparencySettings : ScriptableObject
    {
        private enum TransparentType
        {
            NearTransparent,
            FarTransparent
        }

        private static readonly int TransparentPart = Shader.PropertyToID("_TransparentPart");
        private static readonly int BlurRadius = Shader.PropertyToID("_BlurRadius");
        private static readonly int OpaqueRadius = Shader.PropertyToID("_OpaqueRadius");

        [SerializeField] private TransparentType transparentPart = TransparentType.FarTransparent;
        [SerializeField] private float blurRadius = 10;
        [SerializeField] private float opaqueRadius = 10;

        public void ApplyTo(Material material)
        {
            if (!material) return;
            switch (transparentPart)
            {
                case TransparentType.NearTransparent:
                    material.SetFloat(TransparentPart, 0f);
                    break;
                case TransparentType.FarTransparent:
                    material.SetFloat(TransparentPart, 1f);
                    break;
            }
            material.SetFloat(BlurRadius, blurRadius);
            material.SetFloat(OpaqueRadius, opaqueRadius);
        }
    }
}
