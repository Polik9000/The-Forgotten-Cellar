Shader "UI/AngularFillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FillAmount ("Fill Amount", Range(0,1)) = 1
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _FillAmount;
            fixed4 _Color;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float2 dir = i.uv - center;
                float angle = atan2(dir.y, dir.x);
                if (angle < 0) angle += 6.2831853; // 2*PI

                float normalizedAngle = angle / 6.2831853;
                float dist = length(dir);

                if (normalizedAngle > _FillAmount)
                    discard;

                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.a *= 0.75;
                return col;
            }
            ENDCG
        }
    }
}
