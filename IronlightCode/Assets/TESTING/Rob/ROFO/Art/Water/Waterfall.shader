Shader "Custom/Waterfall"
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
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
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
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float4 tSpace0 : TEXCOORD4;
				float4 tSpace1 : TEXCOORD5;
				float4 tSpace2 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}

    FallBack "Diffuse"
}
