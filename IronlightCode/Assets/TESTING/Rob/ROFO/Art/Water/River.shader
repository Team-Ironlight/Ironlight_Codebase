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
			float4 Metallic323 = ( tex2D( _Roughness, Panner280 ) * _RoughnessIntensity );
			o.Metallic = Metallic323.r;
			float clampResult340 = clamp( exp( ( 1.0 - saturate( Depth6 ) ) ) , 0.0 , 1.0 );
			float Opacity341 = clampResult340;
			o.Alpha = Opacity341;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
