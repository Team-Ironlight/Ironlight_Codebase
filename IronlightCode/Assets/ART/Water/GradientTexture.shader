// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GradientTexture"
{
	Properties
	{
		_Stylzed_Water_basecolor("Stylzed_Water_basecolor", 2D) = "white" {}
		_Rough_Water_basecolor("Rough_Water_basecolor", 2D) = "white" {}
		_StartGradient("StartGradient", Range( 0.01 , 0.99)) = 0.5406023
		_Strength("Strength", Range( 0 , 10)) = 0.6776728
		_Rough_Water_normal("Rough_Water_normal", 2D) = "white" {}
		_Stylzed_Water_normal("Stylzed_Water_normal", 2D) = "white" {}
		_NormalStrength("NormalStrength", Range( 0.01 , 2)) = 1.195827
		_Stylzed_Water_Emission("Stylzed_Water_Emission", 2D) = "white" {}
		_EmissionStrength("EmissionStrength", Range( 1 , 2)) = 1
		_Stylzed_Water_AO("Stylzed_Water_AO", 2D) = "white" {}
		_Rough_Water_AO("Rough_Water_AO", 2D) = "white" {}
		_PannerX("PannerX", Float) = 0
		_PannerY("PannerY", Float) = 0
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Stylzed_Water_normal;
		uniform float _PannerX;
		uniform float _PannerY;
		uniform sampler2D _Rough_Water_normal;
		uniform float _StartGradient;
		uniform float _Strength;
		uniform float _NormalStrength;
		uniform sampler2D _Stylzed_Water_basecolor;
		uniform sampler2D _Rough_Water_basecolor;
		uniform sampler2D _Stylzed_Water_Emission;
		uniform float _EmissionStrength;
		uniform sampler2D _Stylzed_Water_AO;
		uniform sampler2D _Rough_Water_AO;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 appendResult44 = (float4(_PannerX , _PannerY , 0.0 , 0.0));
			float2 panner39 = ( 1.0 * _Time.y * appendResult44.xy + i.uv_texcoord);
			float temp_output_15_0 = min( ( max( ( i.uv_texcoord.x - _StartGradient ) , 0.0 ) * _Strength ) , 2.0 );
			float4 lerpResult19 = lerp( tex2D( _Stylzed_Water_normal, panner39 ) , tex2D( _Rough_Water_normal, panner39 ) , temp_output_15_0);
			o.Normal = ( lerpResult19 * _NormalStrength ).rgb;
			float4 lerpResult4 = lerp( tex2D( _Stylzed_Water_basecolor, panner39 ) , tex2D( _Rough_Water_basecolor, panner39 ) , temp_output_15_0);
			o.Albedo = lerpResult4.rgb;
			o.Emission = ( tex2D( _Stylzed_Water_Emission, panner39 ) * temp_output_15_0 * _EmissionStrength ).rgb;
			float4 lerpResult31 = lerp( tex2D( _Stylzed_Water_AO, panner39 ) , tex2D( _Rough_Water_AO, panner39 ) , temp_output_15_0);
			o.Occlusion = lerpResult31.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
478;561;889;270;2090.036;967.0193;2.549157;True;False
Node;AmplifyShaderEditor.RangedFloatNode;3;-1832.289,375.5901;Inherit;False;Property;_StartGradient;StartGradient;2;0;Create;True;0;0;False;0;0.5406023;0.12;0.01;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1843.152,89.10736;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;42;-1196.012,-767.5149;Inherit;False;Property;_PannerX;PannerX;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1190.522,-538.1583;Inherit;False;Property;_PannerY;PannerY;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-1459.019,112.8936;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1431.115,-141.9367;Inherit;True;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1831.341,613.8128;Inherit;False;Property;_Strength;Strength;3;0;Create;True;0;0;False;0;0.6776728;2.15;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;11;-1142.863,112.0993;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;38;-939.5594,-943.698;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;44;-939.4809,-661.6124;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-803.5935,371.7114;Inherit;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;39;-647.1443,-943.6979;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-841.9172,109.3081;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-656.1294,-596.429;Inherit;True;Property;_Stylzed_Water_normal;Stylzed_Water_normal;5;0;Create;True;0;0;False;0;-1;c0909b386e802384db15b1d8d32e1a1d;c0909b386e802384db15b1d8d32e1a1d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMinOpNode;15;-560.9,109.6987;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-651.8562,-399.6793;Inherit;True;Property;_Rough_Water_normal;Rough_Water_normal;4;0;Create;True;0;0;False;0;-1;5372cc374b8da3f4b8fe0ae66e7e9589;5372cc374b8da3f4b8fe0ae66e7e9589;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;425.7221,-304.3435;Inherit;True;Property;_Stylzed_Water_Emission;Stylzed_Water_Emission;7;0;Create;True;0;0;False;0;-1;a9466649578b0524e8542809021a0899;a9466649578b0524e8542809021a0899;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;19;-243.0542,-519.527;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-251.6772,-760.6452;Inherit;True;Property;_Rough_Water_basecolor;Rough_Water_basecolor;1;0;Create;True;0;0;False;0;-1;8b70714da22ac3444849b8febcc9a04f;8b70714da22ac3444849b8febcc9a04f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;427.4322,352.2974;Inherit;True;Property;_Rough_Water_AO;Rough_Water_AO;10;0;Create;True;0;0;False;0;-1;d409fe4f7de780a43b2085b6bb831cbe;d409fe4f7de780a43b2085b6bb831cbe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;421.4987,141.4452;Inherit;True;Property;_Stylzed_Water_AO;Stylzed_Water_AO;9;0;Create;True;0;0;False;0;-1;eecdb08045c60d74db7dfc57367da920;eecdb08045c60d74db7dfc57367da920;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-251.8236,-949.6959;Inherit;True;Property;_Stylzed_Water_basecolor;Stylzed_Water_basecolor;0;0;Create;True;0;0;False;0;-1;f212c8380b57cee41b123a2b8ae73650;f212c8380b57cee41b123a2b8ae73650;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;424.034,-93.73444;Inherit;False;Property;_EmissionStrength;EmissionStrength;8;0;Create;True;0;0;False;0;1;2;1;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-239.5933,-272.5664;Inherit;False;Property;_NormalStrength;NormalStrength;6;0;Create;True;0;0;False;0;1.195827;1;0.01;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;4;116.6817,-835.793;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;31;754.4883,244.6209;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;122.2234,-478.439;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;834.8872,-298.4989;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1214.223,-423.9672;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;GradientTexture;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;7;1
WireConnection;12;1;3;0
WireConnection;11;0;12;0
WireConnection;11;1;13;0
WireConnection;44;0;42;0
WireConnection;44;1;43;0
WireConnection;39;0;38;0
WireConnection;39;2;44;0
WireConnection;9;0;11;0
WireConnection;9;1;5;0
WireConnection;18;1;39;0
WireConnection;15;0;9;0
WireConnection;15;1;16;0
WireConnection;17;1;39;0
WireConnection;26;1;39;0
WireConnection;19;0;18;0
WireConnection;19;1;17;0
WireConnection;19;2;15;0
WireConnection;2;1;39;0
WireConnection;30;1;39;0
WireConnection;29;1;39;0
WireConnection;1;1;39;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;15;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;31;2;15;0
WireConnection;22;0;19;0
WireConnection;22;1;24;0
WireConnection;27;0;26;0
WireConnection;27;1;15;0
WireConnection;27;2;28;0
WireConnection;0;0;4;0
WireConnection;0;1;22;0
WireConnection;0;2;27;0
WireConnection;0;5;31;0
ASEEND*/
//CHKSM=7EDBE3DA3BEF210D32F9035D67D7BDEBDBE8D831