Shader "Custom/Hologram" {
    Properties{
        _MainTex("Albedo", 2D) = "white" {}
        _BumpMap("BumpMap", 2D) = "bump" {}
        _RimColor("RimColor", Color) = (1,1,1,1)
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "RenderType" = "Qpaque" }

            CGPROGRAM
            #pragma surface surf Lambert noambient alpha:fade

            sampler2D _MainTex;
            sampler2D _BumpMap;
            struct Input {
                float2 uv_MainTex;
                float2 uv_BumpMap;
                float3 viewDir;
                float3 worldPos;
            };

            float4 _RimColor;

            void surf(Input In, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, In.uv_MainTex);
                o.Albedo = c.rgb;
                o.Normal = UnpackNormal(tex2D(_BumpMap, In.uv_BumpMap));
                o.Emission = _RimColor;
                float rim = saturate(dot(o.Normal, In.viewDir));
                rim = pow(1 - rim, 2) + pow(frac(In.worldPos.z * 2 - _Time.y), 10);
                o.Alpha = rim;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
