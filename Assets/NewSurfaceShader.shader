Shader "Custom/InsideOnlySolidSphereShader_URP"
{
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 1) // Default black color
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Cull Front // Cull front faces to make the outside invisible and only render the inside

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPosition : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            float4 _Color;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.worldPosition = TransformObjectToWorld(IN.positionOS);
                OUT.worldNormal = TransformObjectToWorldNormal(IN.normalOS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Render solid black on the inside
                return half4(_Color.rgb, 1.0); // Solid black
            }

            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
