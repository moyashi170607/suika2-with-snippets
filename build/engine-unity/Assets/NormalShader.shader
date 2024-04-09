Shader "Unlit/sampleUnit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        // 基本Opaque、半透明ならTransparent(?)
        Tags { "RenderType"="Opaque" }

        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert 
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = v.vertex;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(1.0, 1.0, 1.0, 1.0);
                return col;
            }
            ENDCG
        }
    }
}
