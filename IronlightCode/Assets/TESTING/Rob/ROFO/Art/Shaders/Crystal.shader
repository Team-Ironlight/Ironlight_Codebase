// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Crystal"
{
	Properties
	{
		_21092_Crystal_Albedo("21092_Crystal_Albedo", 2D) = "white" {}
		_21090_Crystal_Emission("21090_Crystal_Emission", 2D) = "white" {}
		_62202_Crystal_Normal("62202_Crystal_Normal", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_EmissionIntensity("Emission Intensity", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _62202_Crystal_Normal;
		uniform float4 _62202_Crystal_Normal_ST;
		uniform sampler2D _21092_Crystal_Albedo;
		uniform float4 _21092_Crystal_Albedo_ST;
		uniform sampler2D _21090_Crystal_Emission;
		uniform float4 _21090_Crystal_Emission_ST;
		uniform float _EmissionIntensity;
		uniform float4 _EmissionColour;
		uniform float _Metallic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_62202_Crystal_Normal = i.uv_texcoord * _62202_Crystal_Normal_ST.xy + _62202_Crystal_Normal_ST.zw;
			o.Normal = tex2D( _62202_Crystal_Normal, uv_62202_Crystal_Normal ).rgb;
			float2 uv_21092_Crystal_Albedo = i.uv_texcoord * _21092_Crystal_Albedo_ST.xy + _21092_Crystal_Albedo_ST.zw;
			o.Albedo = tex2D( _21092_Crystal_Albedo, uv_21092_Crystal_Albedo ).rgb;
			float2 uv_21090_Crystal_Emission = i.uv_texcoord * _21090_Crystal_Emission_ST.xy + _21090_Crystal_Emission_ST.zw;
			o.Emission = ( ( tex2D( _21090_Crystal_Emission, uv_21090_Crystal_Emission ) * _EmissionIntensity ) * _EmissionColour ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;2240.474;98.12817;1.968001;True;False
Node;AmplifyShaderEditor.RangedFloatNode;8;-1156.293,24.93712;Inherit;False;Property;_EmissionIntensity;Emission Intensity;6;0;Create;True;0;0;False;0;0;4.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1590.61,-127.0573;Inherit;True;Property;_21090_Crystal_Emission;21090_Crystal_Emission;1;0;Create;True;0;0;False;0;-1;c719623e01d9cdd48a40fad3e1c13fd1;c719623e01d9cdd48a40fad3e1c13fd1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-908.7191,-44.81695;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-905.1226,218.7372;Inherit;False;Property;_EmissionColour;Emission Colour;5;0;Create;True;0;0;False;0;0,0,0,0;1,0.01568627,0.919493,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1592.31,-536.7646;Inherit;True;Property;_21092_Crystal_Albedo;21092_Crystal_Albedo;0;0;Create;True;0;0;False;0;-1;aa0e0c87cc6861c43be196d09b498bbc;aa0e0c87cc6861c43be196d09b498bbc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;10;-1904.741,650.1277;Inherit;True;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;3;-1586.989,-334.5714;Inherit;True;Property;_62202_Crystal_Normal;62202_Crystal_Normal;2;0;Create;True;0;0;False;0;-1;253d701ada0a152428bac4449288a5d7;253d701ada0a152428bac4449288a5d7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;93.49585,108.9387;Inherit;False;Property;_Metallic;Metallic;4;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1605.644,286.5012;Inherit;True;Constant;_Float0;Float 0;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;20;-1319.004,423.288;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;101.7284,341.8002;Inherit;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-635.4502,-16.47562;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;21;-893.5217,504.4268;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1317.026,664.7247;Inherit;False;Property;_DotMin;DotMin;7;0;Create;True;0;0;False;0;0;0.48;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1218.077,902.2039;Inherit;False;Constant;_Float1;Float 1;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;14;-1621.485,541.0571;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;11;-1908.544,417.6236;Inherit;True;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-584.7983,601.3969;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;24;-857.8989,773.5695;Inherit;False;Property;_RimColour;RimColour;8;0;Create;True;0;0;False;0;1,0,0,0;0,1,0.9608455,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;441.3149,-45.51678;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Crystal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;2;0
WireConnection;9;1;8;0
WireConnection;20;0;19;0
WireConnection;20;1;14;0
WireConnection;7;0;9;0
WireConnection;7;1;6;0
WireConnection;21;0;20;0
WireConnection;21;1;23;0
WireConnection;21;2;22;0
WireConnection;14;0;11;0
WireConnection;14;1;10;0
WireConnection;25;0;21;0
WireConnection;25;1;24;0
WireConnection;0;0;1;0
WireConnection;0;1;3;0
WireConnection;0;2;7;0
WireConnection;0;3;5;0
WireConnection;0;4;4;0
ASEEND*/
//CHKSM=9D7BEDDD0ECC87C92934F4AD2B5D3B5B0D44D97D