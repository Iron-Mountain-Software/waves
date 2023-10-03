Shader "Custom/Waves/Gerstner Waves - Transparent"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Cull Mode", Float) = 2.0

        _DimensionsX("Dimensions X", Float) = 0
        _DimensionsZ("Dimensions Z", Float) = 0
        
        [MainColor] _TopColor ("Top Color", Color) = (1,1,1,1)
        _MiddleColor ("Middle Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Color", Color) = (1,1,1,1)
        
        [MainTexture] _TopTexture ("Top Texture", 2D) = "white" {}
        _TopTextureTileX("Top Texture Tile X", Float) = 0
        _TopTextureTileY("Top Texture Tile Y", Float) = 0
        _TopTextureScrollX("Top Texture Scroll X", Float) = 0
        _TopTextureScrollY("Top Texture Scroll Y", Float) = 0
        _MiddleTexture ("Middle Texture", 2D) = "white" {}
        _MiddleTextureTileX("Middle Texture Tile X", Float) = 0
        _MiddleTextureTileY("Middle Texture Tile Y", Float) = 0
        _MiddleTextureScrollX("Middle Texture Scroll X", Float) = 0
        _MiddleTextureScrollY("Middle Texture Scroll Y", Float) = 0
        _BottomTexture ("Bottom Texture", 2D) = "white" {}
        _BottomTextureTileX("Bottom Texture Tile X", Float) = 0
        _BottomTextureTileY("Bottom Texture Tile Y", Float) = 0
        _BottomTextureScrollX("Bottom Texture Scroll X", Float) = 0
        _BottomTextureScrollY("Bottom Texture Scroll Y", Float) = 0
        
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [MaterialToggle] _UseWaveA("Use Wave A", Float) = 0
        _WaveARotation("Wave A Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveASteepness("Wave A Steepness", Range(0, 1)) = .25
        _WaveAWavelength("Wave A Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveB("Use Wave B", Float) = 0
        _WaveBRotation("Wave B Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveBSteepness("Wave B Steepness", Range(0, 1)) = .25
        _WaveBWavelength("Wave B Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveC("Use Wave C", Float) = 0
        _WaveCRotation("Wave C Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveCSteepness("Wave C Steepness", Range(0, 1)) = .25
        _WaveCWavelength("Wave C Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveD("Use Wave D", Float) = 0
        _WaveDRotation("Wave D Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveDSteepness("Wave D Steepness", Range(0, 1)) = .25
        _WaveDWavelength("Wave D Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveE("Use Wave E", Float) = 0
        _WaveERotation("Wave E Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveESteepness("Wave E Steepness", Range(0, 1)) = .25
        _WaveEWavelength("Wave E Wavelength", Float) = 5

        [KeywordEnum(NearTransparent, FarTransparent)] _TransparentPart("Transparent Part", Float) = 0
        _BlurRadius ("Blur Radius", Range(0.001, 1000)) = 10
        _OpaqueRadius ("Opaque Radius", Range(0.001, 1000)) = 10
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" "RenderType"="Transparent"
        }
        LOD 200

        Cull [_CullMode]
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade
        #pragma target 3.5

        struct Input
        {
            float2 uv_TopTexture : TEXCOORD0;
            float2 uv_MiddleTexture : TEXCOORD1;
            float2 uv_BottomTexture : TEXCOORD2;
            float3 localPos : TEXCOORD3;
            float3 worldPos;
            float amplitude;
        };

		float _DimensionsX, _DimensionsZ;
        
		float4 _TopColor;
		float4 _MiddleColor;
        float4 _BottomColor;
		float _ColorRange;
		float _ColorOffset;
        
        sampler2D _TopTexture;
        sampler2D _MiddleTexture;
        sampler2D _BottomTexture;
        
        float _TopTextureTileX, _TopTextureTileY;
        float _MiddleTextureTileX, _MiddleTextureTileY;
        float _BottomTextureTileX, _BottomTextureTileY;
        float _TopTextureScrollX, _TopTextureScrollY;
        float _MiddleTextureScrollX, _MiddleTextureScrollY;
        float _BottomTextureScrollX, _BottomTextureScrollY;

        half _Smoothness;
        half _Metallic;
        float _UseWaveA, _UseWaveB, _UseWaveC, _UseWaveD, _UseWaveE;
        float _WaveARotation, _WaveBRotation, _WaveCRotation, _WaveDRotation, _WaveERotation;
        float _WaveASteepness, _WaveBSteepness, _WaveCSteepness, _WaveDSteepness, _WaveESteepness;
        float _WaveAWavelength, _WaveBWavelength, _WaveCWavelength, _WaveDWavelength, _WaveEWavelength;
        float _TransparentPart;

        float _OpaqueRadius;
        float _BlurRadius;

        float4 GerstnerWave(float rotation, float steepness, float wavelength, float2 worldPositionXZ, inout float3 tangent, inout float3 binormal)
        {
            float k = 2 * UNITY_PI / wavelength;
            float phaseSpeed = sqrt(9.8 / k);
            float2 direction = float2(cos(rotation), sin(rotation));
            float f = k * (dot(direction, worldPositionXZ) - phaseSpeed * _Time.y);
            float amplitude = steepness / k;

            tangent += float3(
                -direction.x * direction.x * (steepness * sin(f)),
                direction.x * (steepness * cos(f)),
                -direction.x * direction.y * (steepness * sin(f))
            );
            binormal += float3(
                -direction.x * direction.y * (steepness * sin(f)),
                direction.y * (steepness * cos(f)),
                -direction.y * direction.y * (steepness * sin(f))
            );
            return float4(
                direction.x * (amplitude * cos(f)),
                amplitude * sin(f),
                direction.y * (amplitude * cos(f)),
                amplitude
            );
        }

        void vert(inout appdata_full vertexData, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.worldPos = mul(unity_ObjectToWorld, vertexData.vertex).xyz;
            float3 tangent = float3(1, 0, 0);
            float3 binormal = float3(0, 0, 1);
            float4 modifiedPosition = vertexData.vertex;
            if (_UseWaveA == 1) modifiedPosition += GerstnerWave(_WaveARotation, _WaveASteepness, _WaveAWavelength, o.worldPos.xz, tangent, binormal);
            if (_UseWaveB == 1) modifiedPosition += GerstnerWave(_WaveBRotation, _WaveBSteepness, _WaveBWavelength, o.worldPos.xz, tangent, binormal);
            if (_UseWaveC == 1) modifiedPosition += GerstnerWave(_WaveCRotation, _WaveCSteepness, _WaveCWavelength, o.worldPos.xz, tangent, binormal);
            if (_UseWaveD == 1) modifiedPosition += GerstnerWave(_WaveDRotation, _WaveDSteepness, _WaveDWavelength, o.worldPos.xz, tangent, binormal);
            if (_UseWaveE == 1) modifiedPosition += GerstnerWave(_WaveERotation, _WaveESteepness, _WaveEWavelength, o.worldPos.xz, tangent, binormal);
            vertexData.vertex.xyz = modifiedPosition.xyz;
            vertexData.normal = normalize(cross(binormal, tangent));
            o.localPos = modifiedPosition.xyz;
            o.amplitude = modifiedPosition.w;
        }

        float2 TopTextureGlobalUVOffset(Input IN)
        {
            return float2(
                _TopTextureTileX * (IN.worldPos.x - IN.localPos.x) / _DimensionsX,
                _TopTextureTileY * (IN.worldPos.z - IN.localPos.z) / _DimensionsZ
            );
        }
        
        float2 MiddleTextureGlobalUVOffset(Input IN)
        {
            return float2(
                _MiddleTextureTileX * (IN.worldPos.x - IN.localPos.x) / _DimensionsX,
                _MiddleTextureTileY * (IN.worldPos.z - IN.localPos.z) / _DimensionsZ
            );
        }

        float2 BottomTextureGlobalUVOffset(Input IN)
        {
            return float2(
                _BottomTextureTileX * (IN.worldPos.x - IN.localPos.x) / _DimensionsX,
                _BottomTextureTileY * (IN.worldPos.z - IN.localPos.z) / _DimensionsZ
            );
        }
        
        float2 TopTextureScroll() { return float2( _TopTextureScrollX, _TopTextureScrollX) * _Time.y; }
        float2 MiddleTextureScroll() { return float2( _MiddleTextureScrollX, _MiddleTextureScrollY) * _Time.y; }
        float2 BottomTextureScroll() { return float2( _BottomTextureScrollX, _BottomTextureScrollY) * _Time.y; }
        
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float percentUp = clamp((IN.localPos.y + IN.amplitude / 2) / (IN.amplitude), 0, 1);
            fixed4 color = percentUp < .5
                ? lerp(
                    tex2D(_BottomTexture, IN.uv_BottomTexture + BottomTextureGlobalUVOffset(IN) + BottomTextureScroll()) * _BottomColor,
                    tex2D(_MiddleTexture, IN.uv_MiddleTexture + MiddleTextureGlobalUVOffset(IN) + MiddleTextureScroll()) * _MiddleColor,
                    percentUp * 2)
                : lerp(
                    tex2D(_MiddleTexture, IN.uv_MiddleTexture + MiddleTextureGlobalUVOffset(IN) + MiddleTextureScroll()) * _MiddleColor,
                    tex2D(_TopTexture, IN.uv_TopTexture + TopTextureGlobalUVOffset(IN) + TopTextureScroll()) * _TopColor,
                    percentUp * 2 - 1);
            float dist = distance(IN.worldPos, _WorldSpaceCameraPos);

            if (_TransparentPart == 0)
            {
                color.a = 1 - color.a;
            }

            if (_TransparentPart == 1)
            {
                if (dist < _OpaqueRadius)
                {
                    color.a = 1;
                }
                else color.a = 1 - saturate((dist - _OpaqueRadius) / _BlurRadius);
            }
            
            o.Emission = color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Alpha = color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}