// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Leaf"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 10)) = 0
		_Emission("Emission", 2D) = "white" {}
		_EmissionIntensity("Emission Intensity", Float) = 0
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_DotThreshold("Dot Threshold", Range( -1 , 1)) = 0
		_Metallic("Metallic", 2D) = "white" {}
		_MetallicIntensity("Metallic Intensity", Range( 0 , 1)) = 0
		_Roughness("Roughness", 2D) = "white" {}
		_RoughnessIntensity("Roughness Intensity", Range( 0 , 1)) = 0
		_VertexDisplacement("Vertex Displacement", Vector) = (0,0,0,0)
		_PannerDirection("Panner Direction", Vector) = (0,0,0,0)
		_PannerTime("Panner Time", Float) = 0
		_NoiseScale("Noise Scale", Float) = 0
		_WindStrength("Wind Strength", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform float _PannerTime;
		uniform float2 _PannerDirection;
		uniform float _NoiseScale;
		uniform float _WindStrength;
		uniform float3 _VertexDisplacement;
		uniform float _NormalIntensity;
		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float _EmissionIntensity;
		uniform float4 _EmissionColour;
		uniform float _DotThreshold;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _MetallicIntensity;
		uniform sampler2D _Roughness;
		uniform float4 _Roughness_ST;
		uniform float _RoughnessIntensity;


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


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult15 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float2 panner18 = ( ( _PannerTime * _Time.y ) * _PannerDirection + appendResult15.xy);
			float simplePerlin2D20 = snoise( panner18*_NoiseScale );
			simplePerlin2D20 = simplePerlin2D20*0.5 + 0.5;
			float3 VertexDisplacement32 = ( ( ( simplePerlin2D20 - 0.5 ) * _WindStrength ) * _VertexDisplacement );
			v.vertex.xyz += VertexDisplacement32;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float3 break4 = UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) );
			float4 appendResult5 = (float4(break4.x , break4.y , 0.0 , 0.0));
			float4 appendResult8 = (float4(( _NormalIntensity * appendResult5 ).xy , break4.z , 0.0));
			float4 Normal9 = appendResult8;
			o.Normal = Normal9.xyz;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode37 = tex2D( _Albedo, uv_Albedo );
			float4 Albedo38 = tex2DNode37;
			o.Albedo = Albedo38.rgb;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float4 Emission77 = ( tex2D( _Emission, uv_Emission ) * _EmissionIntensity * _EmissionColour );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult81 = dot( ase_worldViewDir , ( ase_worldNormal * -1.0 ) );
			float clampResult86 = clamp( dotResult81 , -1.0 , 1.0 );
			float4 lerpResult85 = lerp( float4( 0,0,0,0 ) , Emission77 , (( clampResult86 > _DotThreshold ) ? clampResult86 :  0.0 ));
			o.Emission = lerpResult85.rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float Metallic57 = ( tex2D( _Metallic, uv_Metallic ).a * _MetallicIntensity );
			o.Metallic = Metallic57;
			float2 uv_Roughness = i.uv_texcoord * _Roughness_ST.xy + _Roughness_ST.zw;
			float Roughness59 = ( tex2D( _Roughness, uv_Roughness ).a * _RoughnessIntensity );
			o.Smoothness = Roughness59;
			float Alpha46 = tex2DNode37.a;
			o.Alpha = Alpha46;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;1164.264;-233.1792;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;11;-3062.725,2941.807;Inherit;False;2597.581;1171.641;Comment;15;32;67;66;23;22;21;20;19;18;16;15;17;13;14;12;Vertex Displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;12;-2986.34,3043.104;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;14;-2974.304,3585.405;Inherit;False;Property;_PannerTime;Panner Time;13;0;Create;True;0;0;False;0;0;0.48;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;13;-2982.025,3828.097;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1;-3034.917,1695.459;Inherit;False;2023.642;652.5309;Comment;8;9;8;7;6;5;4;3;2;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;2;-2992.758,1792.277;Inherit;True;Property;_NormalMap;Normal Map;1;0;Create;True;0;0;False;0;None;133a9c2501696a444acf02ee510c217a;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-2734.969,3503.225;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;17;-2995.038,3279.84;Inherit;False;Property;_PannerDirection;Panner Direction;12;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DynamicAppendNode;15;-2704.678,3066.726;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;18;-2421.871,3261.532;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;3;-2714.652,1910.985;Inherit;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-2338.058,3512.882;Inherit;False;Property;_NoiseScale;Noise Scale;14;0;Create;True;0;0;False;0;0;0.46;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;79;-637.4573,1912.086;Inherit;False;1266.483;634.2704;Comment;6;73;72;74;75;76;77;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-1425.123,1047.33;Inherit;False;Constant;_Float0;Float 0;16;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;82;-1460.837,739.4164;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NoiseGeneratorNode;20;-2049.003,3491.726;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;72;-587.4572,1962.086;Inherit;True;Property;_Emission;Emission;3;0;Create;True;0;0;False;0;None;c719623e01d9cdd48a40fad3e1c13fd1;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.CommentaryNode;90;-1208.662,135.1137;Inherit;False;1811.34;877.483;Comment;8;80;81;86;88;89;78;85;91;Emission View Dir;1,1,1,1;0;0
