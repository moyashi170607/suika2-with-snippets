Shader "PolarisEngine/NormalShader"
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

	    StructuredBuffer<appdata> _Input;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (uint id : SV_VertexID, uint inst : SV_InstanceID)
            {
                v2f o;
                o.vertex = _Input[id].vertex;
                o.uv = _Input[id].uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(1.0, 1.0, 1.0, 1.0);
                return col;
            }

            ENDCG
        }
    }
}
