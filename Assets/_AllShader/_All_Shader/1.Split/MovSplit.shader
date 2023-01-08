Shader "LSQ/ImageProcessing/PerLineScan"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(Up,1,Down,2,Left,3,Right,4)] _ScanDirection ("Scan Direction", Int) = 1 
        _Value ("Value", Range(0, 1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            int _ScanDirection;
            float _Value;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                int lineIndex = 1;
                int valueIndex = 0;

                if(_ScanDirection == 1) //向上
                {
                    lineIndex = i.uv.y * _MainTex_TexelSize.w;
                    valueIndex = _Value * _MainTex_TexelSize.w;
                }
                else if(_ScanDirection == 2) //向下
                {
                    lineIndex = (1 - i.uv.y) * _MainTex_TexelSize.w;
                    valueIndex = _Value * _MainTex_TexelSize.w;
                }
                else if(_ScanDirection == 3) //向左
                {
                    lineIndex = i.uv.x * _MainTex_TexelSize.z;
                    valueIndex = _Value * _MainTex_TexelSize.z;
                }
                else //向右
                {
                    lineIndex = (1 - i.uv.x) * _MainTex_TexelSize.z;
                    valueIndex = _Value * _MainTex_TexelSize.z;
                }

                clip(lineIndex - valueIndex);

                return col;
            }
            ENDCG
        }
    }
}