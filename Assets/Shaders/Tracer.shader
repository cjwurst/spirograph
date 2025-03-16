Shader "Unlit/Tracer"
{
    Properties
    {
        _MainTex("Color (RGB) Alpha (A)", 2D) = "white" {}

        _IsDrawing("Is Drawing", Integer) = 0
        _X("X", Float) = 0.0
        _Y("Y", Float) = 0.0
        _DX("DX", Float) = 0.0
        _DY("DY", Float) = 0.0
        _L("L", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZTest Always Cull Off ZWrite Off
        Fog { Mode off }
        Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _MainTex_ST;

            uniform int _IsDrawing = 0;
            uniform float _X = 0.0;
            uniform float _Y = 0.0;
            uniform float _DX = 0.0;
            uniform float _DY = 0.0;
            uniform float _L = 0.0;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float u = i.uv[0];
                float v = i.uv[1];
                float2 pos = float2(u - 0.5 - _X/2, v - 0.5 - _Y/2);
                float dot = pos.x * _DX + pos.y * _DY;
                float2 diff = float2(pos.x - dot * _DX, pos.y - dot * _DY);
                if ((pow(diff.x, 2) + pow(diff.y, 2) < 0.000002) && (0 < dot) && (dot < _L))
                    col = float4(1.0, 1.0, 1.0, 1.0);
                else if (!_IsDrawing) col = float4(0.0, 0.0, 0.0, 0.0);
                return col;
            }
            ENDCG
        }
    }
}
