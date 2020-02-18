Shader "Unlit/MoonBillboard"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "white" {}
		_EmissionMap("EmissionMap", 2D) = "white" {}
		_EColor("EmissionColour", Color) = (0,0,0,0)
		_EIntensity("EmissionIntensity", float) = 1
		_AmbientOcclusion("AmbientOcclusion", 2D) = "white" {}
		_AOIntensity("OcclusionIntensity", float) = 0.5
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

		ZWrite Off
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
				float3 normal : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			//added
			sampler2D _NormalMap;
			sampler2D _EmissionMap;
			half4 _EColor;
			float _EIntensity;
			sampler2D _AmbientOcclusion;
			float _AOIntensity;

			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv.xy;

				// billboard mesh towards camera
				float3 vpos = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
				float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1);
				float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
				float4 outPos = mul(UNITY_MATRIX_P, viewPos);
				o.normal = v.normal;
				o.pos = outPos;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 e = tex2D(_EmissionMap, i.uv) * _EColor;
				col += fixed4(e.rbg,0);
				return col;
			}
			ENDCG
		}
	}
}
