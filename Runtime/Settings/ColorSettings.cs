using UnityEngine;

namespace IronMountain.Waves.Settings
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Gameplay/Waves/Color Settings")]
    public class ColorSettings : ScriptableObject
    {
        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        private static readonly int MiddleColor = Shader.PropertyToID("_MiddleColor");
        private static readonly int BottomColor = Shader.PropertyToID("_BottomColor");
        
        private static readonly int TopTexture = Shader.PropertyToID("_TopTexture");
        private static readonly int TopTextureTileX = Shader.PropertyToID("_TopTextureTileX");
        private static readonly int TopTextureTileY = Shader.PropertyToID("_TopTextureTileY");
        private static readonly int TopTextureScrollX = Shader.PropertyToID("_TopTextureScrollX");
        private static readonly int TopTextureScrollY = Shader.PropertyToID("_TopTextureScrollY");
        private static readonly int MiddleTexture = Shader.PropertyToID("_MiddleTexture");
        private static readonly int MiddleTextureTileX = Shader.PropertyToID("_MiddleTextureTileX");
        private static readonly int MiddleTextureTileY = Shader.PropertyToID("_MiddleTextureTileY");
        private static readonly int MiddleTextureScrollX = Shader.PropertyToID("_MiddleTextureScrollX");
        private static readonly int MiddleTextureScrollY = Shader.PropertyToID("_MiddleTextureScrollY");
        private static readonly int BottomTexture = Shader.PropertyToID("_BottomTexture");
        private static readonly int BottomTextureTileX = Shader.PropertyToID("_BottomTextureTileX");
        private static readonly int BottomTextureTileY = Shader.PropertyToID("_BottomTextureTileY");
        private static readonly int BottomTextureScrollX = Shader.PropertyToID("_BottomTextureScrollX");
        private static readonly int BottomTextureScrollY = Shader.PropertyToID("_BottomTextureScrollY");
        
        private static readonly int Smoothness = Shader.PropertyToID("_Smoothness");
        private static readonly int Metallic = Shader.PropertyToID("_Metallic");
        
        [SerializeField] private Color topColor = Color.white;
        [SerializeField] private Color middleColor = Color.white;
        [SerializeField] private Color bottomColor = Color.white;
        [Space]
        [SerializeField] private Texture topTexture;
        [SerializeField] private Vector2 topTextureTiling = Vector2.one;
        [SerializeField] private Vector2 topTextureScrolling = Vector2.zero;
        [Space]
        [SerializeField] private Texture middleTexture;
        [SerializeField] private Vector2 middleTextureTiling = Vector2.one;
        [SerializeField] private Vector2 middleTextureScrolling = Vector2.zero;
        [Space]
        [SerializeField] private Texture bottomTexture;
        [SerializeField] private Vector2 bottomTextureTiling = Vector2.one;
        [SerializeField] private Vector2 bottomTextureScrolling = Vector2.zero;
        [Space]
        [SerializeField] [Range(0, 1)] private float smoothness = 0.5f;
        [SerializeField] [Range(0, 1)] private float metallic;

        public void ApplyTo(Material material)
        {
            if (!material) return;
            material.SetColor(TopColor, topColor);
            material.SetColor(MiddleColor, middleColor);
            material.SetColor(BottomColor, bottomColor);
            
            material.SetTexture(TopTexture, topTexture);
            material.SetTextureScale(TopTexture, topTextureTiling);
            material.SetFloat(TopTextureTileX, topTextureTiling.x);
            material.SetFloat(TopTextureTileY, topTextureTiling.y);
            material.SetFloat(TopTextureScrollX, topTextureScrolling.x);
            material.SetFloat(TopTextureScrollY, topTextureScrolling.y);
            
            material.SetTexture(MiddleTexture, middleTexture);
            material.SetTextureScale(MiddleTexture, middleTextureTiling);
            material.SetFloat(MiddleTextureTileX, middleTextureTiling.x);
            material.SetFloat(MiddleTextureTileY, middleTextureTiling.y);
            material.SetFloat(MiddleTextureScrollX, middleTextureScrolling.x);
            material.SetFloat(MiddleTextureScrollY, middleTextureScrolling.y);
            
            material.SetTexture(BottomTexture, bottomTexture);
            material.SetTextureScale(BottomTexture, bottomTextureTiling);
            material.SetFloat(BottomTextureTileX, bottomTextureTiling.x);
            material.SetFloat(BottomTextureTileY, bottomTextureTiling.y);
            material.SetFloat(BottomTextureScrollX, bottomTextureScrolling.x);
            material.SetFloat(BottomTextureScrollY, bottomTextureScrolling.y);

            material.SetFloat(Smoothness, smoothness);
            material.SetFloat(Metallic, metallic);
        }
    }
}
