// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Waterfall"
{
	Properties
	{
		_Texture1("Texture 1", 2D) = "white" {}
		_T1Intensity("T1 Intensity", Float) = 1
		_NormalTexture1("Normal Texture 1", 2D) = "white" {}
		_NormalIntensity1("Normal Intensity 1", Range( 0.01 , 10)) = 3
		_Texture2("Texture 2", 2D) = "white" {}
		_T2Intensity("T2 Intensity", Float) = 2
		_NormalTexture2("Normal Texture 2", 2D) = "white" {}
		_NormalIntensity2("Normal Intensity 2", Range( 0.01 , 10)) = 3
		_GradientPosition("Gradient Position", Range( 0 , 1)) = 0
		_GradientWidth("Gradient Width", Range( 0 , 1)) = 0
		_Emission("Emission", 2D) = "white" {}
		_EmissionIntensity("Emission Intensity", Range( 0 , 10)) = 0
		_EmissionColour("Emission Colour", Color) = (0,0,0,0)
		_WaveDirection("Wave Direction", Vector) = (0,1,0,0)
		_WaveTiling("Wave Tiling", Vector) = (0,0,0,0)
		_WaveTime("Wave Time", Float) = 1
		_WaveSpeed("Wave Speed", Vector) = (0,0,0,0)
		_WaveIntensity("Wave Intensity", Float) = 0.1
		_PannerSpeed("Panner Speed", Vector) = (-1,0,0,0)
		_PannerTime("Panner Time", Float) = 0
		_DepthAmount("DepthAmount", Range( 0 , 10)) = 0
		_RefractAmount("RefractAmount", Range( 0 , 5)) = 0.1
		_RefractColourIntensity("Refract Colour Intensity", Range( 0 , 10)) = 1
		_RefractColour("Refract Colour", Color) = (1,0,0,0)
		_EdgeTexture("Edge Texture", 2D) = "white" {}
		_EdgeSomething("Edge Something", Float) = 0
		_EdgeTiling("Edge Tiling", Vector) = (0,0,0,0)
		_EdgeDistance("Edge Distance", Float) = 1
		_EdgePower("Edge Power", Range( 0 , 2)) = 1
		_SeaFoamColour("Sea Foam Colour", Color) = (1,0,0,0)
		_Roughness("Roughness", 2D) = "white" {}
		_RoughnessIntensity("Roughness Intensity", Range( 0 , 10)) = 0
		_PannerTiling("Panner Tiling", Vector) = (0,0,0,0)
		_PannerOffset("Panner Offset", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 4.6
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float _WaveTime;
		uniform float2 _WaveSpeed;
		uniform float2 _WaveTiling;
		uniform float _WaveIntensity;
		uniform float3 _WaveDirection;
		uniform float _GradientPosition;
		uniform float _GradientWidth;
		uniform sampler2D _NormalTexture1;
		uniform float _PannerTime;
		uniform float2 _PannerSpeed;
		uniform float2 _PannerTiling;
		uniform float2 _PannerOffset;
		uniform float _NormalIntensity1;
		uniform sampler2D _NormalTexture2;
		uniform float _NormalIntensity2;
		uniform sampler2D _Texture1;
		uniform float _T1Intensity;
		uniform sampler2D _Texture2;
		uniform float _T2Intensity;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _RefractAmount;
		uniform float4 _RefractColour;
		uniform float _RefractColourIntensity;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _DepthAmount;
		uniform sampler2D _Emission;
		uniform float _EmissionIntensity;
		uniform float4 _EmissionColour;
		uniform float _EdgeDistance;
		uniform sampler2D _EdgeTexture;
		uniform float2 _EdgeTiling;
		uniform float _EdgeSomething;
		uniform float _EdgePower;
		uniform float4 _SeaFoamColour;
		uniform sampler2D _Roughness;
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


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult22 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldUVs29 = appendResult22;
			float4 WaveTileUV67 = ( WorldUVs29 * float4( _WaveTiling, 0.0 , 0.0 ) );
			float2 panner115 = ( ( _WaveTime * _Time.y ) * _WaveSpeed + WaveTileUV67.xy);
			float simplePerlin2D117 = snoise( panner115 );
			simplePerlin2D117 = simplePerlin2D117*0.5 + 0.5;
			float temp_output_55_0 = ( _GradientPosition + _GradientWidth );
			float temp_output_42_0 = ( _GradientPosition - _GradientWidth );
			float temp_output_52_0 = (( temp_output_42_0 < v.texcoord.xy.y ) ? v.texcoord.xy.y :  0.0 );
			float smoothstepResult87 = smoothstep( temp_output_42_0 , temp_output_55_0 , (( temp_output_55_0 > temp_output_52_0 ) ? temp_output_52_0 :  0.0 ));
			float clampResult123 = clamp( ( (( temp_output_55_0 < v.texcoord.xy.y ) ? 1.0 :  0.0 ) + smoothstepResult87 ) , 0.0 , 1.0 );
			float Gradient129 = clampResult123;
			float3 WaveHeight143 = ( simplePerlin2D117 * ( _WaveIntensity * _WaveDirection * Gradient129 ) );
			v.vertex.xyz += WaveHeight143;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord7 = i.uv_texcoord * _PannerTiling + _PannerOffset;
			float2 panner9 = ( ( _PannerTime * _Time.y ) * _PannerSpeed + uv_TexCoord7);
			float2 Panner10 = panner9;
			float3 break17 = UnpackNormal( tex2D( _NormalTexture1, Panner10 ) );
			float4 appendResult25 = (float4(( break17.x * _NormalIntensity1 ) , ( break17.y * _NormalIntensity1 ) , break17.z , 0.0));
			float4 NormalT134 = appendResult25;
			float3 break81 = UnpackNormal( tex2D( _NormalTexture2, Panner10 ) );
			float4 appendResult120 = (float4(( break81.x * _NormalIntensity2 ) , ( break81.y * _NormalIntensity2 ) , break81.z , 0.0));
			float4 NormalT2131 = appendResult120;
			float temp_output_55_0 = ( _GradientPosition + _GradientWidth );
			float temp_output_42_0 = ( _GradientPosition - _GradientWidth );
			float temp_output_52_0 = (( temp_output_42_0 < i.uv_texcoord.y ) ? i.uv_texcoord.y :  0.0 );
			float smoothstepResult87 = smoothstep( temp_output_42_0 , temp_output_55_0 , (( temp_output_55_0 > temp_output_52_0 ) ? temp_output_52_0 :  0.0 ));
			float clampResult123 = clamp( ( (( temp_output_55_0 < i.uv_texcoord.y ) ? 1.0 :  0.0 ) + smoothstepResult87 ) , 0.0 , 1.0 );
			float Gradient129 = clampResult123;
			float4 lerpResult152 = lerp( NormalT134 , NormalT2131 , Gradient129);
			o.Normal = lerpResult152.xyz;
			float4 lerpResult119 = lerp( ( tex2D( _Texture1, Panner10 ) * _T1Intensity ) , ( tex2D( _Texture2, Panner10 ) * _T2Intensity ) , Gradient129);
			float4 Albedo128 = lerpResult119;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor98 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float4( (ase_grabScreenPosNorm).xy, 0.0 , 0.0 ) + ( _RefractAmount * NormalT134 ) ).xy);
			float4 clampResult101 = clamp( screenColor98 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Refraction124 = clampResult101;
			float4 lerpResult144 = lerp( Refraction124 , _RefractColour , _RefractColourIntensity);
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth23 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth23 = abs( ( screenDepth23 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthAmount ) );
			float clampResult37 = clamp( ( 1.0 - distanceDepth23 ) , 0.0 , 1.0 );
			float Depth38 = clampResult37;
			float4 lerpResult157 = lerp( Albedo128 , lerpResult144 , Depth38);
			o.Albedo = lerpResult157.rgb;
			float4 Emission130 = ( tex2D( _Emission, Panner10 ) * _EmissionIntensity * _EmissionColour );
			float screenDepth63 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth63 = abs( ( screenDepth63 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeDistance ) );
			float3 ase_worldPos = i.worldPos;
			float4 appendResult22 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldUVs29 = appendResult22;
			float2 panner60 = ( 1.0 * _Time.y * _EdgeTiling + ( WorldUVs29 * _EdgeSomething ).xy);
			float4 tex2DNode74 = tex2D( _EdgeTexture, panner60 );
			float4 clampResult125 = clamp( ( ( ( 1.0 - distanceDepth63 ) + tex2DNode74 ) * _EdgePower * _SeaFoamColour ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Edge136 = clampResult125;
			float4 lerpResult158 = lerp( Emission130 , Edge136 , Depth38);
			o.Emission = lerpResult158.rgb;
			float4 Metallic139 = ( tex2D( _Roughness, Panner10 ) * _RoughnessIntensity );
			o.Metallic = Metallic139.r;
			float clampResult132 = clamp( exp( ( 1.0 - saturate( Depth38 ) ) ) , 0.0 , 1.0 );
			float Opacity142 = clampResult132;
			o.Alpha = Opacity142;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;5676.312;-3195.496;1.472941;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-129.5457,2848.357;Inherit;False;1336.439;1008.063;Comment;8;159;10;9;8;7;6;5;4;Panner;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;2;-453.9109,2879.966;Inherit;False;Property;_PannerTiling;Panner Tiling;33;0;Create;True;0;0;False;0;0,0;13.62,5.15;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;3;-450.9038,3179.154;Inherit;False;Property;_PannerOffset;Panner Offset;34;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;4;-27.09766,3280.959;Inherit;False;Property;_PannerTime;Panner Time;19;0;Create;True;0;0;False;0;0;0.78;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-21.13965,3532.092;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;313.9564,3512.533;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-51.44666,2966.776;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;8;312.9724,3220.51;Inherit;False;Property;_PannerSpeed;Panner Speed;18;0;Create;True;0;0;False;0;-1,0;0,-1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;9;661.3469,3201.978;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;10;962.077,3198.073;Inherit;False;Panner;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;11;-4858.83,-3217.814;Inherit;False;1805.244;566.2767;Comment;9;34;25;24;21;18;17;14;13;12;Normal Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;12;-4817.866,-2867.979;Inherit;False;10;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;13;-4808.83,-3163.905;Inherit;True;Property;_NormalTexture1;Normal Texture 1;2;0;Create;True;0;0;False;0;None;2a7b4c9b0ca26f84bb76c23d1a691037;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;14;-4509.489,-3164.263;Inherit;True;Property;_NormalMap;NormalMap;13;0;Create;True;0;0;False;0;-1;None;5b653e484c8e303439ef414b62f969f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;15;-2833.835,2121.987;Inherit;False;810.1257;282.6798;Make all tiles reliant on world space;3;29;22;19;Make all tiles reliant on world space;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;28;-8168.451,-2603.711;Inherit;False;2625.506;854.0272;Comment;12;129;123;107;87;78;64;55;52;43;42;36;32;Gradient Control;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;16;-461.0128,-2720.823;Inherit;False;1651.3;378.0659;Depth;5;38;37;26;23;20;Depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.BreakToComponentsNode;17;-4154.112,-3158.384;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;18;-4202.427,-2909.537;Inherit;False;Property;_NormalIntensity1;Normal Intensity 1;3;0;Create;True;0;0;False;0;3;1.73;0.01;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;19;-2801.434,2171.987;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;36;-8109.654,-2317.525;Inherit;False;Property;_GradientPosition;Gradient Position;8;0;Create;True;0;0;False;0;0;0.61;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-8118.451,-2553.711;Inherit;False;Property;_GradientWidth;Gradient Width;9;0;Create;True;0;0;False;0;0;0.19;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;22;-2524.912,2178.563;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-395.8558,-2642.717;Inherit;False;Property;_DepthAmount;DepthAmount;20;0;Create;True;0;0;False;0;0;2.65;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-3835.796,-3161.064;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-3834.372,-2925.169;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;42;-7735.094,-2317.138;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;-7717.928,-2092.488;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;27;-7877.46,-251.5824;Inherit;False;2517.084;1434.453;Edge with Texture;17;153;136;125;106;99;92;86;74;72;63;61;60;48;47;44;35;31;Edge with Texture;1,1,1,1;0;0
Node;AmplifyShaderEditor.DepthFade;23;0.1699829,-2637.948;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-2247.521,2175.073;Inherit;False;WorldUVs;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-7731.833,-2548.609;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;52;-7278.593,-2113.309;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-3558.689,-3161.064;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TFHCCompareGreater;64;-6934.357,-2181.583;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;33;-4248.159,1868.109;Inherit;False;802.1453;611.0583;Wave Tiling and UVs;4;67;62;41;39;Wave Tiling and UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;-7810.476,483.4246;Inherit;False;29;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-7779.222,729.6226;Inherit;False;Property;_EdgeSomething;Edge Something;25;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;30;-2395.701,-2674.819;Inherit;False;1674.853;797.2925;Refraction;9;124;101;98;73;57;56;49;45;40;Refraction;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;34;-3287.585,-3167.814;Inherit;False;NormalT1;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;26;354.7339,-2633.17;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-7821.769,-143.8074;Inherit;False;Property;_EdgeDistance;Edge Distance;27;0;Create;True;0;0;False;0;1;1.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;41;-4209.288,1949.902;Inherit;False;29;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;47;-7528.918,852.7285;Inherit;False;Property;_EdgeTiling;Edge Tiling;26;0;Create;True;0;0;False;0;0,0;0,-1.4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-7541.455,592.9937;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ClampOpNode;37;636.0276,-2625.542;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;39;-4203.408,2158.986;Inherit;False;Property;_WaveTiling;Wave Tiling;14;0;Create;True;0;0;False;0;0,0;0,-2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;45;-2239.729,-2154.999;Inherit;False;34;NormalT1;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-2312.724,-2403.924;Inherit;False;Property;_RefractAmount;RefractAmount;21;0;Create;True;0;0;False;0;0.1;1.85;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;46;-4839.273,-2530.16;Inherit;False;1805.244;566.2767;Comment;8;131;120;112;111;93;81;70;50;Normal Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCCompareLower;78;-6881.655,-2551.054;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;49;-2271.558,-2613.702;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;87;-6631.385,-2331.383;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;53;-2358.444,-1607.874;Inherit;False;2239.248;1062.65;Comment;12;155;128;119;104;103;95;91;83;82;76;68;65;Texture Lerp;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;50;-4789.273,-2476.251;Inherit;True;Property;_NormalTexture2;Normal Texture 2;6;0;Create;True;0;0;False;0;None;065352c36540df54ba2986bfe9a16578;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;966.9753,-2632.709;Inherit;False;Depth;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;56;-1996.965,-2604.561;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-3939.776,1955.927;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;59;2540.512,-2347.59;Inherit;False;1691.374;310.1648;Opa;6;142;132;122;116;96;75;Opacity;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;58;-5645.022,2920.667;Inherit;False;1810.499;1072.69;Comment;8;117;115;97;90;89;88;77;69;Waves;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;107;-6337.499,-2552.58;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-1982.976,-2373.504;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;60;-7254.538,593.8696;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;61;-7684.431,88.12256;Inherit;True;Property;_EdgeTexture;Edge Texture;24;0;Create;True;0;0;False;0;None;6283e1983c081f242b63e6287058e3c6;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.DepthFade;63;-7559.443,-154.6864;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;69;-5594.112,3696.806;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-1705.198,-2511.556;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;75;2598.512,-2297.59;Inherit;False;38;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;68;-2294.676,-1020.874;Inherit;True;Property;_Texture2;Texture 2;4;0;Create;True;0;0;False;0;None;6283e1983c081f242b63e6287058e3c6;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.CommentaryNode;71;-3055.992,2987.331;Inherit;False;1362.53;717.103;Comment;7;130;118;110;109;108;94;80;Emission Control;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;70;-4489.933,-2476.609;Inherit;True;Property;_TextureSample0;Texture Sample 0;13;0;Create;True;0;0;False;0;-1;None;5b653e484c8e303439ef414b62f969f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;76;-2298.193,-1537.108;Inherit;True;Property;_Texture1;Texture 1;0;0;Create;True;0;0;False;0;None;4e9f338126105ab4c882fc8f8a14df42;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;67;-3685.085,1951.212;Inherit;False;WaveTileUV;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-5591.604,3465.594;Inherit;False;Property;_WaveTime;Wave Time;15;0;Create;True;0;0;False;0;1;8.31;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;123;-6089.819,-2553.26;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;65;-1905.977,-1456.166;Inherit;False;10;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;74;-6945.524,90.85156;Inherit;True;Property;_TextureSample3;Texture Sample 3;20;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;72;-7226.746,-145.3124;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;86;-6468.938,373.2296;Inherit;False;Property;_SeaFoamColour;Sea Foam Colour;29;0;Create;True;0;0;False;0;1,0,0,0;0.759434,1,0.9809988,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;80;-3005.992,3039.847;Inherit;True;Property;_Emission;Emission;10;0;Create;True;0;0;False;0;None;cba10a92f655f5642b4589f5c5f64765;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;99;-6526.873,-152.8914;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;83;-1620.497,-1539.727;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;93;-4182.87,-2221.883;Inherit;False;Property;_NormalIntensity2;Normal Intensity 2;7;0;Create;True;0;0;False;0;3;3.3;0.01;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;91;-1647.064,-1020.149;Inherit;True;Property;_TextureSample1;Texture Sample 1;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;88;-5595.022,2970.667;Inherit;False;67;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;94;-2989.856,3255.839;Inherit;False;10;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;82;-1540.186,-800.4344;Inherit;False;Property;_T2Intensity;T2 Intensity;5;0;Create;True;0;0;False;0;2;2.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;97;-5034.492,3314.471;Inherit;False;1140.851;609.6873;Wave Height;6;156;143;133;126;114;113;Wave Height;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;96;2880.894,-2292.711;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;89;-5589.818,3189.634;Inherit;False;Property;_WaveSpeed;Wave Speed;16;0;Create;True;0;0;False;0;0,0;0,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;92;-6531.021,97.44653;Inherit;False;Property;_EdgePower;Edge Power;28;0;Create;True;0;0;False;0;1;0.62;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;84;-2605.355,-47.22241;Inherit;False;1300.397;648.495;Comment;6;139;135;127;121;102;100;Metallic;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-5384.404,3468.988;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;98;-1456.054,-2515.999;Inherit;False;Global;_GrabScreen0;Grab Screen 0;23;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;81;-4134.555,-2470.73;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;95;-1497.9,-1322.104;Inherit;False;Property;_T1Intensity;T1 Intensity;1;0;Create;True;0;0;False;0;1;0.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;129;-5796.49,-2555.673;Inherit;False;Gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;114;-5000.942,3611.646;Inherit;False;Property;_WaveDirection;Wave Direction;13;0;Create;True;0;0;False;0;0,1,0;0,0,2;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;110;-2666.778,3486.414;Inherit;False;Property;_EmissionColour;Emission Colour;12;0;Create;True;0;0;False;0;0,0,0,0;0,0.490566,0.331716,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;-3816.239,-2473.41;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;-1013.048,-884.5955;Inherit;False;129;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;100;-2555.355,2.777588;Inherit;False;10;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;101;-1258.964,-2512.146;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;116;3136.89,-2292.455;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-2706.111,3249.213;Inherit;False;Property;_EmissionIntensity;Emission Intensity;11;0;Create;True;0;0;False;0;0;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-1185.697,-1540.247;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-3814.816,-2237.514;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;-1325.436,-1034.2;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-6140.889,-150.8984;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;102;-2552.593,223.2726;Inherit;True;Property;_Roughness;Roughness;30;0;Create;True;0;0;False;0;None;71ff7d72bfa0325419e2f1de32220351;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;108;-2713.543,3040.727;Inherit;True;Property;_TextureSample2;Texture Sample 2;12;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;164;-5010.542,3962.898;Inherit;False;129;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-4996.607,3377.876;Inherit;False;Property;_WaveIntensity;Wave Intensity;17;0;Create;True;0;0;False;0;0.1;1.17;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;115;-5121.399,2999.271;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;117;-4713.448,2990.702;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;125;-5893.289,-152.4304;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-2243.593,343.2726;Inherit;True;Property;_RoughnessIntensity;Roughness Intensity;31;0;Create;True;0;0;False;0;0;0.59;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ExpOpNode;122;3405.456,-2291.277;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;124;-972.8558,-2515.103;Inherit;False;Refraction;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;121;-2258.593,122.2726;Inherit;True;Property;_TextureSample4;Texture Sample 4;30;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;120;-3539.133,-2473.41;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;-2241.554,3044.122;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;119;-613.9828,-1534.606;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-4742.067,3595.399;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;128;-319.6254,-1538.509;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;136;-5631.138,-157.2454;Inherit;False;Edge;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;134;1097.577,-1776.618;Inherit;False;124;Refraction;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;135;-1901.593,61.27258;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;137;1062.132,-1369.288;Inherit;False;Property;_RefractColourIntensity;Refract Colour Intensity;22;0;Create;True;0;0;False;0;1;1.19;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;132;3678.348,-2291.277;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;130;-1962.359,3037.332;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;131;-3268.03,-2480.16;Inherit;False;NormalT2;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;133;-4436.263,3478.023;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;138;1089.046,-1570.529;Inherit;False;Property;_RefractColour;Refract Colour;23;0;Create;True;0;0;False;0;1,0,0,0;0.2382965,0.625924,0.990566,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;145;1614.801,-750.0557;Inherit;False;129;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;140;1591.865,-1932.73;Inherit;False;128;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;147;1223.503,-273.2844;Inherit;False;130;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;149;1589.844,-1422.004;Inherit;False;38;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;146;1221.405,-482.3224;Inherit;False;136;Edge;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;144;1404.214,-1697.644;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;139;-1538.958,58.38965;Inherit;False;Metallic;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;143;-4144.706,3471.04;Inherit;False;WaveHeight;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;148;1213.583,-16.94739;Inherit;False;38;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;142;3997.886,-2295.425;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;150;1609.851,-955.7938;Inherit;False;131;NormalT2;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;141;1606.986,-1158.687;Inherit;False;34;NormalT1;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;153;-6507.841,683.8726;Inherit;False;SeaFoamUVs;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;154;2164.898,-155.3213;Inherit;False;142;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;161;2081.638,-385.791;Inherit;False;139;Metallic;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;151;2455.265,-114.8403;Inherit;False;143;WaveHeight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;157;1923.459,-1700.242;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;160;1583.465,-480.2014;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;163;1724.76,-2337.015;Inherit;True;Property;_OpacityMap;Opacity Map;32;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;162;1995.003,-2306.988;Inherit;True;Property;_TextureSample5;Texture Sample 5;33;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;158;1596.278,-168.3104;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;156;-4746.982,3380.001;Inherit;False;WaveHeightIntensity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;152;1965.343,-1152.322;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;159;426.3036,2903.277;Inherit;False;67;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2770.6,-1513.738;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Waterfall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;7;0;2;0
WireConnection;7;1;3;0
WireConnection;9;0;7;0
WireConnection;9;2;8;0
WireConnection;9;1;6;0
WireConnection;10;0;9;0
WireConnection;14;0;13;0
WireConnection;14;1;12;0
WireConnection;17;0;14;0
WireConnection;22;0;19;1
WireConnection;22;1;19;3
WireConnection;21;0;17;0
WireConnection;21;1;18;0
WireConnection;24;0;17;1
WireConnection;24;1;18;0
WireConnection;42;0;36;0
WireConnection;42;1;32;0
WireConnection;23;0;20;0
WireConnection;29;0;22;0
WireConnection;55;0;36;0
WireConnection;55;1;32;0
WireConnection;52;0;42;0
WireConnection;52;1;43;2
WireConnection;52;2;43;2
WireConnection;25;0;21;0
WireConnection;25;1;24;0
WireConnection;25;2;17;2
WireConnection;64;0;55;0
WireConnection;64;1;52;0
WireConnection;64;2;52;0
WireConnection;34;0;25;0
WireConnection;26;0;23;0
WireConnection;48;0;35;0
WireConnection;48;1;31;0
WireConnection;37;0;26;0
WireConnection;78;0;55;0
WireConnection;78;1;43;2
WireConnection;87;0;64;0
WireConnection;87;1;42;0
WireConnection;87;2;55;0
WireConnection;38;0;37;0
WireConnection;56;0;49;0
WireConnection;62;0;41;0
WireConnection;62;1;39;0
WireConnection;107;0;78;0
WireConnection;107;1;87;0
WireConnection;57;0;40;0
WireConnection;57;1;45;0
WireConnection;60;0;48;0
WireConnection;60;2;47;0
WireConnection;63;0;44;0
WireConnection;73;0;56;0
WireConnection;73;1;57;0
WireConnection;70;0;50;0
WireConnection;70;1;12;0
WireConnection;67;0;62;0
WireConnection;123;0;107;0
WireConnection;74;0;61;0
WireConnection;74;1;60;0
WireConnection;72;0;63;0
WireConnection;99;0;72;0
WireConnection;99;1;74;0
WireConnection;83;0;76;0
WireConnection;83;1;65;0
WireConnection;91;0;68;0
WireConnection;91;1;65;0
WireConnection;96;0;75;0
WireConnection;90;0;77;0
WireConnection;90;1;69;0
WireConnection;98;0;73;0
WireConnection;81;0;70;0
WireConnection;129;0;123;0
WireConnection;111;0;81;0
WireConnection;111;1;93;0
WireConnection;101;0;98;0
WireConnection;116;0;96;0
WireConnection;104;0;83;0
WireConnection;104;1;95;0
WireConnection;112;0;81;1
WireConnection;112;1;93;0
WireConnection;103;0;91;0
WireConnection;103;1;82;0
WireConnection;106;0;99;0
WireConnection;106;1;92;0
WireConnection;106;2;86;0
WireConnection;108;0;80;0
WireConnection;108;1;94;0
WireConnection;115;0;88;0
WireConnection;115;2;89;0
WireConnection;115;1;90;0
WireConnection;117;0;115;0
WireConnection;125;0;106;0
WireConnection;122;0;116;0
WireConnection;124;0;101;0
WireConnection;121;0;102;0
WireConnection;121;1;100;0
WireConnection;120;0;111;0
WireConnection;120;1;112;0
WireConnection;120;2;81;2
WireConnection;118;0;108;0
WireConnection;118;1;109;0
WireConnection;118;2;110;0
WireConnection;119;0;104;0
WireConnection;119;1;103;0
WireConnection;119;2;155;0
WireConnection;126;0;113;0
WireConnection;126;1;114;0
WireConnection;126;2;164;0
WireConnection;128;0;119;0
WireConnection;136;0;125;0
WireConnection;135;0;121;0
WireConnection;135;1;127;0
WireConnection;132;0;122;0
WireConnection;130;0;118;0
WireConnection;131;0;120;0
WireConnection;133;0;117;0
WireConnection;133;1;126;0
WireConnection;144;0;134;0
WireConnection;144;1;138;0
WireConnection;144;2;137;0
WireConnection;139;0;135;0
WireConnection;143;0;133;0
WireConnection;142;0;132;0
WireConnection;153;0;74;0
WireConnection;157;0;140;0
WireConnection;157;1;144;0
WireConnection;157;2;149;0
WireConnection;160;0;146;0
WireConnection;160;1;147;0
WireConnection;162;0;163;0
WireConnection;158;0;147;0
WireConnection;158;1;146;0
WireConnection;158;2;148;0
WireConnection;156;0;113;0
WireConnection;152;0;141;0
WireConnection;152;1;150;0
WireConnection;152;2;145;0
WireConnection;0;0;157;0
WireConnection;0;1;152;0
WireConnection;0;2;158;0
WireConnection;0;3;161;0
WireConnection;0;9;154;0
WireConnection;0;11;151;0
ASEEND*/
//CHKSM=27BF45B947CD701BB9AE64E55F497C3810A75805