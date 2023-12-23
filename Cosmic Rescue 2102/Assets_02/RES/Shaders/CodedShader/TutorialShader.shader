// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Quryss/TutorialShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _D2C_Value ("D2C Value", float) = 0
        _elevationMinMax ("ElevationMinMax", vector) = (0,0,0,0) 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float inverseLerp(float a, float b, float value) {
                return saturate((value - a) / (b - a));
            }

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
            float4 _Color;
            float4 _elevationMinMax;
            float Emin;
            float Emax;
            float _D2C_Value;
            float distanceToCenter;

            void SetDistance2Center(float val)
            {
                _D2C_Value = val;              
            }

            v2f vert(appdata v)  
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                SetDistance2Center(100);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                   float3 objectPos = i.vertex.xyz;
                   float3 centerObjectPos = float3(0, 0, 0);
                   float3 vectorToCenter = objectPos.xyz - centerObjectPos.xyz;

                   distanceToCenter = length(i.vertex.xyz) + 1;
                   SetDistance2Center(distanceToCenter);           
                   //distanceToCenter = normalize(distance(i.vertex.xyz,unity_ObjectToWorld[3].xyz));
                   
                   Emin =  _elevationMinMax.x;
                   Emax =  _elevationMinMax.y;
               
                   float t = normalize(inverseLerp(Emin,Emax, distanceToCenter)); 
                   //float t = saturate((distanceToCenter - Emin) / (Emax - Emin));
                   //float t = lerp(Emin,Emax,distanceToCenter);
                   
                   //float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color + t ;

                    //o.Albedo = t;
                    //o.Metallic = _Metallic;
                    //o.Smoothness = _Glossiness;
                    //o.Alpha = c.a;
                 

                fixed4 col = t * _Color;//*tex2D(_MainTex,i.uv) + _Color;
                return col;
            }




            ENDCG
        }
    }
    FallBack "Diffuse"
}
