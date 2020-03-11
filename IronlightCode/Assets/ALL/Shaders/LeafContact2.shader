// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LeafContact2"
{
	Properties
	{
		_ContactLocation("ContactLocation", Vector) = (0,0,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_Count("Count", Float) = 0
		_Thickness("Thickness", Float) = 0
		_Emission("Emission", 2D) = "white" {}
		_EmissionColour("EmissionColour", Color) = (0,0,0,0)
		_EmissionIntensity("EmissionIntensity", Float) = 0
		_WaveColour("Wave Colour", Color) = (0,0,0,0)
		_WaveIntensity("Wave Intensity", Float) = 1
		_BodyRadius("BodyRadius", Float) = 0
		_PlayerPosition("PlayerPosition", Vector) = (0,0,0,0)
		_BodyRadiusColour("BodyRadiusColour", Color) = (1,0,0,0)
		_BRIntensity("BRIntensity", Float) = 0
		_RimFade("RimFade", Float) = 1
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
			float3 worldPos;
		};

		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float3 _PlayerPosition;
		uniform float _BodyRadius;
		uniform float4 _BodyRadiusColour;
		uniform float _BRIntensity;
		uniform float _Count;
		uniform float3 _ContactLocation;
		uniform float _Thickness;
		uniform float4 _WaveColour;
		uniform float _WaveIntensity;
		uniform float _RimFade;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float4 _EmissionColour;
		uniform float _EmissionIntensity;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float temp_output_91_0 = abs( ( ase_worldPos.x - _PlayerPosition.x ) );
			float temp_output_90_0 = abs( ( ase_worldPos.z - _PlayerPosition.z ) );
			float RadiusFromPlayer96 = sqrt( ( ( temp_output_91_0 * temp_output_91_0 ) + ( temp_output_90_0 * temp_output_90_0 ) ) );
			float2 temp_cast_0 = (( _Time.y * 0.01 )).xx;
			float2 uv_TexCoord102 = i.uv_texcoord * float2( 0.1,1 );
			float2 panner103 = ( 1.0 * _Time.y * temp_cast_0 + uv_TexCoord102);
			float simplePerlin2D104 = snoise( panner103 );
			simplePerlin2D104 = simplePerlin2D104*0.5 + 0.5;
			float2 temp_cast_1 = (( _Time.y * 1.0 )).xx;
			float2 uv_TexCoord107 = i.uv_texcoord * float2( 10,-10 );
			float2 panner109 = ( 1.0 * _Time.y * temp_cast_1 + uv_TexCoord107);
			float simplePerlin2D110 = snoise( panner109 );
			simplePerlin2D110 = simplePerlin2D110*0.5 + 0.5;
			o.Albedo = ( ( tex2D( _Albedo, uv_Albedo ) * 0.25 ) + ( step( RadiusFromPlayer96 , _BodyRadius ) * _BodyRadiusColour * ( ( simplePerlin2D104 * simplePerlin2D110 ) * _BRIntensity ) ) ).rgb;
			float temp_output_9_0 = abs( ( ase_worldPos.x - _ContactLocation.x ) );
			float temp_output_10_0 = abs( ( ase_worldPos.z - _ContactLocation.z ) );
			float RadiusFromCollision34 = sqrt( ( ( temp_output_9_0 * temp_output_9_0 ) + ( temp_output_10_0 * temp_output_10_0 ) ) );
			float temp_output_58_0 = abs( ( _Count - RadiusFromCollision34 ) );
			float2 temp_cast_3 = (( _Time.y * 0.1 )).xx;
			float2 uv_TexCoord73 = i.uv_texcoord * float2( 10,10 );
			float2 panner72 = ( 1.0 * _Time.y * temp_cast_3 + uv_TexCoord73);
			float simplePerlin2D74 = snoise( panner72 );
			simplePerlin2D74 = simplePerlin2D74*0.5 + 0.5;
			float4 temp_cast_4 = (( _RimFade * temp_output_58_0 )).xxxx;
			float4 clampResult121 = clamp( ( ( step( temp_output_58_0 , _Thickness ) * _WaveColour * _WaveIntensity * simplePerlin2D74 ) - temp_cast_4 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			o.Emission = ( clampResult121 + ( tex2D( _Emission, uv_Emission ) * _EmissionColour * _EmissionIntensity ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;4780.321;2239.855;6.362919;True;False
Node;AmplifyShaderEditor.CommentaryNode;35;2119.321,-1352.905;Inherit;False;1414.579;401.3249;Hypo from center of object;11;34;6;13;12;11;9;10;8;7;1;2;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;2198.836,-1292.505;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;2;2195.253,-1121.793;Inherit;False;Property;_ContactLocation;ContactLocation;0;0;Create;True;0;0;False;0;0,0,0;-0.21,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;7;2468.775,-1218.472;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;8;2474.3,-1094.183;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;10;2644.161,-1092.802;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;9;2641.399,-1217.092;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;98;2118.531,-887.6323;Inherit;False;1385.064;396.9515;Player Position radius;11;88;89;90;91;92;93;94;95;96;97;86;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;2807.12,-1236.426;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;2809.882,-1098.326;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;86;2172.945,-674.6807;Inherit;False;Property;_PlayerPosition;PlayerPosition;10;0;Create;True;0;0;False;0;0,0,0;2.1,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;13;2985.268,-1165.995;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;97;2168.531,-837.6323;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;88;2443.994,-639.3105;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;6;3137.177,-1163.233;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;89;2438.47,-763.5994;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;91;2611.094,-762.2194;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;34;3288.9,-1167.52;Inherit;False;RadiusFromCollision;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;90;2613.856,-637.9294;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;100;-1360.063,-10.48923;Inherit;False;Constant;_Float2;Float 2;11;0;Create;True;0;0;False;0;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;99;-1361.864,-88.84836;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;105;-993.0278,253.9339;Inherit;False;Constant;_Float3;Float 3;11;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;106;-998.7751,163.7348;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-1686.665,2006.017;Inherit;False;Constant;_Float1;Float 1;11;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1409.136,1254.141;Inherit;False;Property;_Count;Count;2;0;Create;True;0;0;False;0;0;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;75;-1692.413,1915.818;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;55;-1497.59,1346.388;Inherit;False;34;RadiusFromCollision;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;2779.577,-643.4534;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;2776.815,-781.5533;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;94;2954.963,-711.1224;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;107;-996.5232,28.23659;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;10,-10;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;102;-1361.586,-224.3466;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;-1178.206,-99.25814;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;57;-1243.296,1281.989;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-1499.789,1905.408;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;73;-1690.161,1780.319;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;10,10;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;-815.1171,153.3249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;58;-1051.749,1281.988;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;109;-673.8862,28.41158;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;72;-1367.524,1780.494;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;103;-1003.429,-224.1716;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-1099.482,1377.475;Inherit;False;Property;_Thickness;Thickness;3;0;Create;True;0;0;False;0;0;0.77;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SqrtOpNode;95;3106.873,-708.3604;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;96;3258.596,-712.6474;Inherit;False;RadiusFromPlayer;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;104;-459.5406,-224.32;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;123;-1000.337,1015.005;Inherit;False;Property;_RimFade;RimFade;13;0;Create;True;0;0;False;0;1;1.132885;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;59;-891.576,1281.989;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;110;-455.5936,18.39661;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;69;-1121.418,1478.989;Inherit;False;Property;_WaveColour;Wave Colour;7;0;Create;True;0;0;False;0;0,0,0,0;1,0,0.989954,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;71;-1114.417,1659.69;Inherit;False;Property;_WaveIntensity;Wave Intensity;8;0;Create;True;0;0;False;0;1;2.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;74;-1115.685,1768.871;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;173.3286,-479.5601;Inherit;False;Property;_BodyRadius;BodyRadius;9;0;Create;True;0;0;False;0;0;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;78;124.9755,-389.3794;Inherit;False;96;RadiusFromPlayer;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-559.3946,1294.941;Inherit;True;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;-631.0546,1070.6;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;-27.24017,-85.71095;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;209.5563,50.44707;Inherit;False;Property;_BRIntensity;BRIntensity;12;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;506.8836,-512.7122;Inherit;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;False;0;0.25;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;65;1125.848,1264.294;Inherit;False;Property;_EmissionIntensity;EmissionIntensity;6;0;Create;True;0;0;False;0;0;1.86;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;81;300.1994,-276.951;Inherit;False;Property;_BodyRadiusColour;BodyRadiusColour;11;0;Create;True;0;0;False;0;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;64;1117.153,1068.38;Inherit;False;Property;_EmissionColour;EmissionColour;5;0;Create;True;0;0;False;0;0,0,0,0;0.7254902,0.6716574,0.2078431,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;363.7721,-718.7781;Inherit;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;-1;None;2741be98b31d56c43ad9cfbcaf99a799;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;63;1035.36,866.7482;Inherit;True;Property;_Emission;Emission;4;0;Create;True;0;0;False;0;-1;None;2f671ae13419dfa40ba030bc3c8949e3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;80;393.7994,-404.3511;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;120;-290.8131,959.9432;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;400.9673,-91.63098;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;121;-7.250784,717.3967;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;1456.934,868.0842;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;723.9992,-347.151;Inherit;True;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;761.5623,-643.681;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;1743.084,726.918;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;1093.141,-447.864;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2539.054,80.41936;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;LeafContact2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;1;1
WireConnection;7;1;2;1
WireConnection;8;0;1;3
WireConnection;8;1;2;3
WireConnection;10;0;8;0
WireConnection;9;0;7;0
WireConnection;11;0;9;0
WireConnection;11;1;9;0
WireConnection;12;0;10;0
WireConnection;12;1;10;0
WireConnection;13;0;11;0
WireConnection;13;1;12;0
WireConnection;88;0;97;3
WireConnection;88;1;86;3
WireConnection;6;0;13;0
WireConnection;89;0;97;1
WireConnection;89;1;86;1
WireConnection;91;0;89;0
WireConnection;34;0;6;0
WireConnection;90;0;88;0
WireConnection;92;0;90;0
WireConnection;92;1;90;0
WireConnection;93;0;91;0
WireConnection;93;1;91;0
WireConnection;94;0;93;0
WireConnection;94;1;92;0
WireConnection;101;0;99;0
WireConnection;101;1;100;0
WireConnection;57;0;56;0
WireConnection;57;1;55;0
WireConnection;76;0;75;0
WireConnection;76;1;77;0
WireConnection;108;0;106;0
WireConnection;108;1;105;0
WireConnection;58;0;57;0
WireConnection;109;0;107;0
WireConnection;109;2;108;0
WireConnection;72;0;73;0
WireConnection;72;2;76;0
WireConnection;103;0;102;0
WireConnection;103;2;101;0
WireConnection;95;0;94;0
WireConnection;96;0;95;0
WireConnection;104;0;103;0
WireConnection;59;0;58;0
WireConnection;59;1;60;0
WireConnection;110;0;109;0
WireConnection;74;0;72;0
WireConnection;70;0;59;0
WireConnection;70;1;69;0
WireConnection;70;2;71;0
WireConnection;70;3;74;0
WireConnection;122;0;123;0
WireConnection;122;1;58;0
WireConnection;111;0;104;0
WireConnection;111;1;110;0
WireConnection;80;0;78;0
WireConnection;80;1;79;0
WireConnection;120;0;70;0
WireConnection;120;1;122;0
WireConnection;113;0;111;0
WireConnection;113;1;112;0
WireConnection;121;0;120;0
WireConnection;66;0;63;0
WireConnection;66;1;64;0
WireConnection;66;2;65;0
WireConnection;82;0;80;0
WireConnection;82;1;81;0
WireConnection;82;2;113;0
WireConnection;25;0;17;0
WireConnection;25;1;67;0
WireConnection;68;0;121;0
WireConnection;68;1;66;0
WireConnection;84;0;25;0
WireConnection;84;1;82;0
WireConnection;0;0;84;0
WireConnection;0;2;68;0
ASEEND*/
//CHKSM=F99F50EBBBD0B39298A93A6A5FFA8CB05A44C03A