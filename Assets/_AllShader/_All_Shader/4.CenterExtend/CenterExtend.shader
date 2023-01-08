Shader "LSQ/ImageProcessing/CenterExtend"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(Horizontal,1,Vertical,2)] _Direction ("Direction", Int) = 1
        [Enum(Extend,1,Shrink,2)] _Type ("Direction", Int) = 1
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

            int _Direction;
            int _Type;
            float _Value;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float value = 1;

                if(_Direction == 1) //水平
                {
                    if(_Type == 1) //扩张
                    {
                        value = abs(i.uv.x - 0.5) * 2;
                    }
                    else //收缩
                    {
                        value = 1 - abs(i.uv.x - 0.5) * 2;
                    }
                }
                else if(_Direction == 2) //竖直
                {
                    if(_Type == 2) //扩张
                    {
                        value = 1 - abs(i.uv.y - 0.5) * 2; 
                    }
                    else //收缩
                    {
                        value = abs(i.uv.y - 0.5) * 2;
                    }
                }

                clip(_Value - value);

                return col;
            }
            ENDCG
        }
    }
}


