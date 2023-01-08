Shader "Unlit/TextuShow"
{
    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}
        _TransitionTex("_TransitionTex",2D) = "white"{}
        _Offset("_Offset",Vector) = (0, 0, 0, 0)
        _Value ("Value", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        CULL Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

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
            float4 _MainTex_TexelSize;
            sampler2D _TransitionTex;
            float4 _TransitionTex_TexelSize;
          
            float2 _Offset;
            float _Value;
            

           
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                float2 fixedUV;
                fixedUV.x = _TransitionTex_TexelSize.w * i.uv.x / _MainTex_TexelSize.w;
                fixedUV.y = _TransitionTex_TexelSize.z * i.uv.y / _MainTex_TexelSize.z;
                fixedUV += _Offset;
                float mask = tex2D(_TransitionTex,saturate(fixedUV)).r;
                if(mask <_Value || mask == 1 && _Value == 1)
                {
                    return fixed4(0,0,0,1);
                }
                
                
                return col;
            }
            ENDCG
        }
    }
}
