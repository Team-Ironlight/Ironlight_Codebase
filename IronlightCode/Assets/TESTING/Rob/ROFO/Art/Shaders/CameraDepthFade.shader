// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CameraDepthFade"
{
	Properties
	{
		_FallOff("FallOff", Range( 1 , 100)) = 1
		_Distance("Distance", Range( 0 , 10)) = 0
		_MainTexture("MainTexture", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "white" {}
		_EmissionTexture("EmissionTexture", 2D) = "white" {}
		_EmissionColour("EmissionColour", Color) = (0.7169812,0.6738338,0.05749378,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float eyeDepth;
		};

		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform sampler2D _EmissionTexture;
		uniform float4 _EmissionTexture_ST;
		uniform float4 _EmissionColour;
		uniform float _FallOff;
		uniform float _Distance;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			o.Normal = tex2D( _NormalMap, uv_NormalMap ).rgb;
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			o.Albedo = tex2D( _MainTexture, uv_MainTexture ).rgb;
			float2 uv_EmissionTexture = i.uv_texcoord * _EmissionTexture_ST.xy + _EmissionTexture_ST.zw;
			float cameraDepthFade1 = (( i.eyeDepth -_ProjectionParams.y - _Distance ) / _FallOff);
			o.Emission = ( ( tex2D( _EmissionTexture, uv_EmissionTexture ) * _EmissionColour ) * cameraDepthFade1 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
479;72;888;471;2004.188;402.0616;3.112581;True;False
Node;AmplifyShaderEditor.CommentaryNode;16;-1102.053,226.9981;Inherit;False;826.4079;340.5591;Take a texture and multiply it by a colour to make that texture that colour;3;13;14;15;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;10;-895.8796,689.975;Inherit;False;616.2753;274.4987;Produces a value between 0 and 1 depending on distance to camera;3;2;3;1;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-845.8796,754.0402;Inherit;False;Property;_FallOff;FallOff;0;0;Create;True;0;0;False;0;1;7;1;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-842.3352,849.4738;Inherit;False;Property;_Distance;Distance;1;0;Create;True;0;0;False;0;0;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-736.1592,360.5573;Inherit;False;Property;_EmissionColour;EmissionColour;5;0;Create;True;0;0;False;0;0.7169812,0.6738338,0.05749378,0;0.360404,0.4433595,0.8584906,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-1052.053,276.9981;Inherit;True;Property;_EmissionTexture;EmissionTexture;4;0;Create;True;0;0;False;0;-1;None;5798ded558355430c8a9b13ee12a847c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CameraDepthFade;1;-551.6042,739.9749;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-440.6453,285.1502;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-81.26318,285.15;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-454.274,-247.9657;Inherit;True;Property;_MainTexture;MainTexture;2;0;Create;True;0;0;False;0;-1;fd81e0290e232ea4592fb734173c77d5;fd81e0290e232ea4592fb734173c77d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-447.6949,-10.59611;Inherit;True;Property;_NormalMap;NormalMap;3;0;Create;True;0;0;False;0;-1;ebb6cd8e0a017164ab632b4ea0b90f6b;ebb6cd8e0a017164ab632b4ea0b90f6b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;148.5372,-32.02969;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;CameraDepthFade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;12;0;15;0
WireConnection;12;1;1;0
WireConnection;0;0;7;0
WireConnection;0;1;8;0
WireConnection;0;2;12;0
ASEEND*/
//CHKSM=279BC23E3E1E0E507BF7A74CDF068C6B1F86ED66