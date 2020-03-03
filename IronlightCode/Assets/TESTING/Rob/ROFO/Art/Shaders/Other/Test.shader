Shader "Custom/Test"
{
    Properties
	{
		_AlbedoColour("Albedo Colour", Color) = (0,0,0,0)
		_Emission("Emission", 2D) = "white" {}
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_EmissionIntensity("Emission Intensity", Range( 0 , 10)) = 0
		_Opacity("Opacity", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _AlbedoColour;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float _EmissionIntensity;
		uniform float4 _EmissionColour;
		uniform float _Opacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _AlbedoColour.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float4 Emission10 = ( tex2D( _Emission, uv_Emission ) * _EmissionIntensity * _EmissionColour );
			o.Emission = Emission10.rgb;
			o.Alpha = _Opacity;
		}

		ENDCG
    }
    FallBack "Diffuse"
}