Node;AmplifyShaderEditor.BreakToComponentsNode;4;-2369.062,2079.682;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.CommentaryNode;62;-2840.24,-656.9366;Inherit;False;1227.155;526.6119;Comment;5;50;51;54;56;57;Metallic;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;61;-2832.33,148.3519;Inherit;False;1222.389;544.5693;Comment;5;53;52;55;58;59;Roughness;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-176.9876,2230.593;Inherit;False;Property;_EmissionIntensity;Emission Intensity;4;0;Create;True;0;0;False;0;0;1.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-1163.277,745.8627;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;52;-2782.329,198.3519;Inherit;True;Property;_Roughness;Roughness;9;0;Create;True;0;0;False;0;None;5e906c048507a8f49a852e303d3bee21;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;80;-1154.577,482.2296;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;6;-2363.224,1823.939;Inherit;False;Property;_NormalIntensity;Normal Intensity;2;0;Create;True;0;0;False;0;0;0.26;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;50;-2790.239,-606.9366;Inherit;True;Property;_Metallic;Metallic;7;0;Create;True;0;0;False;0;None;8d71e3cfd4eb5344994bdb2439a19a63;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;73;-250.8391,2006.353;Inherit;True;Property;_TextureSample4;Texture Sample 4;13;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;75;-181.0159,2339.356;Inherit;False;Property;_EmissionColour;Emission Colour;5;0;Create;True;0;0;False;0;0,0,0,0;0,0.7566807,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;22;-1692.005,3262.68;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;71;-1390.949,-1409.299;Inherit;False;1107.094;663.2904;Comment;4;36;37;46;38;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-2029.571,1948.204;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1666.125,3491.61;Inherit;False;Property;_WindStrength;Wind Strength;15;0;Create;True;0;0;False;0;0;0.59;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1363.538,3262.68;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;81;-816.6057,486.9637;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;53;-2484.08,209.2825;Inherit;True;Property;_TextureSample3;Texture Sample 3;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;173.4709,2039.922;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;66;-1376.521,3513.119;Inherit;False;Property;_VertexDisplacement;Vertex Displacement;11;0;Create;True;0;0;False;0;0,0,0;0,0.5,0.1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1771.211,1829.92;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;36;-1340.949,-1359.299;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;d1876ca3f2ca7744fbb51541c01c0e20;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;51;-2491.99,-596.006;Inherit;True;Property;_TextureSample2;Texture Sample 2;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;55;-2455.028,441.2572;Inherit;False;Property;_RoughnessIntensity;Roughness Intensity;10;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-2467.515,-385.4957;Inherit;False;Property;_MetallicIntensity;Metallic Intensity;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;86;-520.9992,486.9064;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;77;395.0256,2062.749;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-2113.052,305.2154;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;88;-534.5795,754.5968;Inherit;False;Property;_DotThreshold;Dot Threshold;6;0;Create;True;0;0;False;0;0;0.64;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;37;-961.9498,-1259.162;Inherit;True;Property;_TextureSample0;Texture Sample 0;9;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;8;-1507.375,2095.187;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-1058.293,3352.202;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-2096.303,-501.7482;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;46;-518.7657,-1004.009;Inherit;False;Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-742.2679,3346.118;Inherit;False;VertexDisplacement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCCompareGreater;89;-127.5681,488.2192;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;78;-357.3515,199.9801;Inherit;False;77;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;57;-1826.113,-507.588;Inherit;False;Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;59;-1843.941,292.0333;Inherit;False;Roughness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-517.8546,-1230.511;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;9;-1237.72,2093.359;Inherit;False;Normal;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;63;761.1564,337.455;Inherit;False;57;Metallic;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;47;757.4213,782.3214;Inherit;False;46;Alpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;761.3297,1028.65;Inherit;False;32;VertexDisplacement;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;10;727.5001,32.699;Inherit;False;9;Normal;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;85;336.6782,185.1137;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;39;756.4554,-127.3471;Inherit;False;38;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;753.1541,560.4602;Inherit;False;59;Roughness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1117.164,5.078032;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Leaf;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;14;0
WireConnection;16;1;13;0
WireConnection;15;0;12;1
WireConnection;15;1;12;3
WireConnection;18;0;15;0
WireConnection;18;2;17;0
WireConnection;18;1;16;0
WireConnection;3;0;2;0
WireConnection;20;0;18;0
WireConnection;20;1;19;0
WireConnection;4;0;3;0
WireConnection;91;0;82;0
WireConnection;91;1;92;0
WireConnection;73;0;72;0
WireConnection;22;0;20;0
WireConnection;5;0;4;0
WireConnection;5;1;4;1
WireConnection;23;0;22;0
WireConnection;23;1;21;0
WireConnection;81;0;80;0
WireConnection;81;1;91;0
WireConnection;53;0;52;0
WireConnection;76;0;73;0
WireConnection;76;1;74;0
WireConnection;76;2;75;0
WireConnection;7;0;6;0
WireConnection;7;1;5;0
WireConnection;51;0;50;0
WireConnection;86;0;81;0
WireConnection;77;0;76;0
WireConnection;58;0;53;4
WireConnection;58;1;55;0
WireConnection;37;0;36;0
WireConnection;8;0;7;0
WireConnection;8;2;4;2
WireConnection;67;0;23;0
WireConnection;67;1;66;0
WireConnection;56;0;51;4
WireConnection;56;1;54;0
WireConnection;46;0;37;4
WireConnection;32;0;67;0
WireConnection;89;0;86;0
WireConnection;89;1;88;0
WireConnection;89;2;86;0
WireConnection;57;0;56;0
WireConnection;59;0;58;0
WireConnection;38;0;37;0
WireConnection;9;0;8;0
WireConnection;85;1;78;0
WireConnection;85;2;89;0
WireConnection;0;0;39;0
WireConnection;0;1;10;0
WireConnection;0;2;85;0
WireConnection;0;3;63;0
WireConnection;0;4;64;0
WireConnection;0;9;47;0
WireConnection;0;11;33;0
ASEEND*/
//CHKSM=2B15CD810BA6D7B79607767C5BD3FB0DA6B7338A