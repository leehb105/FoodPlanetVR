Shader "Custom/Cartoon_Ceiling"
{

    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}

        _SpecularMap("Specular Map", 2D) = "white" {}
        _SpecularColor("Specular Color", Color) = (1,1,1,1)
        _SpecularIntensity("Specular Intensity", Float) = 50

        _FresnelColor("Fresnel Color", Color) = (1,1,1,1)
        _FresnelIntensity("Fresnel Intensity", Float) = 5
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" }

            CGPROGRAM

            #pragma surface surf _CustomLight noambient

            sampler2D _MainTex;
            sampler2D _BumpMap;
            sampler2D _SpecularMap;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_BumpMap;
                float2 uv_SpecularMap;
            };

            fixed4 _Color;

            float4 _SpecularColor;
            float _SpecularIntensity;

            float4 _FresnelColor;
            float _FresnelIntensity;

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;

                float3 fNormal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
                o.Normal = fNormal;

                float4 fSpecular = tex2D(_SpecularMap, IN.uv_SpecularMap);
                o.Gloss = fSpecular.a;
            }

            float4 Lighting_CustomLight(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
            {
                //Diffuse
                float3 fDiffuseColor;
                float fNDotL = saturate(dot(lightDir, s.Normal));
                fDiffuseColor = s.Albedo * fNDotL * atten;

                //Specular
                float3 fSpecularColor;
                float3 fReflectVector = reflect(-lightDir, s.Normal);
                float fRDotV = saturate(dot(fReflectVector, viewDir));
                fSpecularColor = pow(fRDotV, _SpecularIntensity) * _SpecularColor.rgb * s.Gloss;

                //Fresnel
                float3 fFresnelColor;
                float fNDotV = dot(s.Normal, viewDir);
                float fInverseRim = 1 - abs(fNDotV);
                fFresnelColor = pow(fInverseRim, _FresnelIntensity) * _FresnelColor.rgb * s.Albedo * 3.0f;


                //! Final Result
                float4 fFinalColor;
                fFinalColor.rgb = fDiffuseColor + fSpecularColor + fFresnelColor;
                fFinalColor.a = s.Alpha;

                return fFinalColor;
            }

            ENDCG
        }
            FallBack "Diffuse"
}

