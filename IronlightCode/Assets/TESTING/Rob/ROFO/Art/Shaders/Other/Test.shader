Shader "Custom/Test"
{
    Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 0
		_OpacityMap("Opacity Map", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _NormalIntensity;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _OpacityMap;
		uniform float4 _OpacityMap_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode20 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 appendResult24 = (float4(( (tex2DNode20).xy * _NormalIntensity ) , tex2DNode20.b , 0.0));
			float4 Normal25 = appendResult24;
			o.Normal = Normal25.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 Albedo16 = tex2D( _Albedo, uv_Albedo );
			o.Albedo = Albedo16.rgb;
			float2 uv_OpacityMap = i.uv_texcoord * _OpacityMap_ST.xy + _OpacityMap_ST.zw;
			float4 Opacity12 = tex2D( _OpacityMap, uv_OpacityMap );
			o.Alpha = Opacity12.r;
		}

		ENDCG
    }
    FallBack "Diffuse"
}
