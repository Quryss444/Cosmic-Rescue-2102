Shader "Quryss/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _VertexPosition ("Vertex Position", Range(-10,10)) = 0.0
        _elevationMinMax ("ElevationMinMax", Vector) = (0,0,0,0)
        _CurrentLength ("Current Length", float) = 0.0
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
        float4 _elevationMinMax;
        float _CurrentLength;
        float _VertexPosition;
        //float4 _MainTex_ST;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float Emin;
        float Emax;
        float wt;

        struct Input
        {
            float2 uv_MainTex;
            float3 ObjectPos;
            float3 WorldPos;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : POSITION;
        };

        float inverseLerp(float a, float b, float value)
        {
            return saturate((value - a) / (b - a));
        }

        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
  
        fixed4 surf(Input IN,inout SurfaceOutputStandard o)
        {      
            fixed4 k = _Color;//tex2D (_MainTex, IN.uv_MainTex) * (_Color);
            
            //o.Albedo = tex2D (_MainTex, IN.uv_MainTex) * _Color.rgb ;//* wt ;
            
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = k.a;           
            //wt += 0.02;
            return k;
        }

         v2f vert (v2f v,inout Input inn)
        {
            v2f o;
            v.vertex = UnityObjectToClipPos(v.vertex);
            v.vertex += _VertexPosition;
            o.uv = v.uv;
            return o;
        }       

        fixed4 frag (Input IN,out v2f i,inout SurfaceOutputStandard o) : SV_Target
        {
            
            float3 objectPos = i.vertex;
            float3 centerObjectPos = float3(0, 0, 0);
            float3 vectorToCenter = objectPos - centerObjectPos;
            float  distanceToCenter = normalize(length(vectorToCenter));
            
            Emin =  _elevationMinMax.x;
            Emax =  _elevationMinMax.y;

            float t = inverseLerp(Emin, Emax, distanceToCenter); 
            fixed4 col = _Color * (t);        

            return col;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
