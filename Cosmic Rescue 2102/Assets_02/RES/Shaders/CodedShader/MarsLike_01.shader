Shader "Quryss/MarsLike_01"
{
    Properties
    {   //Get Directives to Manage all this 
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.0
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _VertexPosition ("Vertex Position", Range(-1000,1000)) = 0.0

        _Radius ("Sphere Radius", Range (1, 10)) = 5
        _Frequency ("Wave Frequency", Range (0.1, 5)) = 1
        _Amplitude ("Wave Amplitude", Range (0.1, 5)) = 1
       
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float Emin;
        float Emax;

        float _VertexPosition;
        float _Radius;

        struct Input {
            float2 _MainTex;
            float2 uv_MainTex;
            float3 worldPos;
            float3 objectPos : POSITION;
            float2 _elevationMinMax;
        };

        struct appdata
        {
            float4 vertex : POSITION;   // Vertex position in object space
            float3 normal : NORMAL;     // Vertex normal
        };

        struct v2f {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float inverseLerp(float a, float b, float value) {
            return saturate((value - a) / (b - a));
        }

        void ModifyVertexPosition(Input IN,float3 centerObjectPos, inout float3 vertexPosition, float vertexPositionStrength)
        {
            float3 vectorToCenter = vertexPosition - centerObjectPos;
            float distanceToCenter = length(vectorToCenter);

            vertexPosition += vectorToCenter * _VertexPosition * vertexPositionStrength;
        }
        
        void vertex(inout appdata_full v)
        {
            v2f o;

            v.vertex *= _VertexPosition * sin(_Time.y) * float4(-1, -1, -1, -1);
            o.vertex = v.vertex;
        }

        void surf (Input IN,inout SurfaceOutputStandard o) {
            
            float3 objectPos = IN.objectPos;
            float3 centerObjectPos = float3(0, 0, 0);
            float3 vectorToCenter = IN.objectPos - centerObjectPos;
            float  distanceToCenter = length(vectorToCenter);
            
            Emin =  IN._elevationMinMax.x;
            Emax =  IN._elevationMinMax.y;
               
            float t = inverseLerp(Emin, Emax, distanceToCenter); 
           
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color * _Radius;
            o.Albedo = _Color * (1/t);

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
