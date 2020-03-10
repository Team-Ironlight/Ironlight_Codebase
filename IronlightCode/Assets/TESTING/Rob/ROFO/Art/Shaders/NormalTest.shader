// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NormalTest"
{
	Properties
	{
		_Rough_Water_normal("Rough_Water_normal", 2D) = "white" {}
		_Rough_Water_basecolor("Rough_Water_basecolor", 2D) = "white" {}
		_NormalStrength("NormalStrength", Range( 0 , 10)) = 0
		_Rough_Water_metallic("Rough_Water_metallic", 2D) = "white" {}
		_Rough_Water_AO("Rough_Water_AO", 2D) = "white" {}
		_PannerX("PannerX", Float) = 0
		_PannerY("PannerY", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Rough_Water_normal;
		uniform float _PannerX;
		uniform float _PannerY;
		uniform float _NormalStrength;
		uniform sampler2D _Rough_Water_basecolor;
		uniform sampler2D _Rough_Water_metallic;
		uniform float4 _Rough_Water_metallic_ST;
		uniform sampler2D _Rough_Water_AO;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 appendResult11 = (float4(_PannerX , ( _PannerY * _SinTime.x ) , 0.0 , 0.0));
			float2 panner8 = ( _SinTime.x * appendResult11.xy + i.uv_texcoord);
			o.Normal = ( tex2D( _Rough_Water_normal, panner8 ) * _NormalStrength );
			o.Albedo = tex2D( _Rough_Water_basecolor, panner8 ).rgb;
			float2 uv_Rough_Water_metallic = i.uv_texcoord * _Rough_Water_metallic_ST.xy + _Rough_Water_metallic_ST.zw;
			o.Metallic = tex2D( _Rough_Water_metallic, uv_Rough_Water_metallic ).r;
			o.Occlusion = tex2D( _Rough_Water_AO, panner8 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
478;454;889;377;1221.857;-172.3011;1.3;True;False
Node;AmplifyShaderEditor.RangedFloatNode;10;-1310.253,277.5887;Inherit;False;Property;_PannerY;PannerY;6;0;Create;True;0;0;False;0;0;0.58;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;12;-1309.632,373.5079;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1191.246,201.7487;Inherit;False;Property;_PannerX;PannerX;5;0;Create;True;0;0;False;0;0;-0.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1134.055,322.249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1063.96,75.28027;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;11;-973.5565,213.7314;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;8;-766.6691,79.49718;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-507.321,58.17252;Inherit;True;Property;_Rough_Water_normal;Rough_Water_normal;0;0;Create;True;0;0;False;0;-1;5372cc374b8da3f4b8fe0ae66e7e9589;5372cc374b8da3f4b8fe0ae66e7e9589;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-486.0364,263.2108;Inherit;False;Property;_NormalStrength;NormalStrength;2;0;Create;True;0;0;False;0;0;2.79;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-191.0601,65.30325;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;5;-501.1647,367.963;Inherit;True;Property;_Rough_Water_metallic;Rough_Water_metallic;3;0;Create;True;0;0;False;0;-1;e706bc86cf72cca448ae2804435674b7;e706bc86cf72cca448ae2804435674b7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-508.067,578.8745;Inherit;True;Property;_Rough_Water_AO;Rough_Water_AO;4;0;Create;True;0;0;False;0;-1;d409fe4f7de780a43b2085b6bb831cbe;d409fe4f7de780a43b2085b6bb831cbe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-502.2815,-180.7659;Inherit;True;Property;_Rough_Water_basecolor;Rough_Water_basecolor;1;0;Create;True;0;0;False;0;-1;8b70714da22ac3444849b8febcc9a04f;8b70714da22ac3444849b8febcc9a04f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;NormalTest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;10;0
WireConnection;13;1;12;1
WireConnection;11;0;9;0
WireConnection;11;1;13;0
WireConnection;8;0;7;0
WireConnection;8;2;11;0
WireConnection;8;1;12;1
WireConnection;1;1;8;0
WireConnection;3;0;1;0
WireConnection;3;1;4;0
WireConnection;6;1;8;0
WireConnection;2;1;8;0
WireConnection;0;0;2;0
WireConnection;0;1;3;0
WireConnection;0;3;5;1
WireConnection;0;5;6;0
ASEEND*/
//CHKSM=17750F93DB25416BA3CA1CBDA9F0FA779FF6A31B