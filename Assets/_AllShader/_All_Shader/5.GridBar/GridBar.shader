Shader "LSQ/ImageProcessing/GridBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(Horizontal,1,Vertical,2)] _Direction ("Direction", Int) = 1
        _Count ("Count", int) = 10
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
            int _Count;
            float _Value;

            fixed4 frag (v2f i) : SV_Target
            {
                if(_Direction == 1) //水平
                {
                    float lineIndex = i.uv.y * _Count;
                    lineIndex = floor(lineIndex);
                    if(lineIndex % 2 == 0)
                    {
                        i.uv.x =  i.uv.x - 1 + _Value;
                    }
                    else
                    {
                        i.uv.x = i.uv.x + 1 - _Value;
                    }
                    
                }
                else if(_Direction == 2) //竖直
                {
                    float lineIndex = i.uv.x * _Count;
                    lineIndex = floor(lineIndex);
                    if(lineIndex % 2 == 0)
                    {
                        i.uv.y = i.uv.y + 1 - _Value;
                    }
                    else
                    {
                        i.uv.y = i.uv.y - 1 + _Value;
                    }

                }

                fixed4 col = tex2D(_MainTex, i.uv);

                if(i.uv.x > 1 || i.uv.y > 1 || i.uv.x < 0 || i.uv.y < 0)
                    return 1;

                return col;
            }
            ENDCG
        }
    }
}


