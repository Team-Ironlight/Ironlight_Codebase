// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Vertex"
{
	Properties
	{
		_XChange("XChange", Float) = 0
		_YChange("YChange", Float) = 0
		_ZChange("ZChange", Float) = 0
		_XSpeed("XSpeed", Float) = 0
		_YSpeed("YSpeed", Float) = 0
		_ZSpeed("ZSpeed", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform float _XChange;
		uniform float _XSpeed;
		uniform float _YChange;
		uniform float _YSpeed;
		uniform float _ZChange;
		uniform float _ZSpeed;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 appendResult17 = (float4(( _XChange * sin( ( _XSpeed * _Time.y ) ) ) , ( _YChange * sin( ( _YSpeed * _Time.y ) ) ) , ( _ZChange * sin( ( _ZSpeed * _Time.y ) ) ) , 0.0));
			v.vertex.xyz += appendResult17.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color1 = IsGammaSpace() ? float4(0,0.04381943,1,0) : float4(0,0.003402638,1,0);
			o.Albedo = color1.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
479;530;889;321;4681.708;148.6331;4.751042;True;False
Node;AmplifyShaderEditor.TimeNode;14;-1127.425,894.9431;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-1055.317,783.9731;Inherit;False;Property;_XSpeed;XSpeed;3;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1118.387,1535.079;Inherit;False;Property;_ZSpeed;ZSpeed;5;0;Create;True;0;0;False;0;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;18;-1187.152,1281.657;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-1115.044,1170.687;Inherit;False;Property;_YSpeed;YSpeed;4;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;24;-1190.495,1646.049;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-849.8098,845.8191;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-912.8798,1596.925;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-909.5367,1232.533;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-791.4471,720.0999;Inherit;False;Property;_XChange;XChange;0;0;Create;True;0;0;False;0;0;0.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;20;-746.9188,1210.512;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;26;-750.262,1574.904;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-854.5172,1471.205;Inherit;False;Property;_ZChange;ZChange;2;0;Create;True;0;0;False;0;0;0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;12;-687.192,823.7979;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-851.174,1106.814;Inherit;False;Property;_YChange;YChange;1;0;Create;True;0;0;False;0;0;0.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-576.4407,1508.998;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-513.371,757.8926;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-573.0975,1144.607;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;35;-2658.589,136.9528;Inherit;False;Property;_Tiling;Tiling;6;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ColorNode;1;-317.7717,-101.8752;Inherit;False;Constant;_Color0;Color 0;0;0;Create;True;0;0;False;0;0,0.04381943,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;17;173.7742,276.5884;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;34;-1775.19,136.7212;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1681.759,370.6378;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;32;-2114.406,122.3099;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;33;-2432.192,121.7146;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;37;-1375.474,199.1184;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-1115.291,203.4617;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;476.1512,-5.1165;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Vertex;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;10;0
WireConnection;15;1;14;2
WireConnection;25;0;28;0
WireConnection;25;1;24;2
WireConnection;19;0;22;0
WireConnection;19;1;18;2
WireConnection;20;0;19;0
WireConnection;26;0;25;0
WireConnection;12;0;15;0
WireConnection;27;0;29;0
WireConnection;27;1;26;0
WireConnection;9;0;7;0
WireConnection;9;1;12;0
WireConnection;21;0;23;0
WireConnection;21;1;20;0
WireConnection;17;0;9;0
WireConnection;17;1;21;0
WireConnection;17;2;27;0
WireConnection;34;0;32;0
WireConnection;32;0;33;0
WireConnection;33;0;35;0
WireConnection;37;0;34;0
WireConnection;37;1;38;0
WireConnection;36;0;37;0
WireConnection;0;0;1;0
WireConnection;0;11;17;0
ASEEND*/
//CHKSM=F23774744B992433B39A059F6999E2925398452D