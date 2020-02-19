// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TinyShader"
{
	Properties
	{
		_Albedo("Albedo", Color) = (0,0,0,0)
		_RimIntensity("RimIntensity", Range( 0 , 1)) = 0
		_Threshold("Threshold", Range( 0.01 , 0.5)) = 0
		_TopColour("TopColour", Color) = (0,0,0,0)
		_BottomColour("BottomColour", Color) = (0,0,0,0)
		_GradientBottom("GradientBottom", Range( 0 , 0.9)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			float3 viewDir;
		};

		uniform float4 _TopColour;
		uniform float4 _BottomColour;
		uniform float _GradientBottom;
		uniform float4 _Albedo;
		uniform float _RimIntensity;
		uniform float _Threshold;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult16 = lerp( _TopColour , _BottomColour , min( ( max( ( ( 1.0 - i.uv_texcoord.y ) - _GradientBottom ) , 0.0 ) * ( 1.0 / _GradientBottom ) ) , 1.0 ));
			o.Albedo = lerpResult16.rgb;
			float3 ase_worldNormal = i.worldNormal;
			float dotResult3 = dot( ase_worldNormal , i.viewDir );
			float smoothstepResult10 = smoothstep( ( _RimIntensity - _Threshold ) , ( _RimIntensity + _Threshold ) , ( 1.0 - dotResult3 ));
			o.Emission = ( _Albedo * smoothstepResult10 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Off"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
479;116.5;889;735;3950.735;1098.532;7.930368;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1144.67,1414.087;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;26;-1142.796,1687.11;Inherit;False;Constant;_Float2;Float 2;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1145.712,1179.246;Inherit;False;Property;_GradientBottom;GradientBottom;5;0;Create;True;0;0;False;0;1;0.39;0;0.9;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;-871.9355,1701.584;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-481.2979,1309.098;Inherit;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-518.2549,1056.95;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;4;-1271.898,-205.1635;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;1;-1273.091,31.71322;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;25;-155.2723,1252.542;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;22;-189.8383,1009.193;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1053.921,-459.9357;Inherit;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1268.735,488.4366;Inherit;False;Property;_Threshold;Threshold;2;0;Create;True;0;0;False;0;0;0.05;0.01;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;3;-903.9921,17.96378;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1270.556,251.1002;Inherit;False;Property;_RimIntensity;RimIntensity;1;0;Create;True;0;0;False;0;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-896.3748,267.2849;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;159.5515,1152.456;Inherit;False;Constant;_Float3;Float 3;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;120.3062,930.623;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-890.7531,485.2073;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;5;-657.9205,-3.066718;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;31;455.1143,900.5438;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-1148.44,819.2621;Inherit;False;Property;_TopColour;TopColour;3;0;Create;True;0;0;False;0;0,0,0,0;1,0.8740748,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;15;-1142.729,1002.007;Inherit;False;Property;_BottomColour;BottomColour;4;0;Create;True;0;0;False;0;0,0,0,0;1,0.5415316,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;10;-405.8648,-5.068062;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-774.6165,-507.7596;Inherit;False;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-44.15673,-18.99287;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;16;878.1943,766.8351;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1727.441,-32.31193;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TinyShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;Off;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;28;0;26;0
WireConnection;28;1;17;2
WireConnection;19;0;28;0
WireConnection;19;1;18;0
WireConnection;25;0;26;0
WireConnection;25;1;18;0
WireConnection;22;0;19;0
WireConnection;22;1;23;0
WireConnection;3;0;4;0
WireConnection;3;1;1;0
WireConnection;12;0;9;0
WireConnection;12;1;11;0
WireConnection;27;0;22;0
WireConnection;27;1;25;0
WireConnection;13;0;9;0
WireConnection;13;1;11;0
WireConnection;5;0;6;0
WireConnection;5;1;3;0
WireConnection;31;0;27;0
WireConnection;31;1;30;0
WireConnection;10;0;5;0
WireConnection;10;1;12;0
WireConnection;10;2;13;0
WireConnection;8;0;7;0
WireConnection;8;1;10;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;16;2;31;0
WireConnection;0;0;16;0
WireConnection;0;2;8;0
ASEEND*/
//CHKSM=8F86077B755999D87CA548AB7C4F27CDF3B35C6F