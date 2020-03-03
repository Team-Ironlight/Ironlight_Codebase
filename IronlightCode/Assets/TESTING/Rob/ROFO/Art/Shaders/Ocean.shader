// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Ocean"
{
	Properties
	{
		_WaveTime("Wave Time", Float) = 0
		_WaveSpeed("Wave Speed", Vector) = (1,0,0,0)
		_WaveTiling("Wave Tiling", Vector) = (0,0,0,0)
		_Tesselation("Tesselation", Float) = 1
		_WaveHeight("Wave Height", Float) = 0.1
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		_SubWaveSpeed("SubWaveSpeed", Vector) = (0.1,0.2,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_EdgeDistance("Edge Distance", Float) = 1
		_ColourBright("ColourBright", Color) = (0,0.3995438,1,0)
		_ColourDark("ColourDark", Color) = (0.1339903,0,1,0)
		_EdgePower("Edge Power", Range( 0 , 1)) = 1
		_NormalMap("NormalMap", 2D) = "white" {}
		_NormalDirection("Normal Direction", Vector) = (0,0,0,0)
		_NormalDirectionAlt("Normal Direction Alt", Vector) = (0,0,0,0)
		_NormalSpeed("NormalSpeed", Float) = 0
		_NormalSpeedAlt("NormalSpeed Alt", Float) = 0
		_NormalIntensity("Normal Intensity", Range( 0 , 1)) = 0
		_NormalTile("Normal Tile", Vector) = (0,0,0,0)
		_SeaFoam("Sea Foam", 2D) = "white" {}
		_EdgeFoam("Edge Foam", Float) = 0
		_EdgeFoamTiling("Edge Foam Tiling", Vector) = (0,0,0,0)
		_RefractAmount("RefractAmount", Float) = 0.1
		_DepthAmount("DepthAmount", Float) = 0
		_RefractColour("Refract Colour", Color) = (1,0,0,0)
		_RefractColourIntensity("Refract Colour Intensity", Float) = 1
		_TessMin("Tess Min", Float) = 0
		_TessMax("Tess Max", Float) = 80
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
		};

		uniform float _WaveTime;
		uniform float2 _WaveSpeed;
		uniform float2 _WaveTiling;
		uniform float2 _SubWaveSpeed;
		uniform float _WaveHeight;
		uniform sampler2D _NormalMap;
		uniform float _NormalIntensity;
		uniform float _NormalSpeed;
		uniform float2 _NormalDirection;
		uniform float2 _NormalTile;
		uniform float _NormalSpeedAlt;
		uniform float2 _NormalDirectionAlt;
		uniform sampler2D _Albedo;
		uniform float4 _ColourDark;
		uniform sampler2D _SeaFoam;
		uniform float2 _EdgeFoamTiling;
		uniform float _EdgeFoam;
		uniform float4 _ColourBright;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _RefractAmount;
		uniform float4 _RefractColour;
		uniform float _RefractColourIntensity;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _DepthAmount;
		uniform float _EdgeDistance;
		uniform float _EdgePower;
		uniform float _Smoothness;
		uniform float _TessMin;
		uniform float _TessMax;
		uniform float _Tesselation;


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


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float WaveHeightIntensity147 = _WaveHeight;
			return UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, _TessMin,_TessMax,( _Tesselation * WaveHeightIntensity147 ));
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float temp_output_16_0 = ( _Time.y * _WaveTime );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult23 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldUVs24 = appendResult23;
			float4 WaveTileUV33 = ( WorldUVs24 * float4( _WaveTiling, 0.0 , 0.0 ) );
			float2 panner17 = ( temp_output_16_0 * _WaveSpeed + WaveTileUV33.xy);
			float simplePerlin2D18 = snoise( panner17 );
			float2 panner34 = ( temp_output_16_0 * _WaveSpeed + ( WaveTileUV33 * float4( _SubWaveSpeed, 0.0 , 0.0 ) ).xy);
			float simplePerlin2D35 = snoise( panner34 );
			float temp_output_37_0 = ( simplePerlin2D18 + simplePerlin2D35 );
			float3 WaveHeight47 = ( temp_output_37_0 * ( _WaveHeight * float3(0,1,0) ) );
			v.vertex.xyz += WaveHeight47;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float4 appendResult23 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldUVs24 = appendResult23;
			float4 temp_output_80_0 = ( WorldUVs24 * float4( _NormalTile, 0.0 , 0.0 ) );
			float2 panner82 = ( ( _Time.y * _NormalSpeed ) * _NormalDirection + temp_output_80_0.xy);
			float2 panner83 = ( ( _NormalSpeedAlt * _Time.y ) * _NormalDirectionAlt + ( temp_output_80_0 * float4( 3,3,0,0 ) ).xy);
			float3 Normals97 = BlendNormals( UnpackScaleNormal( tex2D( _NormalMap, panner82 ), _NormalIntensity ) , UnpackScaleNormal( tex2D( _NormalMap, panner83 ), _NormalIntensity ) );
			o.Normal = Normals97;
			float2 panner119 = ( 1.0 * _Time.y * float2( 0,0 ) + WorldUVs24.xy);
			float2 panner109 = ( 1.0 * _Time.y * _EdgeFoamTiling + ( WorldUVs24 * _EdgeFoam ).xy);
			float4 tex2DNode104 = tex2D( _SeaFoam, panner109 );
			float4 SeaFoamUVs110 = tex2DNode104;
			float temp_output_16_0 = ( _Time.y * _WaveTime );
			float4 WaveTileUV33 = ( WorldUVs24 * float4( _WaveTiling, 0.0 , 0.0 ) );
			float2 panner17 = ( temp_output_16_0 * _WaveSpeed + WaveTileUV33.xy);
			float simplePerlin2D18 = snoise( panner17 );
			float2 panner34 = ( temp_output_16_0 * _WaveSpeed + ( WaveTileUV33 * float4( _SubWaveSpeed, 0.0 , 0.0 ) ).xy);
			float simplePerlin2D35 = snoise( panner34 );
			float temp_output_37_0 = ( simplePerlin2D18 + simplePerlin2D35 );
			float clampResult64 = clamp( temp_output_37_0 , -1.0 , 1.0 );
			float WavePattern43 = clampResult64;
			float clampResult56 = clamp( WavePattern43 , 0.0 , 1.0 );
			float4 lerpResult54 = lerp( _ColourDark , ( SeaFoamUVs110 + _ColourBright ) , clampResult56);
			float4 Albedo58 = ( tex2D( _Albedo, panner119 ) * lerpResult54 );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor126 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float3( (ase_grabScreenPosNorm).xy ,  0.0 ) + ( _RefractAmount * Normals97 ) ).xy);
			float4 clampResult127 = clamp( screenColor126 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Refraction128 = clampResult127;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth131 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth131 = abs( ( screenDepth131 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthAmount ) );
			float clampResult133 = clamp( ( 1.0 - distanceDepth131 ) , 0.0 , 1.0 );
			float Depth134 = clampResult133;
			float4 lerpResult135 = lerp( Albedo58 , ( Refraction128 * ( _RefractColour * _RefractColourIntensity ) ) , Depth134);
			o.Albedo = lerpResult135.rgb;
			float screenDepth65 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth65 = abs( ( screenDepth65 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeDistance ) );
			float4 clampResult73 = clamp( ( ( ( 1.0 - distanceDepth65 ) + tex2DNode104 ) * _EdgePower ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Edge70 = clampResult73;
			o.Emission = Edge70.rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;4943.973;-1814.386;2.279856;True;False
Node;AmplifyShaderEditor.CommentaryNode;25;-6878.952,-1387.466;Inherit;False;810.1257;282.6798;Make all tiles reliant on world space;3;24;23;21;Make all tiles reliant on world space;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;21;-6846.552,-1337.466;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;23;-6570.029,-1330.89;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;24;-6292.638,-1334.38;Inherit;False;WorldUVs;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;40;-6913.195,-796.8228;Inherit;False;798.8284;370.615;Wave Tiling and UVs;4;20;33;29;26;Wave Tiling and UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;26;-6866.032,-738.2457;Inherit;False;24;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;20;-6856.837,-607.0987;Inherit;False;Property;_WaveTiling;Wave Tiling;2;0;Create;True;0;0;False;0;0,0;0.17,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;98;-4000.615,1762.513;Inherit;False;2878.979;1986.205;Normal Map;20;95;93;76;78;51;94;81;80;85;86;91;83;82;88;90;87;77;96;97;100;Normal Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;78;-3835.655,2417.177;Inherit;False;24;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-6596.521,-732.2206;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;100;-3830.528,2641.046;Inherit;False;Property;_NormalTile;Normal Tile;18;0;Create;True;0;0;False;0;0,0;0.3,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;42;-6546.851,123.6136;Inherit;False;2529.821;1198.398;Wave Pattern;16;43;64;37;18;35;34;17;16;15;36;41;12;38;39;13;48;Wave Pattern;1,0.9103774,0.9103774,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;93;-3524.126,3495.718;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;90;-3543.579,1812.513;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-6341.829,-736.9357;Inherit;False;WaveTileUV;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;88;-3536.446,3241.327;Inherit;False;Property;_NormalSpeedAlt;NormalSpeed Alt;16;0;Create;True;0;0;False;0;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;-3538.136,2048.421;Inherit;False;Property;_NormalSpeed;NormalSpeed;15;0;Create;True;0;0;False;0;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-3530.483,2555.859;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;39;-6408.112,589.2713;Inherit;False;Property;_SubWaveSpeed;SubWaveSpeed;6;0;Create;True;0;0;False;0;0.1,0.2;0.6,-0.6;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-3209.678,2052.05;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;94;-3247.908,3354.795;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;85;-3196.977,2291.588;Inherit;False;Property;_NormalDirection;Normal Direction;13;0;Create;True;0;0;False;0;0,0;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;13;-6407.902,1082.354;Inherit;False;Property;_WaveTime;Wave Time;0;0;Create;True;0;0;False;0;0;0.93;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;12;-6410.011,862.1345;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-3253.163,2790.934;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;3,3,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;38;-6157.032,394.9142;Inherit;False;33;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;87;-3252.667,3047.801;Inherit;False;Property;_NormalDirectionAlt;Normal Direction Alt;14;0;Create;True;0;0;False;0;0,0;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;83;-2917.906,2832.868;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-5848.653,834.6286;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;15;-6408.046,192.6042;Inherit;False;Property;_WaveSpeed;Wave Speed;1;0;Create;True;0;0;False;0;1,0;0.25,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;72;-4393.691,-2350.107;Inherit;False;2215.232;826.6559;Edge with Texture;16;70;73;66;65;105;107;113;106;109;103;110;104;69;68;108;67;Edge with Texture;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;82;-2915.702,2514.793;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;76;-3950.615,2924.58;Inherit;True;Property;_NormalMap;NormalMap;12;0;Create;True;0;0;False;0;None;5372cc374b8da3f4b8fe0ae66e7e9589;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;41;-5850.854,180.6552;Inherit;False;33;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-5853.955,579.7119;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;96;-2531.724,2690.177;Inherit;False;Property;_NormalIntensity;Normal Intensity;17;0;Create;True;0;0;False;0;0;0.73;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;77;-2145.468,2968.661;Inherit;True;Property;_TextureSample1;Texture Sample 1;13;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;34;-5409.342,453.0213;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;107;-4338.141,-1795.686;Inherit;False;Property;_EdgeFoam;Edge Foam;20;0;Create;True;0;0;False;0;0;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;105;-4341.473,-1882.559;Inherit;False;24;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;51;-2126.898,2515.031;Inherit;True;Property;_TextureSample0;Texture Sample 0;7;0;Create;True;0;0;False;0;-1;None;c0909b386e802384db15b1d8d32e1a1d;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;17;-5408.281,191.7223;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BlendNormalsNode;95;-1728.705,2679.396;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;18;-5130.967,190.5837;Inherit;False;Simplex2D;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;35;-5125.063,453.2399;Inherit;False;Simplex2D;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-4123.244,-1869.945;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;113;-4146.047,-1745.345;Inherit;False;Property;_EdgeFoamTiling;Edge Foam Tiling;21;0;Create;True;0;0;False;0;0,0;0.01,-0.01;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;129;-693.1765,-2898.154;Inherit;False;1468.417;562.822;Refraction;9;120;121;125;122;124;123;126;127;128;Refraction;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;103;-3993.713,-2111.341;Inherit;True;Property;_SeaFoam;Sea Foam;19;0;Create;True;0;0;False;0;None;e4ec5988a6680194f9178cb38498107b;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;109;-3894.537,-1869.069;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-1355.636,2674.703;Inherit;False;Normals;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-4818.014,189.7996;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;124;-612.7591,-2450.332;Inherit;False;97;Normals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;64;-4508.461,199.3319;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;104;-3666.757,-2114.06;Inherit;True;Property;_TextureSample2;Texture Sample 2;20;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;122;-609.1814,-2611.636;Inherit;False;Property;_RefractAmount;RefractAmount;22;0;Create;True;0;0;False;0;0.1;1.48;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;120;-643.1765,-2848.154;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;43;-4251.45,185.4436;Inherit;False;WavePattern;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;110;-3260.172,-1930.451;Inherit;False;SeaFoamUVs;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;121;-342.644,-2837.159;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-365.7115,-2578.309;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;138;-697.5601,-3660.274;Inherit;False;1651.3;378.0659;Depth;5;134;133;137;131;132;Depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-4280.333,-2238.391;Inherit;False;Property;_EdgeDistance;Edge Distance;8;0;Create;True;0;0;False;0;1;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;61;-1491.566,-1863.192;Inherit;False;1512.506;951.012;Albedo;12;52;53;56;55;58;63;62;54;115;111;118;119;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;52;-1436.211,-1263.473;Inherit;False;Property;_ColourBright;ColourBright;9;0;Create;True;0;0;False;0;0,0.3995438,1,0;0,0.9797904,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;125;-39.75916,-2729.332;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;118;-1359.956,-1781.912;Inherit;False;24;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;111;-1436.033,-1478.414;Inherit;False;110;SeaFoamUVs;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;132;-647.5601,-3588.433;Inherit;False;Property;_DepthAmount;DepthAmount;23;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;55;-1423.763,-1054.006;Inherit;False;43;WavePattern;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;65;-4054.783,-2256.081;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;48;-5222.136,630.3149;Inherit;False;1140.851;609.6873;Wave Height;6;31;47;44;32;46;147;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;119;-1117.451,-1778.955;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;115;-1173.519,-1423.053;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;53;-1170.687,-1629.893;Inherit;False;Property;_ColourDark;ColourDark;10;0;Create;True;0;0;False;0;0.1339903,0,1,0;0.003921568,0.561137,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;131;-447.3164,-3606.638;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;126;148.2408,-2739.332;Inherit;False;Global;_GrabScreen0;Grab Screen 0;23;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;56;-1164.991,-1081.601;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;67;-3752.052,-2252.156;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;31;-5193.586,928.4883;Inherit;False;Constant;_WaveDirection;Wave Direction;4;0;Create;True;0;0;False;0;0,1,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;54;-857.2446,-1365.357;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-3216.159,-2114.243;Inherit;False;Property;_EdgePower;Edge Power;11;0;Create;True;0;0;False;0;1;0.55;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;127;366.2409,-2737.332;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;62;-846.3232,-1608.388;Inherit;True;Property;_Albedo;Albedo;7;0;Create;True;0;0;False;0;-1;None;f212c8380b57cee41b123a2b8ae73650;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;137;-134.5225,-3606.037;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;108;-3297.213,-2254.214;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-5184.251,693.7188;Inherit;False;Property;_WaveHeight;Wave Height;4;0;Create;True;0;0;False;0;0.1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;147;-4945.15,691.3334;Inherit;False;WaveHeightIntensity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;133;142.5944,-3606.762;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;128;553.241,-2740.332;Inherit;False;Refraction;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2877.863,-2253.166;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-518.2086,-1447.351;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;140;369.1394,-61.46077;Inherit;False;Property;_RefractColour;Refract Colour;24;0;Create;True;0;0;False;0;1,0,0,0;1,0.7807935,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;142;325.8292,130.8369;Inherit;False;Property;_RefractColourIntensity;Refract Colour Intensity;25;0;Create;True;0;0;False;0;1;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-4920.68,819.3135;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;150;254.0887,1057.515;Inherit;False;965.213;391.4766;Tesselation;6;30;149;145;148;146;144;Tesselation;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-4623.907,793.8655;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;149;304.0887,1196.021;Inherit;False;147;WaveHeightIntensity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;374.4264,1107.515;Inherit;False;Property;_Tesselation;Tesselation;3;0;Create;True;0;0;False;0;1;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;73;-2671.314,-2257.69;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;130;438.9522,-234.8863;Inherit;False;128;Refraction;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;134;509.0467,-3609.753;Inherit;False;Depth;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;143;606.4798,-70.12282;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;58;-259.1392,-1450.114;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;70;-2457.661,-2247.626;Inherit;False;Edge;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;47;-4332.35,788.3865;Inherit;False;WaveHeight;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;136;886.2137,81.12972;Inherit;False;134;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;148;604.0887,1131.021;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;726.3018,1333.991;Inherit;False;Property;_TessMax;Tess Max;27;0;Create;True;0;0;False;0;80;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;145;725.3018,1255.991;Inherit;False;Property;_TessMin;Tess Min;26;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;59;947.134,-295.367;Inherit;True;58;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;810.9046,-122.0951;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;152;-167.5255,375.9626;Inherit;False;Property;_FlowSpeed;FlowSpeed;28;0;Create;True;0;0;False;0;1;0.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;151;61.6338,505.0445;Inherit;False;flowmap;-1;;1;90d0b43e427f6b54098bb3c952fc9dc8;0;2;27;FLOAT;1;False;24;FLOAT4;0,0,0,0;False;3;FLOAT;26;FLOAT2;25;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;71;966.0836,461.3462;Inherit;False;70;Edge;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.DistanceBasedTessNode;144;957.3016,1151.991;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;99;964.6935,373.5765;Inherit;False;97;Normals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;50;1218.146,544.0024;Inherit;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;False;0;0.5;0.62;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;135;1253.638,-73.60425;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;49;1138.517,748.4517;Inherit;False;47;WaveHeight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;153;580.8702,580.4208;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1548.707,431.2353;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Ocean;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;21;1
WireConnection;23;1;21;3
WireConnection;24;0;23;0
WireConnection;29;0;26;0
WireConnection;29;1;20;0
WireConnection;33;0;29;0
WireConnection;80;0;78;0
WireConnection;80;1;100;0
WireConnection;91;0;90;0
WireConnection;91;1;86;0
WireConnection;94;0;88;0
WireConnection;94;1;93;0
WireConnection;81;0;80;0
WireConnection;83;0;81;0
WireConnection;83;2;87;0
WireConnection;83;1;94;0
WireConnection;16;0;12;0
WireConnection;16;1;13;0
WireConnection;82;0;80;0
WireConnection;82;2;85;0
WireConnection;82;1;91;0
WireConnection;36;0;38;0
WireConnection;36;1;39;0
WireConnection;77;0;76;0
WireConnection;77;1;83;0
WireConnection;77;5;96;0
WireConnection;34;0;36;0
WireConnection;34;2;15;0
WireConnection;34;1;16;0
WireConnection;51;0;76;0
WireConnection;51;1;82;0
WireConnection;51;5;96;0
WireConnection;17;0;41;0
WireConnection;17;2;15;0
WireConnection;17;1;16;0
WireConnection;95;0;51;0
WireConnection;95;1;77;0
WireConnection;18;0;17;0
WireConnection;35;0;34;0
WireConnection;106;0;105;0
WireConnection;106;1;107;0
WireConnection;109;0;106;0
WireConnection;109;2;113;0
WireConnection;97;0;95;0
WireConnection;37;0;18;0
WireConnection;37;1;35;0
WireConnection;64;0;37;0
WireConnection;104;0;103;0
WireConnection;104;1;109;0
WireConnection;43;0;64;0
WireConnection;110;0;104;0
WireConnection;121;0;120;0
WireConnection;123;0;122;0
WireConnection;123;1;124;0
WireConnection;125;0;121;0
WireConnection;125;1;123;0
WireConnection;65;0;66;0
WireConnection;119;0;118;0
WireConnection;115;0;111;0
WireConnection;115;1;52;0
WireConnection;131;0;132;0
WireConnection;126;0;125;0
WireConnection;56;0;55;0
WireConnection;67;0;65;0
WireConnection;54;0;53;0
WireConnection;54;1;115;0
WireConnection;54;2;56;0
WireConnection;127;0;126;0
WireConnection;62;1;119;0
WireConnection;137;0;131;0
WireConnection;108;0;67;0
WireConnection;108;1;104;0
WireConnection;147;0;44;0
WireConnection;133;0;137;0
WireConnection;128;0;127;0
WireConnection;68;0;108;0
WireConnection;68;1;69;0
WireConnection;63;0;62;0
WireConnection;63;1;54;0
WireConnection;32;0;44;0
WireConnection;32;1;31;0
WireConnection;46;0;37;0
WireConnection;46;1;32;0
WireConnection;73;0;68;0
WireConnection;134;0;133;0
WireConnection;143;0;140;0
WireConnection;143;1;142;0
WireConnection;58;0;63;0
WireConnection;70;0;73;0
WireConnection;47;0;46;0
WireConnection;148;0;30;0
WireConnection;148;1;149;0
WireConnection;139;0;130;0
WireConnection;139;1;143;0
WireConnection;151;27;152;0
WireConnection;151;24;97;0
WireConnection;144;0;148;0
WireConnection;144;1;145;0
WireConnection;144;2;146;0
WireConnection;135;0;59;0
WireConnection;135;1;139;0
WireConnection;135;2;136;0
WireConnection;153;0;151;25
WireConnection;153;1;151;0
WireConnection;153;2;151;26
WireConnection;0;0;135;0
WireConnection;0;1;99;0
WireConnection;0;2;71;0
WireConnection;0;4;50;0
WireConnection;0;11;49;0
WireConnection;0;14;144;0
ASEEND*/
//CHKSM=9DCA514D205376EA07AA6366F71851B1E44E8F11