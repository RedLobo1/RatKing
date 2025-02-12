Shader "Custom/Metaball2D_Sprite"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1) // Color of the metaballs
        _Threshold ("Threshold", Range(0, 2)) = 1.0
        _Smoothness ("Smoothness", Range(0, 0.5)) = 0.1
        _Radius ("Radius", Float) = 0.5
        _MetaballCount ("Metaball Count", Float) = 3
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Blend SrcAlpha OneMinusSrcAlpha // Alpha blending for smooth edges
        ZWrite Off // Disable depth writing (needed for transparency)
        Cull Off // Render both sides

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Properties
            float4 _Color;
            float _Threshold;
            float _Smoothness;
            float _Radius;
            int _MetaballCount;
            float2 _Metaballs[10]; // Up to 10 metaballs

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float MetaballField(float2 uv)
            {
                float field = 0.0;
                for (int i = 0; i < _MetaballCount; i++)
                {
                    float2 pos = _Metaballs[i];
                    float dist = distance(uv, pos);
                    field += (_Radius * _Radius) / (dist * dist + 0.01);
                }
                return field;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float field = MetaballField(IN.uv);
                float alpha = smoothstep(_Threshold - _Smoothness, _Threshold + _Smoothness, field);
                
                // Apply sprite color and transparency
                return half4(_Color.rgb, alpha);
            }
            ENDHLSL
        }
    }
}
