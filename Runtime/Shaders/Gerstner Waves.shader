Shader "Custom/Waves/Gerstner Waves"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Cull Mode", Float) = 2.0

        _TopColor ("Top Color", Color) = (1,1,1,1)
        _MiddleColor ("Middle Color", Color) = (1,1,1,1)
        _BottomColor ("Bottom Color", Color) = (1,1,1,1)
		_ColorRange ("Color Range", Float) = 1
		_ColorOffset("Color Offset",Float) = 0.0
        
        _TopTexture ("Top Texture", 2D) = "white" {}
        _TopTextureScrollX("Top Texture Scroll X", Float) = 0
        _TopTextureScrollY("Top Texture Scroll Y", Float) = 0
        _MiddleTexture ("Middle Texture", 2D) = "white" {}
        _MiddleTextureScrollX("Middle Texture Scroll X", Float) = 0
        _MiddleTextureScrollY("Middle Texture Scroll Y", Float) = 0
        _BottomTexture ("Bottom Texture", 2D) = "white" {}
        _BottomTextureScrollX("Bottom Texture Scroll X", Float) = 0
        _BottomTextureScrollY("Bottom Texture Scroll Y", Float) = 0
        
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [MaterialToggle] _UseWaveA("Use Wave A", Float) = 0
        _WaveARotation("Wave A Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveASteepness("Wave A Steepness", Float) = .25
        _WaveAWavelength("Wave A Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveB("Use Wave B", Float) = 0
        _WaveBRotation("Wave B Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveBSteepness("Wave B Steepness", Float) = .25
        _WaveBWavelength("Wave B Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveC("Use Wave C", Float) = 0
        _WaveCRotation("Wave C Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveCSteepness("Wave C Steepness", Float) = .25
        _WaveCWavelength("Wave C Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveD("Use Wave D", Float) = 0
        _WaveDRotation("Wave D Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveDSteepness("Wave D Steepness", Float) = .25
        _WaveDWavelength("Wave D Wavelength", Float) = 5
        
        [MaterialToggle] _UseWaveE("Use Wave E", Float) = 0
        _WaveERotation("Wave E Rotation", Range(-3.1459, 3.1459)) = 0
        _WaveESteepness("Wave E Steepness", Float) = .25
        _WaveEWavelength("Wave E Wavelength", Float) = 5
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" "RenderType"="Opaque"
        }
        LOD 200

        Cull [_CullMode]
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade
        #pragma target 3.0

        struct Input
        {
            float2 uv_TopTexture : TEXCOORD0;
            float3 worldPos : TEXCOORD1;
            float3 localPos : TEXCOORD2;
        };

		float4 _TopColor;
		float4 _MiddleColor;
        float4 _BottomColor;
		float _ColorRange;
		float _ColorOffset;

        sampler2D _TopTexture, _MiddleTexture, _BottomTexture;
        float _TopTextureScrollX, _TopTextureScrollY;
        float _MiddleTextureScrollX, _MiddleTextureScrollY;
        float _BottomTextureScrollX, _BottomTextureScrollY;

        half _Glossiness;
        half _Metallic;
        float _UseWaveA, _UseWaveB, _UseWaveC, _UseWaveD, _UseWaveE;
        float _WaveARotation, _WaveBRotation, _WaveCRotation, _WaveDRotation, _WaveERotation;
        float _WaveASteepness, _WaveBSteepness, _WaveCSteepness, _WaveDSteepness, _WaveESteepness;
        float _WaveAWavelength, _WaveBWavelength, _WaveCWavelength, _WaveDWavelength, _WaveEWavelength;

        float3 GerstnerWave(float rotation, float steepness, float wavelength, float2 worldPositionXZ, inout float3 tangent, inout float3 binormal)
        {
            float k = 2 * UNITY_PI / wavelength;
            float c = sqrt(9.8 / k);
            float2 direction = float2(cos(rotation), sin(rotation));
            float f = k * (dot(direction, worldPositionXZ) - c * _Time.y);
            float a = steepness / k;

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
            return float3(
                direction.x * (a * cos(f)),
                a * sin(f),
                direction.y * (a * cos(f))
            );
        }

        void vert(inout appdata_full vertexData, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            float3 worldPosition = vertexData.vertex.xyz;
            float3 tangent = float3(1, 0, 0);
            float3 binormal = float3(0, 0, 1);
            float3 p = worldPosition;
            if (_UseWaveA == 1) p += GerstnerWave(_WaveARotation, _WaveASteepness, _WaveAWavelength, worldPosition.xz, tangent, binormal);
            if (_UseWaveB == 1) p += GerstnerWave(_WaveBRotation, _WaveBSteepness, _WaveBWavelength, worldPosition.xz, tangent, binormal);
            if (_UseWaveC == 1) p += GerstnerWave(_WaveCRotation, _WaveCSteepness, _WaveCWavelength, worldPosition.xz, tangent, binormal);
            if (_UseWaveD == 1) p += GerstnerWave(_WaveDRotation, _WaveDSteepness, _WaveDWavelength, worldPosition.xz, tangent, binormal);
            if (_UseWaveE == 1) p += GerstnerWave(_WaveERotation, _WaveESteepness, _WaveEWavelength, worldPosition.xz, tangent, binormal);
            vertexData.vertex.xyz = p;
            vertexData.normal = normalize(cross(binormal, tangent));
            o.localPos = p - float3(0, _ColorOffset, 0);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float percentUp = clamp(IN.localPos.y / _ColorRange, 0, 1);
            fixed4 color = percentUp < .5
                ? lerp(
                    tex2D(_BottomTexture, IN.uv_TopTexture + float2(_BottomTextureScrollX, _BottomTextureScrollY) * _Time.y) * _BottomColor,
                    tex2D(_MiddleTexture, IN.uv_TopTexture + float2(_MiddleTextureScrollX, _MiddleTextureScrollY) * _Time.y) * _MiddleColor,
                    percentUp * 2)
                : lerp(
                    tex2D(_MiddleTexture, IN.uv_TopTexture + float2(_MiddleTextureScrollX, _MiddleTextureScrollY) * _Time.y) * _MiddleColor,
                    tex2D(_TopTexture, IN.uv_TopTexture + float2(_TopTextureScrollX, _TopTextureScrollY) * _Time.y) * _TopColor,
                    percentUp * 2 - 1);
            
            o.Emission = color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}