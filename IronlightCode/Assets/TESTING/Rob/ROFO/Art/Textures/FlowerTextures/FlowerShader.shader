// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlowerShader"
{
	Properties
	{
		_DefaultMaterial_Base_Color("DefaultMaterial_Base_Color", 2D) = "white" {}
		_DefaultMaterial_Emissive("DefaultMaterial_Emissive", 2D) = "white" {}
		_DefaultMaterial_Normal("DefaultMaterial_Normal", 2D) = "white" {}
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

		uniform sampler2D _DefaultMaterial_Normal;
		uniform float4 _DefaultMaterial_Normal_ST;
		uniform sampler2D _DefaultMaterial_Base_Color;
		uniform float4 _DefaultMaterial_Base_Color_ST;
		uniform sampler2D _DefaultMaterial_Emissive;
		uniform float4 _DefaultMaterial_Emissive_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_DefaultMaterial_Normal = i.uv_texcoord * _DefaultMaterial_Normal_ST.xy + _DefaultMaterial_Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _DefaultMaterial_Normal, uv_DefaultMaterial_Normal ) );
			float2 uv_DefaultMaterial_Base_Color = i.uv_texcoord * _DefaultMaterial_Base_Color_ST.xy + _DefaultMaterial_Base_Color_ST.zw;
			o.Albedo = tex2D( _DefaultMaterial_Base_Color, uv_DefaultMaterial_Base_Color ).rgb;
			float2 uv_DefaultMaterial_Emissive = i.uv_texcoord * _DefaultMaterial_Emissive_ST.xy + _DefaultMaterial_Emissive_ST.zw;
			o.Emission = tex2D( _DefaultMaterial_Emissive, uv_DefaultMaterial_Emissive ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
479;116.5;889;735;1283.283;433.6639;1.893903;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-710.7228,-294.4789;Inherit;True;Property;_DefaultMaterial_Base_Color;DefaultMaterial_Base_Color;0;0;Create;True;0;0;False;0;-1;dfa6db2d03039824982bb6091539e371;dfa6db2d03039824982bb6091539e371;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-706.6375,-98.3764;Inherit;True;Property;_DefaultMaterial_Normal;DefaultMaterial_Normal;3;0;Create;True;0;0;False;0;-1;332017c4b0404c244a3c19783fca2947;332017c4b0404c244a3c19783fca2947;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-702.5519,101.8115;Inherit;True;Property;_DefaultMaterial_Emissive;DefaultMaterial_Emissive;1;0;Create;True;0;0;False;0;-1;7fc06d98a2fc3ff45afc4b9d49ef14e3;7fc06d98a2fc3ff45afc4b9d49ef14e3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-694.3808,306.0849;Inherit;True;Property;_DefaultMaterial_Height;DefaultMaterial_Height;2;0;Create;True;0;0;False;0;-1;31809dee62879724585f52564a7276ce;31809dee62879724585f52564a7276ce;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;FlowerShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;1;4;0
WireConnection;0;2;2;0
ASEEND*/
//CHKSM=33A7A99B85A2A7BA0134B2D133BA8C61023C6EC1