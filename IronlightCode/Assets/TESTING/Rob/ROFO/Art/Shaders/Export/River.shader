// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "River"
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
		_SurfaceContact("Surface Contact", Float) = 0
		_SurfaceAdd("Surface Add", Range( 0 , 1)) = 0
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
		uniform sampler2D _NormalTexture1;
		uniform float _PannerTime;
		uniform float2 _PannerSpeed;
		uniform float2 _PannerTiling;
		uniform float2 _PannerOffset;
		uniform float _NormalIntensity1;
		uniform sampler2D _NormalTexture2;
		uniform float _NormalIntensity2;
		uniform float _GradientPosition;
		uniform float _GradientWidth;
		uniform sampler2D _Texture1;
		uniform float _T1Intensity;
		uniform sampler2D _Texture2;
		uniform float _T2Intensity;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _DepthAmount;
		uniform float _SurfaceContact;
		uniform float _SurfaceAdd;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _RefractAmount;
		uniform float4 _RefractColour;
		uniform float _RefractColourIntensity;
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
			float4 appendResult233 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldUVs238 = appendResult233;
			float4 WaveTileUV239 = ( WorldUVs238 * float4( _WaveTiling, 0.0 , 0.0 ) );
			float2 panner240 = ( ( _WaveTime * _Time.y ) * _WaveSpeed + WaveTileUV239.xy);
			float simplePerlin2D249 = snoise( panner240 );
			simplePerlin2D249 = simplePerlin2D249*0.5 + 0.5;
			float3 WaveHeight255 = ( simplePerlin2D249 * ( _WaveIntensity * _WaveDirection ) );
			v.vertex.xyz += WaveHeight255;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord288 = i.uv_texcoord * _PannerTiling + _PannerOffset;
			float2 panner275 = ( ( _PannerTime * _Time.y ) * _PannerSpeed + uv_TexCoord288);
			float2 Panner280 = panner275;
			float3 break185 = UnpackNormal( tex2D( _NormalTexture1, Panner280 ) );
			float4 appendResult189 = (float4(( break185.x * _NormalIntensity1 ) , ( break185.y * _NormalIntensity1 ) , break185.z , 0.0));
			float4 NormalT117 = appendResult189;
			float3 break264 = UnpackNormal( tex2D( _NormalTexture2, Panner280 ) );
			float4 appendResult267 = (float4(( break264.x * _NormalIntensity2 ) , ( break264.y * _NormalIntensity2 ) , break264.z , 0.0));
			float4 NormalT2268 = appendResult267;
			float temp_output_203_0 = ( _GradientPosition + _GradientWidth );
			float temp_output_204_0 = ( _GradientPosition - _GradientWidth );
			float temp_output_208_0 = (( temp_output_204_0 < i.uv_texcoord.x ) ? i.uv_texcoord.x :  0.0 );
			float smoothstepResult224 = smoothstep( temp_output_204_0 , temp_output_203_0 , (( temp_output_203_0 > temp_output_208_0 ) ? temp_output_208_0 :  0.0 ));
			float clampResult230 = clamp( ( (( temp_output_203_0 < i.uv_texcoord.x ) ? 1.0 :  0.0 ) + smoothstepResult224 ) , 0.0 , 1.0 );
			float Gradient228 = clampResult230;
			float4 lerpResult272 = lerp( NormalT117 , NormalT2268 , Gradient228);
			o.Normal = lerpResult272.xyz;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth3 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth3 = abs( ( screenDepth3 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthAmount ) );
			float clampResult5 = clamp( ( 1.0 - distanceDepth3 ) , 0.0 , 1.0 );
			float Depth6 = clampResult5;
			float4 lerpResult41 = lerp( ( tex2D( _Texture1, Panner280 ) * _T1Intensity ) , ( tex2D( _Texture2, Panner280 ) * _T2Intensity ) , (( Depth6 < _SurfaceContact ) ? ( 1.0 - ( Depth6 + _SurfaceAdd ) ) :  0.0 ));
			float4 Albedo27 = lerpResult41;
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor14 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float4( (ase_grabScreenPosNorm).xy, 0.0 , 0.0 ) + ( _RefractAmount * NormalT117 ) ).xy);
			float4 clampResult15 = clamp( screenColor14 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Refraction16 = clampResult15;
			float4 lerpResult344 = lerp( Refraction16 , _RefractColour , _RefractColourIntensity);
			float4 lerpResult29 = lerp( Albedo27 , lerpResult344 , Depth6);
			o.Albedo = lerpResult29.rgb;
			float4 Emission196 = ( tex2D( _Emission, Panner280 ) * _EmissionIntensity * _EmissionColour );
			float screenDepth308 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth308 = abs( ( screenDepth308 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeDistance ) );
			float3 ase_worldPos = i.worldPos;
			float4 appendResult233 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 WorldUVs238 = appendResult233;
			float2 panner299 = ( 1.0 * _Time.y * _EdgeTiling + ( WorldUVs238 * _EdgeSomething ).xy);
			float4 tex2DNode300 = tex2D( _EdgeTexture, panner299 );
			float4 clampResult305 = clamp( ( ( ( 1.0 - distanceDepth308 ) + tex2DNode300 ) * _EdgePower * _SeaFoamColour ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Edge306 = clampResult305;
			float4 lerpResult346 = lerp( Emission196 , Edge306 , Depth6);
			o.Emission = lerpResult346.rgb;
			float Metallic323 = ( tex2D( _Roughness, Panner280 ).a * _RoughnessIntensity );
			o.Metallic = Metallic323;
			float clampResult340 = clamp( exp( ( 1.0 - saturate( Depth6 ) ) ) , 0.0 , 1.0 );
			float Opacity341 = clampResult340;
			o.Alpha = Opacity341;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
0;0;1368;851;3658.681;-1177.769;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;281;-750.1848,4336.725;Inherit;False;1336.439;1008.063;Comment;8;280;275;279;276;277;278;288;292;Panner;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;349;-1074.55,4368.334;Inherit;False;Property;_PannerTiling;Panner Tiling;33;0;Create;True;0;0;False;0;0,0;13.62,5.15;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;350;-1071.543,4667.522;Inherit;False;Property;_PannerOffset;Panner Offset;34;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;278;-647.7368,4769.327;Inherit;False;Property;_PannerTime;Panner Time;19;0;Create;True;0;0;False;0;0;0.95;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;277;-641.7788,5020.46;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;279;-306.6828,5000.902;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;288;-672.0858,4455.144;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;276;-307.6668,4708.878;Inherit;False;Property;_PannerSpeed;Panner Speed;18;0;Create;True;0;0;False;0;-1,0;0,-0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;275;40.70778,4690.346;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;190;-5479.469,-1729.446;Inherit;False;1805.244;566.2767;Comment;9;31;19;186;185;187;188;189;17;290;Normal Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;280;341.4378,4686.442;Inherit;False;Panner;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;290;-5438.505,-1379.611;Inherit;False;280;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;31;-5429.469,-1675.537;Inherit;True;Property;_NormalTexture1;Normal Texture 1;2;0;Create;True;0;0;False;0;None;2a7b4c9b0ca26f84bb76c23d1a691037;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;19;-5130.128,-1675.895;Inherit;True;Property;_NormalMap;NormalMap;13;0;Create;True;0;0;False;0;-1;None;5b653e484c8e303439ef414b62f969f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;231;-3454.474,3610.355;Inherit;False;810.1257;282.6798;Make all tiles reliant on world space;3;238;233;232;Make all tiles reliant on world space;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1;-1081.652,-1232.455;Inherit;False;1651.3;378.0659;Depth;5;6;5;4;3;2;Depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.BreakToComponentsNode;185;-4774.751,-1670.016;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;186;-4823.066,-1421.169;Inherit;False;Property;_NormalIntensity1;Normal Intensity 1;3;0;Create;True;0;0;False;0;3;1.73;0.01;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;232;-3422.073,3660.355;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;2;-1016.495,-1154.349;Inherit;False;Property;_DepthAmount;DepthAmount;20;0;Create;True;0;0;False;0;0;3.05;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;188;-4455.011,-1436.801;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;3;-620.4692,-1149.58;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;187;-4456.435,-1672.696;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;233;-3145.551,3666.932;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;189;-4179.328,-1672.696;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;4;-265.9053,-1144.802;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;293;-8498.1,1236.786;Inherit;False;2517.084;1434.453;Edge with Texture;17;301;306;305;304;303;312;302;300;309;298;299;308;296;307;297;294;295;Edge with Texture;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;225;-8789.09,-1115.343;Inherit;False;2625.506;854.0272;Comment;12;228;230;199;221;220;224;217;208;203;204;198;202;Gradient Control;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;238;-2868.16,3663.442;Inherit;False;WorldUVs;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;234;-4868.798,3356.477;Inherit;False;802.1453;611.0583;Wave Tiling and UVs;4;235;239;237;236;Wave Tiling and UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;7;-3016.34,-1186.451;Inherit;False;1674.853;797.2925;Refraction;9;16;15;14;13;11;12;10;9;8;Refraction;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;294;-8399.861,2217.991;Inherit;False;Property;_EdgeSomething;Edge Something;25;0;Create;True;0;0;False;0;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;202;-8739.09,-1065.343;Inherit;False;Property;_GradientWidth;Gradient Width;9;0;Create;True;0;0;False;0;0;0.12;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;-3908.224,-1679.446;Inherit;False;NormalT1;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;295;-8431.115,1971.793;Inherit;False;238;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;199;-8730.293,-829.1568;Inherit;False;Property;_GradientPosition;Gradient Position;8;0;Create;True;0;0;False;0;0;0.85;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;5;15.38843,-1137.173;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;297;-8149.557,2341.097;Inherit;False;Property;_EdgeTiling;Edge Tiling;26;0;Create;True;0;0;False;0;0,0;0,-1.4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;262;-5459.912,-1041.792;Inherit;False;1805.244;566.2767;Comment;8;270;269;268;267;266;265;264;263;Normal Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.GrabScreenPosition;9;-2892.197,-1125.334;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;296;-8162.094,2081.362;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;10;-2860.368,-666.63;Inherit;False;17;NormalT1;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;307;-8442.408,1344.561;Inherit;False;Property;_EdgeDistance;Edge Distance;27;0;Create;True;0;0;False;0;1;1.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;204;-8355.733,-828.7696;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;236;-4829.927,3438.27;Inherit;False;238;WorldUVs;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2933.363,-915.5556;Inherit;False;Property;_RefractAmount;RefractAmount;21;0;Create;True;0;0;False;0;0.1;0.27;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;235;-4824.047,3647.354;Inherit;False;Property;_WaveTiling;Wave Tiling;14;0;Create;True;0;0;False;0;0,0;0,-2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RegisterLocalVarNode;6;346.3362,-1144.341;Inherit;False;Depth;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;198;-8338.567,-604.1192;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;237;-4560.415,3444.295;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DepthFade;308;-8180.082,1333.682;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;298;-8305.07,1576.491;Inherit;True;Property;_EdgeTexture;Edge Texture;24;0;Create;True;0;0;False;0;None;6283e1983c081f242b63e6287058e3c6;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;299;-7875.177,2082.238;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;335;1919.873,-859.2216;Inherit;False;1691.374;310.1648;Opa;6;341;340;339;338;337;336;Opacity;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;261;-6265.661,4409.035;Inherit;False;1810.499;1072.69;Comment;8;248;245;246;241;250;247;240;249;Waves;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-2603.615,-885.1353;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;203;-8352.473,-1060.241;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;351;-2145.519,954.2812;Inherit;False;6;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;226;-2979.083,-119.5053;Inherit;False;2239.248;1062.65;Comment;12;317;316;315;229;40;314;39;27;41;26;32;289;Texture Lerp;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCCompareLower;208;-7899.232,-624.941;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;357;-2214.756,1155.366;Inherit;False;Property;_SurfaceAdd;Surface Add;36;0;Create;True;0;0;False;0;0;0.95;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;269;-5409.912,-987.8827;Inherit;True;Property;_NormalTexture2;Normal Texture 2;6;0;Create;True;0;0;False;0;None;065352c36540df54ba2986bfe9a16578;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ComponentMaskNode;11;-2617.604,-1116.192;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;309;-7847.385,1343.056;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;245;-6212.243,4953.963;Inherit;False;Property;_WaveTime;Wave Time;15;0;Create;True;0;0;False;0;1;4.98;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;32;-2918.832,-48.73996;Inherit;True;Property;_Texture1;Texture 1;0;0;Create;True;0;0;False;0;None;4e9f338126105ab4c882fc8f8a14df42;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;336;1977.873,-809.2216;Inherit;False;6;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;300;-7566.163,1579.22;Inherit;True;Property;_TextureSample3;Texture Sample 3;20;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-2325.837,-1023.188;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;227;-3676.631,4475.699;Inherit;False;1362.53;717.103;Comment;7;191;192;193;195;194;196;291;Emission Control;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;356;-1916.768,1064.673;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;248;-6214.751,5185.175;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;39;-2915.315,467.4948;Inherit;True;Property;_Texture2;Texture 2;4;0;Create;True;0;0;False;0;None;6283e1983c081f242b63e6287058e3c6;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;239;-4305.724,3439.58;Inherit;False;WaveTileUV;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;289;-2526.616,32.20215;Inherit;False;280;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCCompareGreater;217;-7554.996,-693.2147;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;263;-5110.572,-988.2407;Inherit;True;Property;_TextureSample2;Texture Sample 2;13;0;Create;True;0;0;False;0;-1;None;5b653e484c8e303439ef414b62f969f0;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;270;-4803.509,-733.5148;Inherit;False;Property;_NormalIntensity2;Normal Intensity 2;7;0;Create;True;0;0;False;0;3;3.3;0.01;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;40;-2267.703,468.2194;Inherit;True;Property;_TextureSample0;Texture Sample 0;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;302;-7151.66,1585.815;Inherit;False;Property;_EdgePower;Edge Power;28;0;Create;True;0;0;False;0;1;0.62;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;291;-3610.495,4744.207;Inherit;False;280;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;246;-6005.043,4957.356;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;337;2260.255,-804.3424;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;250;-5655.131,4802.84;Inherit;False;1140.851;609.6873;Wave Height;6;256;255;254;253;252;251;Wave Height;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenColorNode;14;-2076.693,-1027.63;Inherit;False;Global;_GrabScreen0;Grab Screen 0;23;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;303;-7147.512,1335.477;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;317;-2118.539,166.264;Inherit;False;Property;_T1Intensity;T1 Intensity;1;0;Create;True;0;0;False;0;1;0.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;247;-6210.457,4678.002;Inherit;False;Property;_WaveSpeed;Wave Speed;16;0;Create;True;0;0;False;0;0,0;0.1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;314;-2160.825,687.934;Inherit;False;Property;_T2Intensity;T2 Intensity;5;0;Create;True;0;0;False;0;2;2.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;224;-7252.024,-843.015;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;220;-7502.294,-1062.686;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;191;-3626.631,4528.216;Inherit;True;Property;_Emission;Emission;10;0;Create;True;0;0;False;0;None;cba10a92f655f5642b4589f5c5f64765;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.BreakToComponentsNode;264;-4755.194,-982.3617;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;241;-6215.661,4459.035;Inherit;False;239;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;353;-1509.204,1226.672;Inherit;False;Property;_SurfaceContact;Surface Contact;35;0;Create;True;0;0;False;0;0;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;345;-3225.994,1441.146;Inherit;False;1300.397;648.495;Comment;6;320;325;321;319;322;323;Metallic;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;355;-1687.448,1046.536;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;312;-7089.577,1861.598;Inherit;False;Property;_SeaFoamColour;Sea Foam Colour;29;0;Create;True;0;0;False;0;1,0,0,0;0.759434,1,0.9809989,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;-2241.136,-51.35872;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;195;-3287.417,4974.782;Inherit;False;Property;_EmissionColour;Emission Colour;12;0;Create;True;0;0;False;0;0,0,0,0;0,0.490566,0.331716,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;338;2516.251,-804.0865;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;240;-5742.038,4487.639;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector3Node;252;-5621.581,5100.014;Inherit;False;Property;_WaveDirection;Wave Direction;13;0;Create;True;0;0;False;0;0,1,0;0,0,3;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;251;-5617.246,4866.244;Inherit;False;Property;_WaveIntensity;Wave Intensity;17;0;Create;True;0;0;False;0;0.1;0.81;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;265;-4435.455,-749.1458;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;193;-3326.75,4737.581;Inherit;False;Property;_EmissionIntensity;Emission Intensity;11;0;Create;True;0;0;False;0;0;1;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;266;-4436.878,-985.0417;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;221;-6958.138,-1064.211;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;304;-6761.528,1337.47;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;320;-3204.232,1700.641;Inherit;True;Property;_Roughness;Roughness;30;0;Create;True;0;0;False;0;None;71ff7d72bfa0325419e2f1de32220351;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TFHCCompareLower;352;-1246.494,960.1652;Inherit;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;316;-1806.336,-51.87822;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;315;-1946.075,454.1684;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;15;-1879.603,-1023.777;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;325;-3175.994,1491.146;Inherit;False;280;Panner;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;192;-3334.182,4529.095;Inherit;True;Property;_TextureSample1;Texture Sample 1;12;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;321;-2864.232,1831.641;Inherit;True;Property;_RoughnessIntensity;Roughness Intensity;31;0;Create;True;0;0;False;0;0;0.59;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;253;-5362.706,5083.767;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;305;-6513.928,1335.938;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;16;-1593.495,-1026.734;Inherit;False;Refraction;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;230;-6719.918,-1057.797;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;267;-4159.772,-985.0417;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;319;-2879.232,1610.641;Inherit;True;Property;_TextureSample4;Texture Sample 4;30;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;41;-1234.622,-46.23809;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;194;-2862.193,4532.49;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;249;-5334.087,4479.071;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ExpOpNode;339;2784.817,-802.9086;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;306;-6251.777,1331.123;Inherit;False;Edge;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;22;468.4066,-82.16022;Inherit;False;Property;_RefractColour;Refract Colour;23;0;Create;True;0;0;False;0;1,0,0,0;0.04200781,0.1509434,0.1047923,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;441.4924,119.0806;Inherit;False;Property;_RefractColourIntensity;Refract Colour Intensity;22;0;Create;True;0;0;False;0;1;1.2;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;25;476.9376,-288.2496;Inherit;False;16;Refraction;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-940.2646,-50.14086;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;340;3057.709,-802.9086;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;268;-3888.669,-991.7917;Inherit;False;NormalT2;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;196;-2582.998,4525.7;Inherit;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;228;-6457.332,-1057.845;Inherit;False;Gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;322;-2483.232,1558.741;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;254;-5056.902,4966.391;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;271;989.212,532.5746;Inherit;False;268;NormalT2;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;969.2045,66.36422;Inherit;False;6;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;347;592.9438,1471.421;Inherit;False;6;Depth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;197;602.864,1215.084;Inherit;False;196;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;323;-2179.097,1551.958;Inherit;False;Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;273;994.162,738.3127;Inherit;False;228;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;344;783.5751,-209.2756;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;255;-4765.345,4959.408;Inherit;False;WaveHeight;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;341;3377.247,-807.0565;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;20;986.347,329.6811;Inherit;False;17;NormalT1;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;971.2255,-444.3616;Inherit;False;27;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;310;600.7657,1006.046;Inherit;False;306;Edge;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;324;1576.615,1135.579;Inherit;False;323;Metallic;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;326;1104.121,-848.6468;Inherit;True;Property;_OpacityMap;Opacity Map;32;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;327;1374.364,-818.6198;Inherit;True;Property;_TextureSample5;Texture Sample 5;33;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;311;962.826,1008.167;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;292;-194.3355,4391.645;Inherit;False;239;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;29;1302.82,-211.8739;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;256;-5367.621,4868.369;Inherit;False;WaveHeightIntensity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;229;-1625.39,388.0465;Inherit;False;228;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;343;1916.924,1014.003;Inherit;False;341;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;301;-7128.48,2172.241;Inherit;False;SeaFoamUVs;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;272;1344.704,336.0465;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;346;975.6393,1320.058;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;257;2207.291,1054.484;Inherit;False;255;WaveHeight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2512.814,316.3547;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;River;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;279;0;278;0
WireConnection;279;1;277;0
WireConnection;288;0;349;0
WireConnection;288;1;350;0
WireConnection;275;0;288;0
WireConnection;275;2;276;0
WireConnection;275;1;279;0
WireConnection;280;0;275;0
WireConnection;19;0;31;0
WireConnection;19;1;290;0
WireConnection;185;0;19;0
WireConnection;188;0;185;1
WireConnection;188;1;186;0
WireConnection;3;0;2;0
WireConnection;187;0;185;0
WireConnection;187;1;186;0
WireConnection;233;0;232;1
WireConnection;233;1;232;3
WireConnection;189;0;187;0
WireConnection;189;1;188;0
WireConnection;189;2;185;2
WireConnection;4;0;3;0
WireConnection;238;0;233;0
WireConnection;17;0;189;0
WireConnection;5;0;4;0
WireConnection;296;0;295;0
WireConnection;296;1;294;0
WireConnection;204;0;199;0
WireConnection;204;1;202;0
WireConnection;6;0;5;0
WireConnection;237;0;236;0
WireConnection;237;1;235;0
WireConnection;308;0;307;0
WireConnection;299;0;296;0
WireConnection;299;2;297;0
WireConnection;12;0;8;0
WireConnection;12;1;10;0
WireConnection;203;0;199;0
WireConnection;203;1;202;0
WireConnection;208;0;204;0
WireConnection;208;1;198;1
WireConnection;208;2;198;1
WireConnection;11;0;9;0
WireConnection;309;0;308;0
WireConnection;300;0;298;0
WireConnection;300;1;299;0
WireConnection;13;0;11;0
WireConnection;13;1;12;0
WireConnection;356;0;351;0
WireConnection;356;1;357;0
WireConnection;239;0;237;0
WireConnection;217;0;203;0
WireConnection;217;1;208;0
WireConnection;217;2;208;0
WireConnection;263;0;269;0
WireConnection;263;1;290;0
WireConnection;40;0;39;0
WireConnection;40;1;289;0
WireConnection;246;0;245;0
WireConnection;246;1;248;0
WireConnection;337;0;336;0
WireConnection;14;0;13;0
WireConnection;303;0;309;0
WireConnection;303;1;300;0
WireConnection;224;0;217;0
WireConnection;224;1;204;0
WireConnection;224;2;203;0
WireConnection;220;0;203;0
WireConnection;220;1;198;1
WireConnection;264;0;263;0
WireConnection;355;0;356;0
WireConnection;26;0;32;0
WireConnection;26;1;289;0
WireConnection;338;0;337;0
WireConnection;240;0;241;0
WireConnection;240;2;247;0
WireConnection;240;1;246;0
WireConnection;265;0;264;1
WireConnection;265;1;270;0
WireConnection;266;0;264;0
WireConnection;266;1;270;0
WireConnection;221;0;220;0
WireConnection;221;1;224;0
WireConnection;304;0;303;0
WireConnection;304;1;302;0
WireConnection;304;2;312;0
WireConnection;352;0;351;0
WireConnection;352;1;353;0
WireConnection;352;2;355;0
WireConnection;316;0;26;0
WireConnection;316;1;317;0
WireConnection;315;0;40;0
WireConnection;315;1;314;0
WireConnection;15;0;14;0
WireConnection;192;0;191;0
WireConnection;192;1;291;0
WireConnection;253;0;251;0
WireConnection;253;1;252;0
WireConnection;305;0;304;0
WireConnection;16;0;15;0
WireConnection;230;0;221;0
WireConnection;267;0;266;0
WireConnection;267;1;265;0
WireConnection;267;2;264;2
WireConnection;319;0;320;0
WireConnection;319;1;325;0
WireConnection;41;0;316;0
WireConnection;41;1;315;0
WireConnection;41;2;352;0
WireConnection;194;0;192;0
WireConnection;194;1;193;0
WireConnection;194;2;195;0
WireConnection;249;0;240;0
WireConnection;339;0;338;0
WireConnection;306;0;305;0
WireConnection;27;0;41;0
WireConnection;340;0;339;0
WireConnection;268;0;267;0
WireConnection;196;0;194;0
WireConnection;228;0;230;0
WireConnection;322;0;319;4
WireConnection;322;1;321;0
WireConnection;254;0;249;0
WireConnection;254;1;253;0
WireConnection;323;0;322;0
WireConnection;344;0;25;0
WireConnection;344;1;22;0
WireConnection;344;2;21;0
WireConnection;255;0;254;0
WireConnection;341;0;340;0
WireConnection;327;0;326;0
WireConnection;311;0;310;0
WireConnection;311;1;197;0
WireConnection;29;0;28;0
WireConnection;29;1;344;0
WireConnection;29;2;30;0
WireConnection;256;0;251;0
WireConnection;301;0;300;0
WireConnection;272;0;20;0
WireConnection;272;1;271;0
WireConnection;272;2;273;0
WireConnection;346;0;197;0
WireConnection;346;1;310;0
WireConnection;346;2;347;0
WireConnection;0;0;29;0
WireConnection;0;1;272;0
WireConnection;0;2;346;0
WireConnection;0;3;324;0
WireConnection;0;9;343;0
WireConnection;0;11;257;0
ASEEND*/
//CHKSM=8C70EFE0E3D25D23CACD836149611E152EEDB47A