Shader "Quryss/PlutoLike"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,0)
        _MainTex ("Albedo (RGB)", 2D) = "Black" {} 
        _Glossiness ("Smoothness", Range(0,1)) = 0.0
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ColorAdjuster ("ColorAdjuster", Range(0,1)) = 1
        _VertexPosition ("VertexPosition", vector) = (0,0,0,0)
        _ElevationMinMax ("ElevationMinMax", vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float4 _elevationMinMax;
            float2 uv_MainTex;
            float3 objectPos : POSITION;
            float3 ObjectPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _ColorAdjuster;
        float4 _VertexPosition;
        float4 _ElevationMinMax;
        float3 objectPosition;
        float4 _elevationMinMax;
        float Emin;
        float Emax;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        float inverseLerp(float a, float b, float value) {
            return saturate((value - a) / (b - a));
        }

        SurfaceOutputStandard surf (Input IN, inout SurfaceOutputStandard o)
        {
           float3 objectPos = IN.objectPos;
           float3 centerObjectPos = float3(0, 0, 0);
           float3 vectorToCenter = objectPos - centerObjectPos;

           float  distanceToCenter = length(vectorToCenter);           

           _ElevationMinMax = IN._elevationMinMax;
           
           Emin =  IN._elevationMinMax.x;
           Emax =  IN._elevationMinMax.y;
               
           float t = inverseLerp(Emin,Emax, distanceToCenter); 
            //float t = lerp(Emin,Emax,distanceToCenter);

           //float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color + t ;

            o.Albedo = t;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
            return o;
        }

        fixed4 frag (Input IN)
	    {

           float3 objectPos = IN.objectPos;
           float3 centerObjectPos = float3(0, 0, 0);
           float3 vectorToCenter = objectPos - centerObjectPos;

           float  distanceToCenter = length(vectorToCenter);           

           _ElevationMinMax = IN._elevationMinMax;
           
           Emin =  IN._elevationMinMax.x;
           Emax =  IN._elevationMinMax.y;
               
           float t = inverseLerp(Emin,Emax, distanceToCenter); 
            //float t = lerp(Emin,Emax,distanceToCenter);

           float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color + t ;


				fixed4 col = t * _Color;
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
		}
        ENDCG
    }
    FallBack "Diffuse"
}
